using Diversity.Domain.Entities;
using Diversity.Infrastructure.Repositories.Interfaces;
using Diversity.Infrastructure.SharedRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diversity.Infrastructure.Repositories.Implementation
{
    public class DepositRequestRepository : GenericRepository<DepositRequest>, IDepositRequestsRepository
    {
        public DepositRequestRepository(DataContext context) : base(context)
        {

        }

        public async Task<List<DepositRequest>> GetDepositRequestsAsync()
        {
            var data= await this.DataContext.Set<DepositRequest>().Include(x=>x.User).ToListAsync();
            return data;
        }

        public async Task<List<DepositRequest>> GetDepositRequestsByUserIdAsync(int userId)
        {
            var data = await this.DataContext.Set<DepositRequest>().Include(x => x.User).Where(x=>x.UserId==userId).ToListAsync();
            return data;
        }
    }
}
