using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diversity.Domain.Entities
{
    public class BankDetails
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public string PublicId { get; set; }
        public string AccountTitle { get; set; }
        public string AccountNumber { get; set; }
    }
}
