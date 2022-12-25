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
    public class WithdrawRequestRepository : GenericRepository<WithdrawRequest>, IWithdrawRequestRepository
    {
        public WithdrawRequestRepository(GenericContext context) : base(context)
        {
        }

        public async Task<List<WithdrawRequest>> GetWithdrawRequestsAsync()
        {
            var data = await this.DataContext.Set<WithdrawRequest>().Include(x => x.User).ToListAsync();
            return data;
        }

        public async Task<List<WithdrawRequest>> GetWithdrawRequestsByUserIdAsync(int userId)
        {
            var data = await this.DataContext.Set<WithdrawRequest>().Include(x => x.User).Where(x => x.UserId == userId).ToListAsync();
            return data;
        }
    }
}
