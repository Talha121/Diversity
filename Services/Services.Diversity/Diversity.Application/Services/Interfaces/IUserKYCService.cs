using Diversity.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diversity.Application.Services.Interfaces
{
    public interface IUserKYCService
    {
        Task<bool> CreateKYC(UserKYCDTO dto);
        Task<bool> UpdateStatus(UserKYCDTO dto);
        Task<UserKYCDTO> GetUserKYC(int userId);
        Task<List<UserKYCDTO>> GetAllUsersKYC();

    }
}
