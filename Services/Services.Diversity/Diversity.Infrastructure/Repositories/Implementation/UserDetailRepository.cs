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
    public class UserDetailRepository : GenericRepository<UserDetail>, IUserDetailRepository
    {
        public UserDetailRepository(GenericContext context) : base(context)
        {
        }

        public async Task<UserDetail> GetByEmail(string email)
        {
            return await this.DataContext.Set<UserDetail>().Where(x => x.Email == email).FirstOrDefaultAsync();
        }
    }
}
