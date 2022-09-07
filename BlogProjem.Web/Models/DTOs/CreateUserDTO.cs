

using System.ComponentModel.DataAnnotations;

namespace BlogProjem.Web.Models.DTOs
{
    public class CreateUserDTO
    {
        [Required(ErrorMessage ="bu alan boş olamaz")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "bu alan boş olamaz")]
        public string LastName { get; set; }

        [Required(ErrorMessage ="bu alan boş bırakılamaz")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="bu aln boş bırakılamaz")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage ="bu alan boş bırakılamaz")]
        public IFormFile Image { get; set; }

        [Required(ErrorMessage ="bu alan boş bırakılamaz")]
        [DataType(DataType.EmailAddress)]
        public string Mail { get; set; }


        public string? ImagePath { get; set; }

    }
}
