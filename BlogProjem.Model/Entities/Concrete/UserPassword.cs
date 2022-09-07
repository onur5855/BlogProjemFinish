using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjem.Model.Entities.Concrete
{
    public class UserPassword
    {
        public int ID { get; set; }
        public DateTime CreateTime { get; set; }
        public string pw { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
