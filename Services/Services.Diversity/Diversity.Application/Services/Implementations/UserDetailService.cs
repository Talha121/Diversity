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
            userData.IsActive= true;
            userData.Email.ToLower();
            var response=await this.userDetailRepository.AddAsync(userData);
            UserDetailDTO data = this.mapper.Map<UserDetailDTO>(response);
            return data;
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
            try
            {
                var data = await this.userDetailRepository.GetAllAsync();
                List<UserDetailDTO> users = this.mapper.Map<List<UserDetailDTO>>(data);
                return users;
            }
            catch (Exception ex)
            {

                throw;
            }
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
            var data =await  this.userDetailRepository.GetByIdAsync((int)userId);
            return this.mapper.Map<UserDetailDTO>(data);
        }

        public async Task<UserDetailDTO> UpdateUserProfile(UserDetailDTO userDetail)
        {
            try
            {
                var data = await this.userDetailRepository.GetByIdAsync((int)userDetail.Id);
                if (userDetail.ProfileImage != null)
                {
                    var fileName =await this.fileService.UploadedFile(userDetail.ProfileImage, "UserProfile");
                    data.ImageUrl = "Images/UserProfile/" + fileName;
                }
                data.Name=userDetail.Name;
                data.PhoneNumber = userDetail.PhoneNumber;

                var update =await this.userDetailRepository.UpdateAsync(data);

                UserDetailDTO response = this.mapper.Map<UserDetailDTO>(update);
                return response;
            }
            catch (Exception ex)
            {

                throw;
            }
            
            
        }
    }
}
