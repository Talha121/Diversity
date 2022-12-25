using Diversity.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diversity.Application.Services.Interfaces
{
    public interface IUserDetailService
    {
        Task<List<UserDetailDTO>> GetAllUsers();
        Task<UserDetailDTO> GetUserProfile(int? userId);
        Task<UserDetailDTO> UpdateUserProfile(UserDetailDTO userDetail);
        Task<UserDetailDTO> CreateUser(UserDetailDTO userDetail);
        Task<UserDetailDTO> EnableDisableUser(int? userId,bool isActive);
        Task<UserDetailDTO> GetUserByEmail(string email);

    }
}
