using System.ComponentModel.DataAnnotations;

namespace BlogProjem.Web.Areas.Member.Models.VMs
{
    public class IdentityUserEditVM
    {
        public int ID { get; set; }

        public string IdentityID { get; set; }

        [Required(ErrorMessage = "bu alan boş olamaz")]
        public string FirstName { get; set; }


        [Required(ErrorMessage = "bu alan boş olamaz")]
        public string Password { get; set; }
        public string pw { get; set; }
        [Required(ErrorMessage = "bu alan boş olamaz")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "bu alan boş olamaz")]
        public string ConfirumNewPassword { get; set; }

        [Required(ErrorMessage = "bu alan boş olamaz")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "bu alan boş olamaz")]
        public string UserName { get; set; }
        public IFormFile? Image { get; set; }
        public string ImagePath { get; set; }
        [Required(ErrorMessage = "bu alan boş olamaz")]
        public string Email { get; set; }
    }
}
