﻿using Diversity.Application.Services.Implementations;
using Diversity.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Diversity.Application
{
    public static class ServiceRegistration
    {
        public static void AddAplicationLayer(this IServiceCollection service)
        {
            service.AddAutoMapper(Assembly.GetExecutingAssembly());
            service.AddScoped<IDepositRequestService, DepositRequestService>();
            service.AddScoped<IFileService, FileService>();
        }
    }
}
