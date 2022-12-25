using Diversity.Application.Models;
using Diversity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diversity.Application.Services.Interfaces
{
    public interface IWithdrawRequestService
    {
        Task<List<WithdrawRequestDTO>> GetAllWithdrawRequests();
        Task<List<WithdrawRequestDTO>> GetUserWithdrawRequests(int userId);
        Task<WithdrawRequestDTO> CreateWithdrawRequest(WithdrawRequestDTO request);
        Task<WithdrawRequestDTO> UpdateWithdrawRequest(WithdrawRequestDTO request);
    }
}
