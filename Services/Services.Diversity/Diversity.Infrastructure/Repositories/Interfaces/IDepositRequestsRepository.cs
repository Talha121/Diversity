using Diversity.Domain.Entities;
using Diversity.Infrastructure.SharedRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diversity.Infrastructure.Repositories.Interfaces
{
    public interface IDepositRequestsRepository : IGenericRepository<DepositRequest>
    {
        Task<List<DepositRequest>> GetDepositRequestsAsync();
        Task<List<DepositRequest>> GetDepositRequestsByUserIdAsync(int userId);
    }
}
