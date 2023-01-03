using Diversity.Application.Services.Interfaces;
using Diversity.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diversity.Application.Services.Implementations
{
    public class UserKYCService:IUserKYCService
    {
        private readonly IUserKYCRepository userKYCRepository;
        public UserKYCService(IUserKYCRepository userKYCRepository)
        {
            this.userKYCRepository = userKYCRepository;
        }

        public Task<bool> CreateKYC()
        {
            throw new NotImplementedException();
        }
    }
}
