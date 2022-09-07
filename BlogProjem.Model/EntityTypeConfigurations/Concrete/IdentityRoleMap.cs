using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjem.Model.EntityTypeConfigurations.Concrete
{
    public class IdentityRoleMap : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            //  seed Data eklmeiş olacağız.
            // göç başlattıktan sonra burdan ekleyeceğimiz Role de sql tarafıda eklenmiş olacak.Kayıtlı kullanıcıyı biz oluştururken rolünün bu olduğunu söyleyeceğiz.

            // kayıtlı kullanıcı için Member rolünü eklemiş olduk.
            builder.HasData(new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Member", NormalizedName = "MEMBER" });
            builder.HasData(new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Admin", NormalizedName = "ADMİN" });
            // admin satırı add-migration ve update-database yapıldıktan sonra silinme otomatik atmin rolü eklenmiş olacak sadece adminguild id yi 
            // bi kullanıcıya verebiliriz onuda sql de manuel olarak biz yapıcaz 
        }
    }
}
