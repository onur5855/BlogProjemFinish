using System.ComponentModel.DataAnnotations;

namespace BlogProjem.Web.Models.DTOs
{
    public class LoginDTO
    {

        [Required(ErrorMessage = "bu alan boş olamaz.")]
        public string Mail { get; set; }

        [Required(ErrorMessage = "bu alan boş olamaz.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }




    }
}
