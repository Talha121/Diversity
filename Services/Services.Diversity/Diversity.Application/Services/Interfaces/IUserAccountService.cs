using Diversity.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diversity.Application.Services.Interfaces
{
    public interface IUserAccountService
    {
        Task<UserAccountDTO> CreateUserAccount(UserAccountDTO userAccount);
        Task<UserAccountDTO> UpdateUserAccount(UserAccountDTO userAccount);
        Task<UserAccountDTO> GetUserAccountById(int userId);
        Task<List<UserAccountDTO>> GetUserAccountList();

    }
}
