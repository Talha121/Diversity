using Diversity.Domain.Entities;
using Diversity.Infrastructure.Repositories.Interfaces;
using Diversity.Infrastructure.SharedRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Diversity.Infrastructure.Repositories.Implementation
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(DataContext context) : base(context)
        {
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await this.DataContext.Set<Product>().Include(x => x.ProductImages).ToListAsync();
        }

        public Task<Product> GetProductById(int productId)
        {
            return this.DataContext.Set<Product>().Where(x=>x.Id==productId).Include(x => x.ProductImages).FirstOrDefaultAsync();
        }
    }
}
