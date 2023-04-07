namespace Hospital_Management.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        public Task<int> GetMaxId();
        public Task<List<TEntity>> GetAll();
        public Task<TEntity> GetEntityById(int? id);
        public Task<TEntity> AddEntity(TEntity entity);
        public Task<TEntity> UpdateEntity(TEntity entity);
        public Task<TEntity> DeleteEntity(TEntity entity);
    }
}
