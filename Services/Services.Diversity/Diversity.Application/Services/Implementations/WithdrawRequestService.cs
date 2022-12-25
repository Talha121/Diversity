using AutoMapper;
using Diversity.Application.Models;
using Diversity.Application.Services.Interfaces;
using Diversity.Domain.Entities;
using Diversity.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diversity.Application.Services.Implementations
{
    public class WithdrawRequestService : IWithdrawRequestService
    {
        private readonly IWithdrawRequestRepository withdrawRequestRepository;
        private readonly IMapper mapper;
        public WithdrawRequestService(IWithdrawRequestRepository withdrawRequestRepository, IMapper mapper)
        {
            this.withdrawRequestRepository = withdrawRequestRepository;
            this.mapper = mapper;
        }

        public async Task<WithdrawRequestDTO> CreateWithdrawRequest(WithdrawRequestDTO request)
        {
            try
            {
                WithdrawRequest withdrawRequest = this.mapper.Map<WithdrawRequest>(request);
                var insert = await this.withdrawRequestRepository.AddAsync(withdrawRequest);
                WithdrawRequestDTO response = this.mapper.Map<WithdrawRequestDTO>(insert);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<WithdrawRequestDTO>> GetAllWithdrawRequests()
        {
            var data = await this.withdrawRequestRepository.GetWithdrawRequestsAsync();
            List<WithdrawRequestDTO> withdrawRequestModel = this.mapper.Map<List<WithdrawRequestDTO>>(data);
            return withdrawRequestModel;
        }

        public async Task<List<WithdrawRequestDTO>> GetUserWithdrawRequests(int userId)
        {
            var data = await this.withdrawRequestRepository.GetWithdrawRequestsByUserIdAsync(userId);
            List<WithdrawRequestDTO> withdrawRequestModel = this.mapper.Map<List<WithdrawRequestDTO>>(data);
            return withdrawRequestModel;
        }

        public async Task<WithdrawRequestDTO> UpdateWithdrawRequest(WithdrawRequestDTO request)
        {
            var data = await this.withdrawRequestRepository.GetByIdAsync((int)request.Id);
            data.Status = request.Status;
            var updateRquest = await this.withdrawRequestRepository.UpdateAsync(data);
            WithdrawRequestDTO withdrawData = this.mapper.Map<WithdrawRequestDTO>(updateRquest);
            return withdrawData;
        }
    }
}
