using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Core
{
    public class Repository<TEntity>(DTADbContext context) : IRepository<TEntity>
        where TEntity : class
    {
        protected DbSet<TEntity> Entities { get; } = context.Set<TEntity>();
        public virtual async Task<IReadOnlyCollection<TEntity>> GetListAsync() =>
            await Entities.ToListAsync();
        public virtual async Task<TEntity?> GetByIdAsync(int id) =>
            await Entities.FindAsync(id);
        public virtual async Task InsertAsync(TEntity entity) =>
            await Entities.AddAsync(entity);
        public virtual void Update(TEntity entity) =>
            Entities.Update(entity);
        public virtual void Delete(TEntity entity) =>
            Entities.Remove(entity);
        public virtual async Task<int> SaveChangesAsync() =>
            await context.SaveChangesAsync();
    }
}
