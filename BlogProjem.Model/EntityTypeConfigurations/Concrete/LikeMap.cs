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
    // baseMapten kalıtım aalamıyor bu yüzden baseMapte olduğu gibi IEntityTypeConfigurationdan klaıtım alacak.
    public class LikeMap : IEntityTypeConfiguration<Like>
    {
        public void Configure(EntityTypeBuilder<Like> builder)
        {
            builder.HasKey(a => new { a.AppUserId, a.ArticleId });
            // composite key gibi birlikteliklerinin eşsiz olması gerekiyor.
        }
    }
}
