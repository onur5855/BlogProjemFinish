using System.ComponentModel.DataAnnotations;

namespace BlogProjem.Web.Areas.Member.Models.DTOs
{
    public class CreateCategoryDTO
    {
        [Required(ErrorMessage ="bu alan boş bırakılamaz!")]
        [MinLength(2,ErrorMessage ="başlık 2 karekterden az olamaz"),MaxLength(100,ErrorMessage ="başlık 100 karekterden fazla olamaz")]
        public string Name { get; set; }

        [Required(ErrorMessage = "bu alan boş bırakılamaz!")]
        [MinLength(25,ErrorMessage ="açıklama 25 karekterden az olamaz"), MaxLength(150,ErrorMessage ="açıklama 150 karekterden fazla olamaz")]
        public string Description { get; set; }


    }
}
