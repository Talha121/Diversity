using Diversity.Domain.Entities;
using Diversity.Infrastructure.Repositories.Interfaces;
using Diversity.Infrastructure.SharedRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diversity.Infrastructure.Repositories.Implementation
{
    public class ProductImageRepository : GenericRepository<ProductImage>, IProductImageRepository
    {
        public ProductImageRepository(DataContext context) : base(context)
        {
        }

        public async Task<bool> AddAllImageAsync(List<ProductImage> image)
        {
             await this.DataContext.Set<ProductImage>().AddRangeAsync(image);
            return true;
        }

        public async Task<bool> DeleteImagesByProductId(int productId)
        {
            var data = await this.DataContext.Set<ProductImage>().Where(x => x.ProductId == productId).AsNoTracking().ToListAsync();
            if(data.Count > 0)
            {
                this.DataContext.Set<ProductImage>().RemoveRange(data);
            }
            return true;
        }
    }
}
