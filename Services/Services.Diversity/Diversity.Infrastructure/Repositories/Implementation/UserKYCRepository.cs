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
    public class UserKYCRepository : GenericRepository<UserKYC>, IUserKYCRepository
    {
        public UserKYCRepository(DataContext context) : base(context)
        {
        }

        public async Task<List<UserKYC>> GetAllUserKYC()
        {
            return await this.DataContext.Set<UserKYC>().Include(x=>x.User).ToListAsync();
        }

        public async Task<UserKYC> GetByUser(int userId)
        {
            return await this.DataContext.Set<UserKYC>().Where(x => x.UserId == userId).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<UserKYC> GetKYCById(int Id)
        {
            return await this.DataContext.Set<UserKYC>().Where(x => x.Id == Id).AsNoTracking().FirstOrDefaultAsync();
        }
    }
}
