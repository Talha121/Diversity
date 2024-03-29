﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Diversity.Infrastructure.SharedRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }

    public class GenericContext : DbContext, IUnitOfWork
    {
        public GenericContext(DbContextOptions options) : base(options)
        {
        }
    }
}
