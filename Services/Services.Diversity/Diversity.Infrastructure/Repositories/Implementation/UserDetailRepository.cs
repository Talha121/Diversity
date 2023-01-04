using Diversity.Domain.Entities;
using Diversity.Infrastructure.Repositories.Interfaces;
using Diversity.Infrastructure.SharedRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Diversity.Infrastructure.Repositories.Implementation
{
    public class UserDetailRepository : GenericRepository<UserDetail>, IUserDetailRepository
    {
        public UserDetailRepository(DataContext context) : base(context)
        {
        }

        public async Task<List<UserDetail>> GetAllUsersDetails()
        {
            var data=await this.DataContext.Set<UserDetail>().Include(x=>x.UserAccounts).Include(x=>x.Orders).ToListAsync();
            return data;
        }

        public async Task<UserDetail> GetByEmail(string email)
        {
            var data= await this.DataContext.Set<UserDetail>().Where(x => x.Email == email).FirstOrDefaultAsync();
            return data;
        }

        public async Task<UserDetail> GetByUserId(int Id)
        {
            try
            {
                var data = await this.DataContext.Set<UserDetail>().Where(x => x.Id == Id).FirstOrDefaultAsync();
                return data;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
