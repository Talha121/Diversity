using Diversity.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diversity.Application.Models;
using CloudinaryDotNet;
using Microsoft.Extensions.Options;
using CloudinaryDotNet.Actions;

namespace Diversity.Application.Services.Implementations
{
    public class FileService:IFileService
    {
        private readonly IHostingEnvironment webHostEnvironment;
        private readonly Cloudinary cloudinary;
        public FileService(IHostingEnvironment webHostEnvironment,IOptions<CloudinarySettings> config)
        {
            this.webHostEnvironment = webHostEnvironment;
            var acc = new Account(config.Value.CloudName, config.Value.ApiKey, config.Value.ApiSecret);
            this.cloudinary= new Cloudinary(acc);
        }

        public async Task<bool> DeleteFile(string publicId)
        {
            var param= new DeletionParams(publicId);
            var isDelete = await this.cloudinary.DestroyAsync(param);
            return true;
        }

        public async Task<FileUploadedData> UploadedFile(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();

            if (file != null)
            {
                using var stream=file.OpenReadStream();
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, stream),

                };
                uploadResult = await cloudinary.UploadAsync(uploadParams);
            }
            var data = new FileUploadedData()
            {
                PublicId = uploadResult.PublicId,
                Url = uploadResult.Url.ToString(),
            };
            return data;
        }
    }
}
