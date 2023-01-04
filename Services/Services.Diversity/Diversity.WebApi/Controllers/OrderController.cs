﻿using Diversity.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Diversity.Application.Models;
using Diversity.Domain.Entities;
using System.Security.Claims;

namespace Diversity.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;
        private readonly IUserAccountService userAccountService;
        private readonly IUserKYCService userKYCService;
        private readonly IProductService productService;
        public OrderController(IOrderService orderService, IUserAccountService userAccountService, IProductService productService, IUserKYCService userKYCService)
        {
            this.orderService = orderService;
            this.userAccountService = userAccountService;
            this.productService = productService;
            this.userKYCService = userKYCService;
        }
        [HttpGet("GetOrdersByUserId", Name = "GetOrdersByUserId")]
        public async Task<IActionResult> GetOrdersByUserId()
        {
            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var data = await this.orderService.GetOrdersByUserId(int.Parse(userId));
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetAllOrders", Name = "GetAllOrders")]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                var data = await this.orderService.GetAllOrders();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetUserCurrentOrder", Name = "GetUserCurrentOrder")]
        public async Task<IActionResult> GetUserCurrentOrder()
        {
            try
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var getUserKYCInfo = await this.userKYCService.GetUserKYC(int.Parse(userId));
                if (getUserKYCInfo != null)
                {
                    if (getUserKYCInfo.Status != "Approved")
                    {
                        return Ok(new { res = "Kyc Not Approved." });
                    }
                }
                else
                {
                    return Ok(new { res = "Kyc Not Approved." });
                }
                var createOrder = await this.orderService.CreateOrder(int.Parse(userId));

                var currentOrders = await this.orderService.GetUserCurrentOrder(int.Parse(userId));
                var totalOrder = await this.productService.GetAllProducts();
                var data = new
                {
                    order = currentOrders,
                    totalCount = totalOrder.Count,
                };
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("CompleteOrder", Name = "CompleteOrder")]
        public async Task<IActionResult> CompleteOrder(int orderId)
        {
            try
            {
                var userId = int.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
                var data = await this.orderService.CompleteOrder(orderId);
                var createNewOrder = await this.orderService.CreateOrder(userId);
                var getUserAccount = await this.userAccountService.GetUserAccountById(userId);
                var userProduct = await this.productService.GetProductById(data.ProductId);
                if (getUserAccount != null)
                {
                    UserAccountDTO acc = new UserAccountDTO()
                    {
                        Id = getUserAccount.Id,
                        UserId = userId,
                        BalanceAmount = getUserAccount.BalanceAmount + userProduct.Commission,
                        TotalCommission = getUserAccount.TotalCommission + userProduct.Commission,
                    };
                    var updateUserAccounts = await this.userAccountService.UpdateUserAccount(acc);
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
