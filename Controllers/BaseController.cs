using Hospital_Management.Data;
using Hospital_Management.Models.Domain;
using Hospital_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using AutoMapper;
using System.Linq.Expressions;

namespace Hospital_Management.Controllers
{
    public class BaseController<TEntity, TAddEntityModel, TUpdateEntityModel>: Controller where TEntity : class
    {
        protected readonly HospitalDbContext hospitalDbContext;
        protected IMapper mapper;
        protected readonly string IdName;

        public BaseController(HospitalDbContext hospitalDbContext)
        {
            this.hospitalDbContext = hospitalDbContext;
            this.IdName = GetIdName();
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new EntityMappingProfile<TAddEntityModel, TUpdateEntityModel, TEntity>());
            });
            mapper = mapperConfig.CreateMapper();
        }
        private string GetIdName()
        {
            var properties = typeof(TEntity).GetProperties();
            foreach (var property in properties)
            {
                if (property.Name.ToLower().Contains("id"))
                {
                    return property.Name;
                }
            }
            return null;
        }

        private async Task<TEntity> GetEntityById(int? id)
        {
            var parameter = Expression.Parameter(typeof(TEntity), "e");
        	var predicate = Expression.Lambda<Func<TEntity, bool>>(
                Expression.Equal(
                Expression.PropertyOrField(parameter, IdName),
                Expression.Constant(id)
                ),
				parameter
			);
			var entity = await hospitalDbContext.Set<TEntity>().FirstOrDefaultAsync(predicate);
            return entity;
		}

        private async Task<int> GetMaxId()
        {
			var parameter = Expression.Parameter(typeof(TEntity), "e");
			var property = Expression.PropertyOrField(parameter, IdName);
			var maxIdExpression = Expression.Lambda<Func<TEntity, int>>(property, parameter);
            var maxId = await hospitalDbContext.Set<TEntity>()
                .MaxAsync(maxIdExpression);

			return maxId;
		}

		[HttpGet]
        public async Task<IActionResult> Index()
        {
            var entities = await hospitalDbContext.Set<TEntity>().ToListAsync();
            return View(entities);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var entity = await GetEntityById(id);
            var entityModel = mapper.Map<TUpdateEntityModel>(entity);

            return await Task.Run(() => View("View", entityModel));
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(TAddEntityModel addModel)
        {
            var entity = mapper.Map<TEntity>(addModel);
			entity.GetType().GetProperty(IdName).SetValue(entity, await GetMaxId() + 1);

			await hospitalDbContext.Set<TEntity>().AddAsync(entity);
            await hospitalDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Update(TUpdateEntityModel updateModel)
        {
            var entity = mapper.Map<TEntity>(updateModel);

            hospitalDbContext.Entry(entity).State = EntityState.Modified;
            await hospitalDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await GetEntityById(id);
            var model = mapper.Map<TUpdateEntityModel>(entity);

			return await Task.Run(() => Delete(model));
		}

        [HttpPost]
        public async Task<IActionResult> Delete(TUpdateEntityModel model)
        {
			var parameter = Expression.Parameter(typeof(TUpdateEntityModel), "e");
			var property = Expression.PropertyOrField(parameter, IdName);
            var entityId = Expression.Lambda<Func<TUpdateEntityModel, int>>(property, parameter).Compile()(model);
			var entity = await GetEntityById(entityId);

            hospitalDbContext.Set<TEntity>().Remove(entity);
            await hospitalDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
