using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjem.Model.Entities.Concrete
{
    public class Like
    {
        // baseEntityden kalıtım almamaktadır çünkü her satırın eşsiz olması gerekir ki sql tarafından verilen id bu durumu bozar.

        // like a ait crud operasyonlarının tümü de geçerli değildir.

        // like kime ait ?
        public int AppUserId { get; set; }

        public AppUser AppUser { get; set; }


        // Like hangi makaleye ait?
        public int ArticleId { get; set; }
        public Article Article { get; set; }

    }
}
