using BlogProjem.Web.Areas.Member.Models.DTOs;
using BlogProjem.Web.Areas.Member.Models.ValidasyonControl;
using System.ComponentModel.DataAnnotations;

namespace BlogProjem.Web.Areas.Member.Models.VMs
{
    public class CreateArticleVM
    {
        //Article
        [Required(ErrorMessage ="bu alan boş geçilemez")]
        [MinLength(20,ErrorMessage ="başlık 20 karekterden az olamaz"),MaxLength(150,ErrorMessage =" başlık 150 karekteri geçemez")]
        public string Title { get; set; }
        [Required(ErrorMessage = "bu alan boş geçilemez")]
        [MinLength(250, ErrorMessage = "açıklama 250 karekterden az olamaz")]
        public string Content { get; set; }

       // public string? ImagePath { get; set; }//bu alan kayıttan sonra dolacak boş bırakılmaz diyemeyiz

        [Required(ErrorMessage = "bu alan boş geçilemez")]
        [Web.Models.Validasyon.AllowedExtensions(new string[] { ".jpg", ".png", ".jpeg", ".bmp", ".gif", ".pbm", ".tga", ".tiff" })]
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
