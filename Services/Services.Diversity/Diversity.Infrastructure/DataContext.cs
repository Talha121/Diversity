using Diversity.Domain.Entities;
using Diversity.Infrastructure.EntitiesConfiguration;
using Diversity.Infrastructure.SharedRepositories;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.Infrastructure;

namespace Diversity.Infrastructure
{
    public class DataContext:GenericContext
    {
        public DataContext(DbContextOptions options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DepositRequestConfiguration());
            modelBuilder.ApplyConfiguration(new WithdrawRequestConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductImageConfiguration());
            modelBuilder.ApplyConfiguration(new UserAccountConfiguration());
            modelBuilder.ApplyConfiguration(new UserDetailConfiguration());

        }

        public DbSet<DepositRequest> DepositRequests { get; set; }
        public DbSet<WithdrawRequest> WithdrawRequests { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }
    }
}
