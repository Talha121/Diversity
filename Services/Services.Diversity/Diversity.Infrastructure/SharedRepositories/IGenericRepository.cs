using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diversity.Infrastructure.SharedRepositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IUnitOfWork UnitOfWork { get; }

        Task<TEntity> GetByIdAsync(int id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetPagedReponseAsync(int pageNumber, int pageSize);
        Task<IEnumerable<TEntity>> GetPagedReponseAsync(int pageNumber, int pageSize, string fields, string orderBy);
        Task<IEnumerable<TEntity>> GetPagedReponseAsync(int pageNumber, int pageSize, string orderBy);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity, Func<TEntity, bool> key = null);
        Task DeleteAsync(TEntity entity);
        Task DeleteAsync(int id);
        Task SaveAsync();
    }
}
