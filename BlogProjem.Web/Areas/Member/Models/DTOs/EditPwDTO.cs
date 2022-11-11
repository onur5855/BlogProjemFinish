using System.ComponentModel.DataAnnotations;

namespace BlogProjem.Web.Areas.Member.Models.DTOs
{
    public class EditPwDTO
    {
        public int Id { get; set; }
        public string identityId { get; set; }

        [Required(ErrorMessage = "bu aln boş bırakılamaz")]
        [DataType(DataType.Password)]
        public string PW { get; set; }

        [RegularExpression(@"^(?=.*?[a-z])(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-.,;]).{8,14}$", ErrorMessage = "şifreniz 8 ile 14 karekter aralığında buyuk harf, küçük harf, özel karekter ve sayı içermelidir.")]
        [Required(ErrorMessage = "bu alan boş bırakılamaz")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [RegularExpression(@"^(?=.*?[a-z])(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-.,;]).{8,14}$", ErrorMessage = "şifreniz 8 ile 14 karekter aralığında buyuk harf, küçük harf, özel karekter ve sayı içermelidir.")]
        [Required(ErrorMessage = "bu alan boş bırakılamaz")]
        [DataType(DataType.Password)]
        [Compare("NewPassword",ErrorMessage ="parola dogrulama uyuşmuyor")]
        public string ConfirumNewPassword { get; set; }
    }
}
