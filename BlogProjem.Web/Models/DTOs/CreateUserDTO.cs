
using BlogProjem.Web.Models.Validasyon;
using System.ComponentModel.DataAnnotations;

namespace BlogProjem.Web.Models.DTOs
{
    public class CreateUserDTO
    {
        [Required(ErrorMessage ="bu alan boş olamaz")]
        [MinLength(2,ErrorMessage ="2 karekterden az olamaz"),MaxLength(20,ErrorMessage ="20 karekteri gecemez")]
        public string FirstName { get; set; }



        [Required(ErrorMessage = "bu alan boş olamaz")]
        [MinLength(2, ErrorMessage = "2 karekterden az olamaz"), MaxLength(30, ErrorMessage = "30 karekteri gecemez")]
        public string LastName { get; set; }



        [Required(ErrorMessage ="bu alan boş bırakılamaz")]
        [MinLength(6, ErrorMessage = "6 karekterden az olamaz"), MaxLength(25, ErrorMessage = "25 karekteri gecemez")]
        public string UserName { get; set; }



        [RegularExpression(@"^(?=.*?[a-z])(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-.,;]).{8,14}$",ErrorMessage ="şifreniz 8 ile 14 karekter aralığında buyuk harf, küçük harf, özel karekter ve sayı içermelidir.")]
        [Required(ErrorMessage ="bu alan boş bırakılamaz")]
        [DataType(DataType.Password)]
        public string Password { get; set; }




        [Required(ErrorMessage ="bu alan boş bırakılamz")]
        //[FileExtensions(Extensions = "jpeg" ,ErrorMessage ="resim tipinde dosya yukleyin" )]
        //[DataType(DataType.Upload),FileExtensions(Extensions = ".png .jpg .jpeg .gif",ErrorMessage = "resim tipinde dosya yukleyin")]
        [AllowedExtensions(new string[] { ".jpg", ".png" , ".jpeg", ".bmp", ".gif", ".pbm", ".tga", ".tiff" })]
        public IFormFile Image { get; set; }



        [Required(ErrorMessage ="bu alan boş bırakılamaz")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage ="Lütfen Email Adresinizi Giriniz..!")]
        public string Mail { get; set; }


        //public string? ImagePath { get; set; }

    }
}
