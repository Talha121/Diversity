using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diversity.Application.Models
{
    public class BankDetailDTO
    {
        public IFormFile file { get; set; }
        public string AccountNumber { get; set; }
        public string AccountTitle { get; set; }
    }
}
