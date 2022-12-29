using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diversity.Domain.Entities
{
    public class UserAccount
    {
        public int Id { get; set; }
        public double? BalanceAmount { get; set; }
        public double? RechargeAmount { get; set; }
        public double? TotalCommission { get; set; }
        public double? TotalWithdraw { get; set; }
        public double? TotalDeposit { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual UserDetail User { get; set; }
    }
}
