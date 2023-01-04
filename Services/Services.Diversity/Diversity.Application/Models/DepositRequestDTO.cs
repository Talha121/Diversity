using Diversity.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diversity.Application.Models
{
    public class DepositRequestDTO
    {
        public int? Id { get; set; }
        public int? UserId { get; set; }
        public float Amount { get; set; }
        public string Type { get; set; }
        public string ProofPath { get; set; }
        public string PublicId { get; set; }
        public string OtherDetails { get; set; }
        public string Status { get; set; }
        public string UserName { get; set; }
        public IFormFile ProofFile { get; set; }
    }
}
