using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diversity.Domain.Entities
{
    public class WithdrawRequest
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public string AccountNumber { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }
        [ForeignKey("UserId")]
        public virtual UserDetail User { get; set; }

    }
}
