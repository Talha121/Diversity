﻿using Diversity.Application.Services.Interfaces;
using Diversity.Domain.Entities;
using Diversity.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diversity.Application.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IProductService productService;
        private readonly IOrderRepository orderRepository;
        public OrderService(IProductService productService, IOrderRepository orderRepository)
        {
            this.productService = productService;
            this.orderRepository = orderRepository;
        }

        public async Task<Order> CompleteOrder(int OrderId)
        {

            return await this.orderRepository.CompleteOrder(OrderId);
        }

        public async Task<Order> CreateOrder(int userId)
        {
            var orders = await this.orderRepository.GetOrdersByUserId(userId);
            if (orders == null||orders.Count==0)
            {
                var products = await this.productService.GetAllProducts();
                Order orderData = new Order()
                {
                    UserId = userId,
                    ProductId = (int)products.Where(x => x.IsActive &&x.OrderNum!=null).OrderBy(x => x.OrderNum).Select(x => x.Id).FirstOrDefault(),
                    OrderStatus = "Pending",
                    OrderId = Guid.NewGuid()
                };
                var orderResponse = await this.orderRepository.AddAsync(orderData);
                return orderResponse;
            }
            else
            {
                var currentProductId = orders.OrderByDescending(x => x.Id).FirstOrDefault();
                var product = await this.productService.GetProductById(currentProductId.ProductId);
                var productList = await this.productService.GetAllProducts();

                var nextProduct = productList.Where(x => x.IsActive == true && x.OrderNum > product.OrderNum).OrderBy(x => x.OrderNum).FirstOrDefault();
                Order orderData = new Order()
                {
                    UserId = userId,
                    ProductId = (int)nextProduct.Id,
                    OrderStatus = "Pending",
                    OrderId= Guid.NewGuid()
                };
                var orderResponse=await this.orderRepository.AddAsync(orderData);
                return orderResponse;
            }
        }

        public async Task<List<Order>> GetOrdersByUserId(int userId)
        {
            return await this.orderRepository.GetOrdersByUserId(userId);
        }

        public async Task<Order> GetUserCurrentOrder(int userId, int id)
        {
            return await this.orderRepository.GetUserCurrentOrder(userId, id);
        }
    }
}
