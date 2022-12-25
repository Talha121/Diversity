using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diversity.Domain.Entities
{
    public class UserDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; }

        public virtual List<DepositRequest> DepositRequests { get; set; }
        public virtual List<WithdrawRequest> WithdrawRequests { get; set; }
        public virtual List<UserAccount> UserAccounts  { get; set; }
        public virtual List<Order> Orders { get; set; }


    }
}
