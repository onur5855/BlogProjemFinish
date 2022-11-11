using System.ComponentModel.DataAnnotations;

namespace BlogProjem.Web.Areas.Member.Models.DTOs
{
    public class EditEmailDTO
    {
        [Required(ErrorMessage = "bu alan boş bırakılamaz")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Lütfen Email Adresinizi Giriniz..!")]
        public string Email { get; set; }
        
    }
}
