using System.ComponentModel.DataAnnotations;

namespace BlogProjem.Web.Areas.Member.Models.DTOs
{
    public class EditUserNameDTO
    {
        public string IdentityID { get; set; }

        [Required(ErrorMessage = "bu alan boş olamaz")]
        [MinLength(3,ErrorMessage ="minumum 3 karekter olmalıdır")]
        [MaxLength(30,ErrorMessage ="en fazla 30 karekter olabilir")]        
        public string UserName { get; set; }

    }
}
