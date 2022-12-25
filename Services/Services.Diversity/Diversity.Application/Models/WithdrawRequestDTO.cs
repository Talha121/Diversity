using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diversity.Application.Models
{
    public class WithdrawRequestDTO
    {
        public int? Id { get; set; }
        public double Amount { get; set; }
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public string Status { get; set; }
    }
}
