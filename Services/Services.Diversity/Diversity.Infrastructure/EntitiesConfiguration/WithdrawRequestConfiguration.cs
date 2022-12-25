using Diversity.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diversity.Infrastructure.EntitiesConfiguration
{
    internal class WithdrawRequestConfiguration : IEntityTypeConfiguration<WithdrawRequest>
    {
        public void Configure(EntityTypeBuilder<WithdrawRequest> builder)
        {
            builder.ToTable("WithdrawRequest");
            builder.HasKey(x => x.Id);
            builder.HasOne(d => d.User)
                .WithMany(s => s.WithdrawRequests)
                .HasForeignKey(d => d.UserId);
        }
    }
}
