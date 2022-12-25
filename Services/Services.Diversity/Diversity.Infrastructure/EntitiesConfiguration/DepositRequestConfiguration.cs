using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Diversity.Domain.Entities;

namespace Diversity.Infrastructure.EntitiesConfiguration
{
    internal class DepositRequestConfiguration : IEntityTypeConfiguration<DepositRequest>
    {
        public void Configure(EntityTypeBuilder<DepositRequest> builder)
        {
            builder.ToTable("DepositRequest");
            builder.HasKey(x => x.Id);
            builder.HasOne(d => d.User)
                .WithMany(s => s.DepositRequests)
                .HasForeignKey(d => d.UserId);
        }
    }
}
