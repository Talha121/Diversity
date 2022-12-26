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
    
    internal class UserAccountConfiguration : IEntityTypeConfiguration<UserAccount>
    {
        public void Configure(EntityTypeBuilder<UserAccount> builder)
        {
            builder.ToTable("UserAccount");
            builder.HasKey(x => x.Id);
            builder.HasOne(d => d.User)
                .WithMany(s => s.UserAccounts)
                .HasForeignKey(d => d.UserId);
        }
    }
}
