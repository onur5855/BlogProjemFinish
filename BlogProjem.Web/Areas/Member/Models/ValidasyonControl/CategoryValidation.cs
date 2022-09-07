using BlogProjem.Web.Areas.Member.Models.DTOs;
using System.ComponentModel.DataAnnotations;

namespace BlogProjem.Web.Areas.Member.Models.ValidasyonControl
{
    public class CategoryValidation: RequiredAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            List<GetCategoryDTO> instance = value as List<GetCategoryDTO>;
            int count = instance == null ? 0 : (from p in instance
                                                where p.IsSelected == true
                                                select p).Count();
            if (count >= 1)
                return ValidationResult.Success;
            else
                return new ValidationResult(ErrorMessage);
        }
    }
}
