using BlogProjem.Model.Entities.Concrete;
using BlogProjem.Web.Areas.Member.Models.DTOs;

namespace BlogProjem.Web.Areas.Member.Models.VMs
{
    public class GetArticleVm
    {
        //list actionında kullanacagım propları toparlamak amacıyla kullandım
        public int ArticleID { get; set; }//Actionlinlerde kullanacagımız update-delet-datail vb.

        public string Title { get; set; }

        public string Content { get; set; }

        public string ImagePath { get; set; }

        public string UserFullName { get; set; }//appUser

      
        public List<string> CategoryName { get; set; }// kategorileri listelicez
       





    }
}
