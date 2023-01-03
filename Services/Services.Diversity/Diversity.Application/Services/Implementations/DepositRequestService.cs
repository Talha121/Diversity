using AutoMapper;
using Diversity.Application.Models;
using Diversity.Application.Services.Interfaces;
using Diversity.Domain.Entities;
using Diversity.Infrastructure.Repositories.Implementation;
using Diversity.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
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
        private readonly IBankDetailRepository bankDetailRepository;
        private readonly IFileService fileService;
        private readonly IMapper mapper;
        private readonly IUserAccountService userAccountService;
        public DepositRequestService(IDepositRequestsRepository depositRequestRepository, IFileService fileService, IMapper mapper, IUserAccountService userAccountService, IBankDetailRepository bankDetailRepository)
        {
            this.depositRequestRepository = depositRequestRepository;
            this.fileService = fileService;
            this.mapper = mapper;
            this.userAccountService = userAccountService;
            this.bankDetailRepository = bankDetailRepository;
        }

        public async Task<BankDetails> CreateBankDetails(IFormFile file)
        {
            var fileName = await this.fileService.UploadedFile(file);
            BankDetails details = new BankDetails()
            {
                ImagePath =fileName
            };
            var create=await this.bankDetailRepository.AddAsync(details);
            return create;
        }

        public async Task<DepositRequest> CreateDepositRequest(DepositRequestDTO request)
        {
            try
            {
                var fileName = await this.fileService.UploadedFile(request.ProofFile);
                DepositRequest depositrequest = new DepositRequest()
                {
                    Amount = request.Amount,
                    OtherDetails = request.OtherDetails,
                    Status = request.Status,
                    Type = request.Type,
                    ProofPath = fileName,
                    UserId = (int)request.UserId
                };
                var insert = await this.depositRequestRepository.AddAsync(depositrequest);
                return insert;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<DepositRequestDTO>> GetAllDepositRequests()
        {
            var data = await this.depositRequestRepository.GetDepositRequestsAsync();
            List<DepositRequestDTO> depositRequestModel = this.mapper.Map<List<DepositRequestDTO>>(data);
            return depositRequestModel;
        }

        public async Task<BankDetails> GetBankDetails()
        {
            var data = await this.bankDetailRepository.GetAllAsync();
            return data.OrderByDescending(x => x.Id).FirstOrDefault();
        }

        public async Task<List<DepositRequestDTO>> GetUserDepositRequests(int userId)
        {
            var data = await this.depositRequestRepository.GetDepositRequestsByUserIdAsync(userId);
            List<DepositRequestDTO> depositRequestModel = this.mapper.Map<List<DepositRequestDTO>>(data);
            return depositRequestModel;
        }

        public async Task<DepositRequestDTO> UpdateDepositRequest(DepositRequestDTO request)
        {
            var data = await this.depositRequestRepository.GetByIdAsync((int)request.Id);
            data.Status = request.Status;
            var updateRquest = await this.depositRequestRepository.UpdateAsync(data);
            if (request.Status == "Approved")
            {
                var getUserAccount = await this.userAccountService.GetUserAccountById(updateRquest.UserId);
                if (getUserAccount != null)
                {
                    getUserAccount.BalanceAmount = (int?)(getUserAccount.BalanceAmount + updateRquest.Amount);
                    var updateUserAccounts = await this.userAccountService.UpdateUserAccount(getUserAccount);
                }
            }

            DepositRequestDTO depositData = this.mapper.Map<DepositRequestDTO>(updateRquest);
            return depositData;
        }
    }
}
