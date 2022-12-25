using Diversity.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diversity.Application.Services.Implementations
{
    public class FileService:IFileService
    {
        private readonly IHostingEnvironment webHostEnvironment;
        public FileService(IHostingEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }
        public async Task<string> UploadedFile(IFormFile file,string folderName)
        {
            string uniqueFileName = null;

            if (file != null)
            {
                string uploadsFolder = Path.Combine(this.webHostEnvironment.WebRootPath, "Images/"+folderName+"/");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                //uniqueFileName = file.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
