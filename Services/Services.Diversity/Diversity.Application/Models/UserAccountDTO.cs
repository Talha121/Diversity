using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diversity.Application.Models
{
    public class UserAccountDTO
    {
        public int? Id { get; set; }
        public int? BalanceAmount { get; set; }
        public int? RechargeAmount { get; set; }
        public int? TotalCommission { get; set; }
        public int? TotalWithdraw { get; set; }
        public int? TotalDeposit { get; set; }
        public int? UserId { get; set; }
        public string UserName { get; set; }
    }
}
