using Diversity.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Diversity.Application.Models;

namespace Diversity.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;
        private readonly IUserAccountService userAccountService;
        private readonly IProductService productService;
        public OrderController(IOrderService orderService, IUserAccountService userAccountService, IProductService productService)
        {
            this.orderService = orderService;
            this.userAccountService = userAccountService;
            this.productService = productService;

        }
        [HttpGet("GetOrdersByUserId", Name = "GetOrdersByUserId")]
        public async Task<IActionResult> GetOrdersByUserId()
        {
            try
            {
                var data = await this.orderService.GetOrdersByUserId(12);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetUserCurrentOrder", Name = "GetUserCurrentOrder")]
        public async Task<IActionResult> GetUserCurrentOrder(int productId)
        {
            try
            {
                var data = await this.orderService.GetUserCurrentOrder(12, productId);
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
                var userId = 18;
                var data = await this.orderService.CompleteOrder(orderId);
                var createNewOrder = await this.orderService.CreateOrder(userId);
                var getUserAccount =await this.userAccountService.GetUserAccountById(userId);
                var userProduct = await this.productService.GetProductById(data.ProductId);
                if (getUserAccount != null)
                {
                    UserAccountDTO acc = new UserAccountDTO()
                    {
                        UserId = userId,
                        BalanceAmount =getUserAccount.BalanceAmount-userProduct.Amount,
                        TotalCommission=getUserAccount.TotalCommission+userProduct.Commission,
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
