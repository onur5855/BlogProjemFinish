using System.ComponentModel.DataAnnotations;

namespace BlogProjem.Web.Areas.Member.Models.DTOs
{
    public class EditUserDTO
    {
        public int ID { get; set; }
        [Required(ErrorMessage ="bu alan boş geçilemez")]
        [MinLength(3,ErrorMessage ="minumum 3 karekter olmak zordunda")]
        [MaxLength(30,ErrorMessage ="maxsimum 30 karekter olmak zordunda")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "bu alan boş geçilemez")]
        [MinLength(3, ErrorMessage = "minumum 3 karekter olmak zordunda")]
        [MaxLength(30, ErrorMessage = "maxsimum 30 karekter olmak zordunda")]
        public string LastName { get; set; }
        public string ImagePath { get; set; }
        public IFormFile? Image { get; set; }
        public string UserName { get; set; }
    }
}
