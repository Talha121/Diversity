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
    public class UserKYCService:IUserKYCService
    {
        private readonly IUserKYCRepository userKYCRepository;
        private readonly IFileService fileService;
        private readonly IMapper mapper;
        public UserKYCService(IUserKYCRepository userKYCRepository, IFileService fileService, IMapper mapper)
        {
            this.userKYCRepository = userKYCRepository;
            this.fileService = fileService;
            this.mapper = mapper;
        }

        public async Task<bool> CreateKYC(UserKYCDTO dto)
        {
            try
            {
                if(dto.Id != null)
                {
                    var data = await this.userKYCRepository.GetKYCById((int)dto.Id);
                    if(data.DocumentImageOne != null)
                    {
                        var delete = await this.fileService.DeleteFile(data.DocumentImageOnePublicId);
                    }
                    if (data.DocumentImageTwo != null)
                    {
                        var delete = await this.fileService.DeleteFile(data.DocumentImageTwoPublicId);
                    }
                    if (dto.DocumentImageOneFile != null)
                    {
                        var fileObj = await this.fileService.UploadedFile(dto.DocumentImageOneFile);
                        dto.DocumentImageOne = fileObj.Url;
                        dto.DocumentImageOnePublicId = fileObj.PublicId;
                    }
                    if (dto.DocumentImageTwoFile != null)
                    {
                        var fileObj = await this.fileService.UploadedFile(dto.DocumentImageTwoFile);
                        dto.DocumentImageTwo = fileObj.Url;
                        dto.DocumentImageTwoPublicId = fileObj.PublicId;
                    }
                    var mappedData = this.mapper.Map<UserKYC>(dto);
                    var update = await this.userKYCRepository.UpdateAsync(mappedData);
                    return true;

                }
                else
                {
                    if (dto.DocumentImageOneFile != null)
                    {
                        var fileObj = await this.fileService.UploadedFile(dto.DocumentImageOneFile);
                        dto.DocumentImageOne = fileObj.Url;
                        dto.DocumentImageOnePublicId=fileObj.PublicId;
                    }
                    if (dto.DocumentImageTwoFile != null)
                    {
                        var fileObj = await this.fileService.UploadedFile(dto.DocumentImageTwoFile);
                        dto.DocumentImageTwo = fileObj.Url;
                        dto.DocumentImageTwoPublicId = fileObj.PublicId;
                    }
                    var data = this.mapper.Map<UserKYC>(dto);
                    var insert = await this.userKYCRepository.AddAsync(data);
                    return true;
                }
              
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<UserKYCDTO>> GetAllUsersKYC()
        {
            var data=await this.userKYCRepository.GetAllAsync();
            var mappedData = this.mapper.Map<List<UserKYCDTO>>(data);
            return mappedData;
        }

        public async Task<UserKYCDTO> GetUserKYC(int userId)
        {
            var data= await this.userKYCRepository.GetByUser(userId);
            var mappedData = this.mapper.Map<UserKYCDTO>(data);
            return mappedData;
        }

        public async Task<bool> UpdateStatus(UserKYCDTO dto)
        {
            var data = await this.userKYCRepository.GetByIdAsync((int)dto.Id);
            data.Status = dto.Status;
            var update=await this.userKYCRepository.UpdateAsync(data);
            return true;
        }
    }
}
