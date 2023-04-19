using Hospital_Management.Data;
using Hospital_Management.Models.Domain;
using Hospital_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using AutoMapper;
using System.Linq.Expressions;
using Hospital_Management.Interfaces;

namespace Hospital_Management.Controllers
{
    public class BaseController<TEntity, TAddEntityModel, TUpdateEntityModel, TEntityDto, TRepository>: Controller
        where TEntity : class
        where TRepository : IRepository<TEntity>
    {
        protected IMapper mapper;
        protected readonly string IdName;
        protected readonly TRepository entityRepository;

		public BaseController(TRepository entityRepository)
		{
			this.IdName = GetIdName();
			var mapperConfig = new MapperConfiguration(cfg =>
			{
				cfg.AddProfile(new EntityMappingProfile<TAddEntityModel, TUpdateEntityModel, TEntity, TEntityDto>());
			});
			mapper = mapperConfig.CreateMapper();
			this.entityRepository = entityRepository;
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

        protected async virtual Task<TUpdateEntityModel> mapToUpdateEntityModel(TEntity entity)
        {
            var entityModel = mapper.Map<TUpdateEntityModel>(entity);
            return entityModel;
        }

		protected async virtual Task<TEntity> mapToEntity(TAddEntityModel entityModel)
		{
            var entity = mapper.Map<TEntity>(entityModel);
            return entity;
		}

		protected async virtual Task<TEntity> mapToEntity(TUpdateEntityModel entityModel)
		{
            var entity = mapper.Map<TEntity>(entityModel);
            return entity;
		}

		protected async virtual Task<TAddEntityModel> mapToAddEntityModel(TEntity entity)
		{
            var entityModel = mapper.Map<TAddEntityModel>(entity);
            return entityModel;
		}

        protected async virtual Task<TEntityDto> mapToEntityDto(TEntity entity)
        {
			var entityDto = mapper.Map<TEntityDto>(entity);
			return entityDto;
		}

		[HttpGet]
        public virtual async Task<IActionResult> Index()
        {
            var entities = await entityRepository.GetAll();
            List<TEntityDto> entitiesDto = new List<TEntityDto>();
            foreach(var entity in  entities)
            {
                var entityDto = await mapToEntityDto(entity);
                entitiesDto.Add(entityDto);
            }
            return View(entitiesDto);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var entity = await entityRepository.GetEntityById(id);
            var entityModel = await this.mapToUpdateEntityModel(entity);

            return await Task.Run(() => View("View", entityModel));
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public virtual async Task<IActionResult> Add(TAddEntityModel addModel)
        {
            var entity = await this.mapToEntity(addModel);
			entity.GetType().GetProperty(IdName).SetValue(entity, await entityRepository.GetMaxId() + 1);

			await entityRepository.AddEntity(entity);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public virtual async Task<IActionResult> Update(TUpdateEntityModel updateModel)
        {
            var entity = await this.mapToEntity(updateModel);

            await entityRepository.UpdateEntity(entity);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public virtual async Task<IActionResult> Delete(int id)
        {
            var entity = await entityRepository.GetEntityById(id);
            var model = await this.mapToUpdateEntityModel(entity);

			return await Task.Run(() => Delete(model));
		}

        [HttpPost]
        public virtual async Task<IActionResult> Delete(TUpdateEntityModel model)
        {
			var parameter = Expression.Parameter(typeof(TUpdateEntityModel), "e");
			var property = Expression.PropertyOrField(parameter, IdName);
            var entityId = Expression.Lambda<Func<TUpdateEntityModel, int>>(property, parameter).Compile()(model);
			var entity = await entityRepository.GetEntityById(entityId);

            await entityRepository.DeleteEntity(entity);
            return RedirectToAction("Index");
        }
    }
}
