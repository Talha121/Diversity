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
    internal class BankDetailConfiguration : IEntityTypeConfiguration<BankDetails>
    {
        public void Configure(EntityTypeBuilder<BankDetails> builder)
        {
            builder.ToTable("BankDetails");
            builder.HasKey(x => x.Id);
        }
    }
}
