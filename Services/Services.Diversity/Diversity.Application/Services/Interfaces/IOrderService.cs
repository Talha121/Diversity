using Diversity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diversity.Application.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrder(int UserId);
        Task<List<Order>> GetOrdersByUserId(int userId);
        Task<Order> GetUserCurrentOrder(int userId);
        Task<Order> CompleteOrder(int OrderId);
    }
}
