﻿using Diversity.Infrastructure.SharedRepositories;
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
