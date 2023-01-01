using AutoMapper;
using Diversity.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System;
using System.Security.Claims;
using System.Linq;

namespace Diversity.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IUserDetailService userDetailService;
        private readonly IUserAccountService userAccountService;
        private readonly IOrderService orderService;
        private readonly IProductService productService;
        private readonly IDepositRequestService depositRequestService;
        private readonly IWithdrawRequestService withdrawRequestService;
        public readonly IConfiguration config;
        public readonly IMapper mapper;
        public DashboardController(IUserDetailService userDetailService,
                IConfiguration config, 
                IMapper mapper, 
                IUserAccountService userAccountService, 
                IOrderService orderService, 
                IProductService productService,
                IDepositRequestService depositRequestService,
                IWithdrawRequestService withdrawRequestService)
        {
            this.userDetailService = userDetailService;
            this.config = config;
            this.mapper = mapper;
            this.userAccountService = userAccountService;
            this.orderService = orderService;
            this.productService = productService;
            this.depositRequestService = depositRequestService;
            this.withdrawRequestService = withdrawRequestService;
        }
        [HttpGet("UserDashboard")]
        public async Task<IActionResult> GetDashBoardData()
        {
            try
            {
                var userId = int.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
                var totalOrder = await this.orderService.GetOrdersByUserId(userId);
                var totaOrderCount = totalOrder.Where(x => x.OrderStatus == "Completed").Count();
                var getAccountInformation = await this.userAccountService.GetUserAccountById(userId);
                var getcurrentOrder = await this.orderService.GetUserCurrentOrder(userId);
                if (getcurrentOrder != null)
                {
                    if (getAccountInformation.BalanceAmount < getcurrentOrder.Products.Amount)
                    {
                        getAccountInformation.RechargeAmount = getcurrentOrder.Products.Amount - getAccountInformation.BalanceAmount;
                    }
                    else
                    {
                        getAccountInformation.RechargeAmount = 0;
                    }
                }
                else
                {
                    getAccountInformation.RechargeAmount = 0;
                }
                var data = new
                {
                    CompletedOrderCount=totaOrderCount,
                    UserAccountDetails= getAccountInformation
                };
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("AdminDashboard")]
        public async Task<IActionResult> AdminDashboard()
        {
            try
            {

                var users = await this.userDetailService.GetAllUsers();
                var orders=await this.orderService.GetAllOrders();
                var products = await this.productService.GetAllProducts();
                var deposits = await this.depositRequestService.GetAllDepositRequests();
                var withdraw = await this.withdrawRequestService.GetAllWithdrawRequests();
                //var userAccounts=await this.userAccountService.GetUserAccountList();
                var data = new
                {
                    totalUsers= users.Where(x=>x.Role=="User").Count(),
                    totalOrders=orders.Count,
                    pendingOrder=orders.Where(x=>x.OrderStatus=="Pending").Count(),
                    completedOrders=orders.Where(x=>x.OrderStatus =="Completed").Count(),
                    totalProducts= products.Count,
                    pendingDeposits=deposits.Where(x=>x.Status == "Pending").Count(),
                    pendingWithdraws=withdraw.Where(x=>x.Status == "Pending").Count(),
                    currentSale=deposits.Where(x=>x.Status == "Approved" +
                    "").Sum(x=>x.Amount)

                };
                return Ok(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
