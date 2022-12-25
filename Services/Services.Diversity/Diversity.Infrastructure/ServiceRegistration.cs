using Diversity.Infrastructure.Repositories.Implementation;
using Diversity.Infrastructure.Repositories.Interfaces;
using Diversity.Infrastructure.SharedRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Diversity.Infrastructure
{
    public static class ServiceRegistration
    {

        public static void AddInfrastructureLayer(this IServiceCollection service)
        {
            service.AddScoped<DbContext, DataContext>();
            service.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            service.AddScoped<IDepositRequestsRepository, DepositRequestRepository>();
            service.AddScoped<IWithdrawRequestRepository, WithdrawRequestRepository>();
            //service.AddScoped<IOrderRepository, OrderRepository>();
            //service.AddScoped<IProductImageRepository, ProductImageRepository>();
            //service.AddScoped<IProductRepository, ProductRepository>();
            //service.AddScoped<IUserAccountRepository, UserAccountRepository>();
            service.AddScoped<IUserDetailRepository, UserDetailRepository>();
        }
        public static void ConfigureDbContext(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<DataContext>(option =>
            {
                option.EnableDetailedErrors(true);
                option.UseSqlServer(configuration.GetConnectionString("DiversityContext"),
                    providerOptions =>
                    {
                        providerOptions.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(10), errorNumbersToAdd: null);
                    });
            });
        }
    }
}
