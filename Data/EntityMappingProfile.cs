using AutoMapper;

namespace Hospital_Management.Data
{
    public class EntityMappingProfile<TAddEntityModel, TUpdateEntityModel, TEntity> : Profile where TEntity : class
    {
        public EntityMappingProfile()
        {
            CreateMap<TAddEntityModel, TEntity>();
            CreateMap<TUpdateEntityModel, TEntity>();
        }
    }
}
