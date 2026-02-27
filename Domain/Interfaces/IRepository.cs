namespace Domain.Interfaces
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        Task<IReadOnlyCollection<TEntity>> GetListAsync();
        Task<TEntity?> GetByIdAsync(int id);
        Task InsertAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task<int> SaveChangesAsync();
    }
}
