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
    public class UserAccountRepository : GenericRepository<UserAccount>, IUserAccountRepository
    {
        public UserAccountRepository(DataContext context) : base(context)
        {
        }

        public async Task<List<UserAccount>> GetAllUserAccount()
        {
            return await this.DataContext.Set<UserAccount>().Include(x => x.User).AsNoTracking().ToListAsync();
        }

        public async Task<UserAccount> GetByUserIdAsync(int userId)
        {
            return await this.DataContext.Set<UserAccount>().Where(x => x.UserId == userId).Include(x=>x.User).AsNoTracking().FirstOrDefaultAsync();
        }
    }
}
