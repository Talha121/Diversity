using Diversity.Application.Models;
using Diversity.Application.Services.Implementations;
using Diversity.Application.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Reflection;

namespace Diversity.Application
{
    public static class ServiceRegistration
    {
        public static void AddAplicationLayer(this IServiceCollection service)
        {
            service.AddAutoMapper(Assembly.GetExecutingAssembly());
            service.AddScoped<IDepositRequestService, DepositRequestService>();
            service.AddScoped<IFileService, FileService>();
            service.AddScoped<IWithdrawRequestService, WithdrawRequestService>();
            service.AddScoped<IOrderService, OrderService>();
            service.AddScoped<IProductService, ProductService>();
            service.AddScoped<IUserDetailService, UserDetailService>();
            service.AddScoped<IUserAccountService, UserAccountService>();
            service.AddScoped<IUserKYCService, UserKYCService>();
        }
    }
}
