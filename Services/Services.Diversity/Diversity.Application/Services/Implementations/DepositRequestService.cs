using AutoMapper;
using Diversity.Application.Models;
using Diversity.Application.Services.Interfaces;
using Diversity.Domain.Entities;
using Diversity.Infrastructure.Repositories.Implementation;
using Diversity.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diversity.Application.Services.Implementations
{
    public class DepositRequestService : IDepositRequestService
    {
        private readonly IDepositRequestsRepository depositRequestRepository;
        private readonly IFileService fileService;
        private readonly IMapper mapper;
        public DepositRequestService(IDepositRequestsRepository depositRequestRepository, IFileService fileService,IMapper mapper)
        {
            this.depositRequestRepository = depositRequestRepository;
            this.fileService = fileService;
            this.mapper = mapper;
        }
        public async Task<DepositRequest> CreateDepositRequest(DepositRequestDTO request)
        {
            try
            {
                var fileName =await this.fileService.UploadedFile(request.ProofFile, "Deposit");
                DepositRequest depositrequest = new DepositRequest()
                {
                    Amount = request.Amount,
                    OtherDetails = request.OtherDetails,
                    Status = request.Status,
                    Type = request.Type,
                    ProofPath = "Images/Deposit/"+fileName,
                    UserId = (int)request.UserId
                };
                var insert = await this.depositRequestRepository.AddAsync(depositrequest);
                return insert;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DepositRequestDTO>> GetAllDepositRequests()
        {
            var data =await this.depositRequestRepository.GetDepositRequestsAsync();
            List<DepositRequestDTO> depositRequestModel = this.mapper.Map<List<DepositRequestDTO>>(data);
            return depositRequestModel;
        }

        public async Task<List<DepositRequestDTO>> GetUserDepositRequests(int userId)
        {
            var data = await this.depositRequestRepository.GetDepositRequestsByUserIdAsync(userId);
            List<DepositRequestDTO> depositRequestModel = this.mapper.Map<List<DepositRequestDTO>>(data);
            return depositRequestModel;
        }

        public async Task<DepositRequestDTO> UpdateDepositRequest(DepositRequestDTO request)
        {
            var data =await this.depositRequestRepository.GetByIdAsync((int)request.Id);
            data.Status = request.Status;
            var updateRquest = await this.depositRequestRepository.UpdateAsync(data);
            DepositRequestDTO depositData = this.mapper.Map<DepositRequestDTO>(updateRquest);
            return depositData;
        }
    }
}
