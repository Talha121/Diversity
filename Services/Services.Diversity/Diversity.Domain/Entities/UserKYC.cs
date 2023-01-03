using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diversity.Domain.Entities
{
    public class UserKYC
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string IdentityType { get; set; }
        public string DocumentNumber { get; set; }
        public string DocumentImageOne { get; set; }
        public string DocumentImageTwo { get; set; }
        public string Status { get; set; }
        [ForeignKey("UserId")]
        public virtual UserDetail User { get; set; }
    }
}
