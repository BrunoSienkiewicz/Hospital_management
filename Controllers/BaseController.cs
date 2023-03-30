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
        private readonly IMapper mapper;
        private readonly string IdName;

        public BaseController(HospitalDbContext hospitalDbContext)
        {
            this.hospitalDbContext = hospitalDbContext;
            this.IdName = GetIdName();
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new EntityMappingProfile<TAddEntityModel, TUpdateEntityModel, TEntity>());
            });
            IMapper mapper = mapperConfig.CreateMapper();
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

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var entities = await hospitalDbContext.Set<TEntity>().ToListAsync();
            return View(entities);
        }

        [HttpPost]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var parameter = Expression.Parameter(typeof(TEntity), "e");
            var predicate = Expression.Lambda<Func<TEntity, bool>>(
                Expression.Equal(
                    Expression.PropertyOrField(parameter, IdName),
                    Expression.Constant(id)
                ),
                parameter
            );
            var entity = await hospitalDbContext.Set<TEntity>().FirstOrDefaultAsync(predicate);

            return View(entity);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> Add(TAddEntityModel addModel)
        //{
        //    int maxId = await hospitalDbContext.Set<TEntity>().MaxAsync(e => e.Id);

        //    var entity = mapper.Map<TEntity>(addModel);
        //    entity.Id = maxId + 1;

        //    await hospitalDbContext.Set<TEntity>().AddAsync(entity);
        //    await hospitalDbContext.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

        [HttpPost]
        public async Task<IActionResult> Update(TUpdateEntityModel updateModel)
        {
            var entity = mapper.Map<TEntity>(updateModel);

            hospitalDbContext.Entry(entity).State = EntityState.Modified;
            await hospitalDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
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
            hospitalDbContext.Set<TEntity>().Remove(entity);
            await hospitalDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }
}
