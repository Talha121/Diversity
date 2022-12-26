using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diversity.Application.Models
{
    public class GetProductDTO
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? Amount { get; set; }
        public int? OrderNum { get; set; }
        public int? EstimatedReturn { get; set; }
        public int? Quantity { get; set; }
        public int? Commission { get; set; }
        public bool IsActive { get; set; }
        public List<ProductImageDTO> productImages { get; set; }
    }
}
