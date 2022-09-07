using BlogProjem.Model.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjem.Model.EntityTypeConfigurations.Concrete
{
    public class UserPasswordMap : IEntityTypeConfiguration<UserPassword>
    {
        public void Configure(EntityTypeBuilder<UserPassword> builder)
        {
            builder.HasOne(a => a.AppUser).WithMany(a => a.userPasswords).HasForeignKey(a => a.AppUserId);
        }
    }
}
