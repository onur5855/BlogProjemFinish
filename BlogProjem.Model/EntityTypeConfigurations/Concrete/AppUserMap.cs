using BlogProjem.Model.Entities.Concrete;
using BlogProjem.Model.EntityTypeConfigurations.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjem.Model.EntityTypeConfigurations.Concrete
{
    public class AppUserMap:BaseMap<AppUser>
    {
        public override void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(a => a.FirstName).IsRequired(true);
            builder.Property(a => a.LastName).HasMaxLength(45).IsRequired(true);
            builder.Property(a => a.UserName).IsRequired(true);
            builder.Property(a => a.Password).IsRequired(true);
            builder.Property(a => a.ImagePath).IsRequired(true); // fotoğrafsız kullanıcı olamaz

           

            base.Configure(builder);
        }

    }
}
