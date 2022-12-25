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
        Task<string> UploadedFile(IFormFile file, string folderName);
    }
}
