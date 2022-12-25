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
    public class UserDetailService:IUserDetailService
    {
        private readonly IUserDetailRepository userDetailRepository;
        private readonly IMapper mapper;
        private readonly IFileService fileService;
        public UserDetailService(IUserDetailRepository userDetailRepository,IMapper mapper,IFileService fileService)
        {
            this.userDetailRepository = userDetailRepository;
            this.mapper = mapper;   
            this.fileService = fileService;
        }

        public async Task<UserDetailDTO> CreateUser(UserDetailDTO userDetail)
        {
            var userData = this.mapper.Map<UserDetail>(userDetail);
            userData.Role = "User";
            var response=this.userDetailRepository.AddAsync(userData);
            return this.mapper.Map<UserDetailDTO>(response);
        }

        public async Task<UserDetailDTO> EnableDisableUser(int? userId, bool isActive)
        {
            var user = await this.userDetailRepository.GetByIdAsync((int)userId);
            user.IsActive = isActive;
            var update = await this.userDetailRepository.UpdateAsync(user);
            return this.mapper.Map<UserDetailDTO>(update);
        }

        public async Task<List<UserDetailDTO>> GetAllUsers()
        {
            var data = this.userDetailRepository.GetAllAsync();
            List<UserDetailDTO> users = this.mapper.Map<List<UserDetailDTO>>(data);
            return users;
        }

        public async Task<UserDetailDTO> GetUserByEmail(string email)
        {
            var check = await this.userDetailRepository.GetByEmail(email);
            if(check != null)
            {
                return this.mapper.Map<UserDetailDTO>(check);
            }
            return null;
        }

        public async Task<UserDetailDTO> GetUserProfile(int? userId)
        {
            var data = this.userDetailRepository.GetByIdAsync((int)userId);
            return this.mapper.Map<UserDetailDTO>(data);
        }

        public async Task<UserDetailDTO> UpdateUserProfile(UserDetailDTO userDetail)
        {
            var data = await this.userDetailRepository.GetByIdAsync((int)userDetail.Id);
            if (userDetail.ProfileImage !=null)
            {
                var fileName = this.fileService.UploadedFile(userDetail.ProfileImage, "UserProfile");
                userDetail.ImageUrl = "Images/UserProfile/" + fileName;
            }
            else
            {
                userDetail.ImageUrl = data.ImageUrl;
            }

            UserDetail detail = this.mapper.Map<UserDetail>(userDetail);
            var update=this.userDetailRepository.UpdateAsync(detail);
            UserDetailDTO response=this.mapper.Map<UserDetailDTO>(update);
            return response;
            
            
        }
    }
}
