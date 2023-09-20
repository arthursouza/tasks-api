using data;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly DataContext context;

        public BaseRepository(DataContext context)
        {
            this.context = context;
        }

        public async Task AddOrUpdateAsync(T entity)
        {
            if (entity.Id > 0)
            {
                context.Entry(entity).State = EntityState.Modified;
            }
            else
            {
                await context.Set<T>().AddAsync(entity);
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public IQueryable<T> Queryable(bool tracking = true)
        {
            return tracking ? context.Set<T>().AsQueryable() : context.Set<T>().AsQueryable().AsNoTracking();
        }

        public void Remove(T entity)
        {
            context.Remove(entity);
        }
    }
}