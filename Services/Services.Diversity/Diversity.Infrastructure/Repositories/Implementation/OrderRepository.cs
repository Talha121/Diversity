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
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(DataContext context) : base(context)
        {
        }

        public async Task<Order> CompleteOrder(int OrderId)
        {
            var order=await GetByIdAsync(OrderId);
            order.OrderStatus = "Completed";
            await UpdateAsync(order);
            return order;
        }

        public async Task<List<Order>> GetOrdersByUserId(int userId)
        {
            return await this.DataContext.Set<Order>().Where(x => x.UserId == userId).Include(x=>x.Products).AsNoTracking().ToListAsync();
        }

        public async Task<Order> GetUserCurrentOrder(int userId)
        {
            return await this.DataContext.Set<Order>().Where(x => x.UserId == userId && x.OrderStatus =="Pending").Include(x=>x.Products).ThenInclude(x=>x.ProductImages).AsNoTracking().FirstOrDefaultAsync();
        }
    }
}
