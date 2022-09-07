using System.ComponentModel.DataAnnotations;

namespace BlogProjem.Web.Areas.Member.Models.DTOs
{
    public class CreateCategoryDTO
    {
        [Required(ErrorMessage ="bu alan boş bırakılamaz!")]
        [MinLength(3),MaxLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "bu alan boş bırakılamaz!")]
        [MinLength(3), MaxLength(150)]
        public string Description { get; set; }


    }
}
