using Diversity.Application.Models;
using Diversity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diversity.Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<bool> CreateProduct(ProductDTO product);
        Task<List<GetProductDTO>> GetAllProducts();
        Task<GetProductDTO> GetProductById(int id);
        Task<bool> UpdateProduct(ProductDTO product);
        Task<bool> ChangeProductStatus(int productId, bool IsActive);
    }
}
