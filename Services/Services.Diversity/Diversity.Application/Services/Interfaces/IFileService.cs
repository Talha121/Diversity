using Diversity.Application.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diversity.Application.Services.Interfaces
{
    public interface IFileService
    {
        Task<FileUploadedData> UploadedFile(IFormFile file);
        Task<bool> DeleteFile(string publicId);
    }
}
