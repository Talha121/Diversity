using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diversity.Application.Models
{
    public class UserKYCDTO
    {
        public int? Id { get; set; }
        public int? UserId { get; set; }
        public string IdentityType { get; set; }
        public string DocumentNumber { get; set; }
        public string DocumentImageOne { get; set; }
        public string DocumentImageTwo { get; set; }
        public IFormFile DocumentImageOneFile { get; set; }
        public IFormFile DocumentImageTwoFile { get; set; }
        public string DocumentImageOnePublicId { get; set; }
        public string DocumentImageTwoPublicId { get; set; }
        public string Status { get; set; }
    }
}
