using Diversity.Domain.Entities;
using Diversity.Infrastructure.SharedRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diversity.Infrastructure.Repositories.Interfaces
{
    public interface IProductImageRepository:IGenericRepository<ProductImage>
    {
        Task<bool> AddAllImageAsync(List<ProductImage> image);
        Task<bool> DeleteImagesByProductId(int productId);  
    }
}
