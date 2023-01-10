using Diversity.Application.Models;
using Diversity.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diversity.Application.Services.Interfaces
{
    public interface IDepositRequestService
    {
        Task<List<DepositRequestDTO>> GetAllDepositRequests();
        Task<List<DepositRequestDTO>> GetUserDepositRequests(int userId);
        Task<DepositRequest> CreateDepositRequest(DepositRequestDTO request);
        Task<DepositRequestDTO> UpdateDepositRequest(DepositRequestDTO request);

        Task<BankDetails> CreateBankDetails(BankDetailDTO dto);
        Task<BankDetails> GetBankDetails();
    }
}
