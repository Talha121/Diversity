using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
namespace Diversity.Infrastructure.SharedRepositories
{
    public class GenericRepository<TEntity> : IDisposable, IGenericRepository<TEntity> where TEntity : class
    {
        protected GenericContext DataContext;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return this.DataContext;
            }
        }

        //private IEntityHelper<TEntity> _entityHelper;
        public GenericRepository(GenericContext context)
        {
            DataContext = context;
        }
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await DataContext.Set<TEntity>().AddAsync(entity);
            await SaveAsync();
            return entity;
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            if (DataContext.Entry(entity).State == EntityState.Detached)
            {
                Attach(entity);
            }
            DataContext.Set<TEntity>().Remove(entity);
            await SaveAsync();
        }
        public virtual async Task DeleteAsync(int id)
        {
            TEntity entity = await DataContext.Set<TEntity>().FindAsync(id);
            await DeleteAsync(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await DataContext.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await DataContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetPagedReponseAsync(int pageNumber, int pageSize, string fields, string orderBy)
        {
            var items = await DataContext.Set<TEntity>().Skip((pageNumber - 1) * pageSize).Take(pageSize).OrderBy(orderBy).AsNoTracking().ToListAsync();

            var result = items.AsQueryable();

            if (!string.IsNullOrEmpty(orderBy))
            {
                result = result.OrderBy(orderBy);
            }
            if (!string.IsNullOrEmpty(fields))
            {
                result = result.Select<TEntity>("new(" + fields + ")");
            }
            return result;
        }

        public async Task<IEnumerable<TEntity>> GetPagedReponseAsync(int pageNumber, int pageSize)
        {
            return await DataContext.Set<TEntity>().Skip((pageNumber - 1) * pageSize).Take(pageSize).AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, Func<TEntity, bool> key = null)
        {
            InternalUpdate(entity, key);
            await SaveAsync();
            return entity;
        }

        public virtual async Task SaveAsync()
        {
            await DataContext.SaveChangesAsync();
        }

        protected void InternalUpdate(TEntity entity, Func<TEntity, bool> key)
        {
            var entry = DataContext.Entry<TEntity>(entity);
            if (entry.State == EntityState.Detached)
            {
                var set = DataContext.Set<TEntity>();
                TEntity attachedEntity = null == key ? null : set.Local.SingleOrDefault(key);
                if (attachedEntity != null)
                {
                    var attachedEntry = DataContext.Entry(attachedEntity);
                    attachedEntry.CurrentValues.SetValues(entity);
                }
                else
                {
                    entry.State = EntityState.Modified;
                }
            }
        }

        protected virtual void Attach(TEntity entity)
        {
            DataContext.Set<TEntity>().Attach(entity);
        }

        public void Dispose()
        {
            DataContext.Dispose();
        }

        public async Task<IEnumerable<TEntity>> GetPagedReponseAsync(int pageNumber, int pageSize, string orderBy)
        {
            var items = await DataContext.Set<TEntity>().Skip((pageNumber - 1) * pageSize).Take(pageSize).OrderBy(orderBy).AsNoTracking().ToListAsync();

            var result = items.AsQueryable();

            if (!string.IsNullOrEmpty(orderBy))
            {
                result = result.OrderBy(orderBy);
            }
            return result;
        }
    }
}
