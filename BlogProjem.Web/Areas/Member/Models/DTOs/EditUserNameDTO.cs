using System.ComponentModel.DataAnnotations;

namespace BlogProjem.Web.Areas.Member.Models.DTOs
{
    public class EditUserNameDTO
    {
        public string IdentityID { get; set; }

        [Required(ErrorMessage = "bu alan boş olamaz")]
        [MinLength(6, ErrorMessage = "6 karekterden az olamaz"), MaxLength(25, ErrorMessage = "25 karekteri gecemez")]
        public string UserName { get; set; }

    }
}
