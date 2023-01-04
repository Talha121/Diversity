using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diversity.Application.Models
{
    public class OrderDTO
    {
        public int? Id { get; set; }
        public int? UserId { get; set; }
        public int ProductId { get; set; }
        public string OrderStatus { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public Guid OrderId { get; set; }
        public ProductDTO Products { get; set; }
    }
}
