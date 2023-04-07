using AutoMapper;

namespace Hospital_Management.Data
{
    public class EntityMappingProfile<TAddEntityModel, TUpdateEntityModel, TEntity, TEntityDto> : Profile where TEntity : class
    {
        public EntityMappingProfile()
        {
            CreateMap<TAddEntityModel, TEntity>();
            CreateMap<TUpdateEntityModel, TEntity>();
            CreateMap<TEntity, TUpdateEntityModel>();
            CreateMap<TEntity, TAddEntityModel>();
            CreateMap<TEntity, TEntityDto>();
            CreateMap<TEntityDto, TEntity>();
        }
    }
}
