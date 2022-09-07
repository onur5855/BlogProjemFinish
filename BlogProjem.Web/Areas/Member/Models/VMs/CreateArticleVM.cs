using BlogProjem.Web.Areas.Member.Models.DTOs;
using BlogProjem.Web.Areas.Member.Models.ValidasyonControl;
using System.ComponentModel.DataAnnotations;

namespace BlogProjem.Web.Areas.Member.Models.VMs
{
    public class CreateArticleVM
    {
        //Article
        [Required(ErrorMessage ="bu alan boş geçilemez")]
        public string Title { get; set; }
        [Required(ErrorMessage = "bu alan boş geçilemez")]
        public string Content { get; set; }

        public string? ImagePath { get; set; }//bu alan kayıttan sonra dolacak boş bırakılmaz diyemeyiz

        [Required(ErrorMessage = "bu alan boş geçilemez")]
        public IFormFile Image { get; set; }

        // Category
        //[Required(ErrorMessage = "lütfen kategori seçiniz")]
        //public int CategoryID { get; set; }


        [CategoryValidation(ErrorMessage ="kategori seçiniz")]
        public List<GetCategoryDTO>  GetCategories  { get; set; }

        //User

        [Required(ErrorMessage = "bu alan boş geçilemez")]
        
        public int AppUserID { get; set; }




    }
}
