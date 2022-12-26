using Diversity.Domain.Entities;
using Diversity.Infrastructure.SharedRepositories;
using System.Threading.Tasks;

namespace Diversity.Infrastructure.Repositories.Interfaces
{
    public interface IUserDetailRepository : IGenericRepository<UserDetail>
    {
        Task<UserDetail> GetByEmail(string email);
        Task<UserDetail> GetByUserId(int Id);
    }
}
