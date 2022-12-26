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
    public class UserAccountService:IUserAccountService
    {
        private readonly IUserAccountRepository userAccountRepository;
        private readonly IMapper mapper;
        public UserAccountService(IUserAccountRepository userAccountRepository,IMapper mapper)
        {
            this.userAccountRepository = userAccountRepository;
            this.mapper = mapper;
        }

        public async Task<UserAccountDTO> CreateUserAccount(UserAccountDTO userAccount)
        {
            UserAccount account=this.mapper.Map<UserAccount>(userAccount);
            var response =await this.userAccountRepository.AddAsync(account);
            return this.mapper.Map<UserAccountDTO>(response);
        }

        public async Task<UserAccountDTO> GetUserAccountById(int userId)
        {
            var data = await this.userAccountRepository.GetByUserIdAsync(userId);
            return this.mapper.Map<UserAccountDTO>(data);
        }

        public async Task<List<UserAccountDTO>> GetUserAccountList()
        {
            var data = await this.userAccountRepository.GetAllUserAccount();
            return this.mapper.Map<List<UserAccountDTO>>(data);
        }

        public async Task<UserAccountDTO> UpdateUserAccount(UserAccountDTO userAccount)
        {
            UserAccount account = this.mapper.Map<UserAccount>(userAccount);
            var response=await this.userAccountRepository.UpdateAsync(account);
            return this.mapper.Map<UserAccountDTO>(response) ;
        }
    }
}
