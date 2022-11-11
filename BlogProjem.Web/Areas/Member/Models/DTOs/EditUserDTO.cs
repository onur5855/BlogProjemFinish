using System.ComponentModel.DataAnnotations;

namespace BlogProjem.Web.Areas.Member.Models.DTOs
{
    public class EditUserDTO
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "bu alan boş olamaz")]
        [MinLength(2, ErrorMessage = "2 karekterden az olamaz"), MaxLength(20, ErrorMessage = "20 karekteri gecemez")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "bu alan boş olamaz")]
        [MinLength(2, ErrorMessage = "2 karekterden az olamaz"), MaxLength(30, ErrorMessage = "30 karekteri gecemez")]
        public string LastName { get; set; }

      
        public string ImagePath { get; set; }

        [Required(ErrorMessage = "bu alan boş bırakılamz")]
        [Web.Models.Validasyon.AllowedExtensions(new string[] { ".jpg", ".png", ".jpeg", ".bmp", ".gif", ".pbm", ".tga", ".tiff" })]
        public IFormFile? Image { get; set; }

        
        //public string UserName { get; set; }
    }
}
