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
    internal class UserKYCConfiguration : IEntityTypeConfiguration<UserKYC>
    {
        public void Configure(EntityTypeBuilder<UserKYC> builder)
        {
            builder.ToTable("UserKYC");
            builder.HasKey(x => x.Id);
        }
    }
}
