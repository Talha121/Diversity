using Diversity.Domain.Entities;
using Diversity.Infrastructure.SharedRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diversity.Infrastructure.Repositories.Interfaces
{
    public interface IUserKYCRepository : IGenericRepository<UserKYC>
    {
        Task<UserKYC> GetByUser(int userId);
        Task<UserKYC> GetKYCById(int Id);
        Task<List<UserKYC>> GetAllUserKYC();
    }
}
