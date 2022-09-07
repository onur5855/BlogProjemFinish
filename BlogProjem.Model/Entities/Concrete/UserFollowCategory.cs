using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjem.Model.Entities.Concrete
{
    public class UserFollowCategory
    {
        // composite key => primary key + foreign key
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }


    }
}
