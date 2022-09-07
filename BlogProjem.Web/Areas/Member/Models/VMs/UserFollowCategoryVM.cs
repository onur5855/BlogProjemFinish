using BlogProjem.Model.Entities.Concrete;
using BlogProjem.Web.Areas.Member.Models.DTOs;

namespace BlogProjem.Web.Areas.Member.Models.VMs
{
    public class UserFollowCategoryVM
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //public int AppUserID { get; set; }
        //public AppUser AppUser { get; set; }
        public List<UserFollowCategory> UserFollowCategories { get; set; }



    }
}
