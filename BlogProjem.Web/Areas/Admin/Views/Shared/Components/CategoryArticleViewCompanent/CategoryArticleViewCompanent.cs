using BlogProjem.Dal.Repositories.Interfaces.Concrete;
using BlogProjem.Web.Areas.Admin.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogProjem.Web.Areas.Admin.Views.Shared.Components.CategoryArticle
{
    [ViewComponent(Name = "CategoryArticleViewCompanent")]
    public class CategoryArticleViewCompanent : ViewComponent
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryArticleViewCompanent(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }


        public IViewComponentResult Invoke()
        {
            var Category = categoryRepository.GetByDefaults(selector: a=>new GetCategoryDto() 
            {
                ID=a.ID,
                Name=a.Name,               
            },
            expression: a => a.Statu != Model.Enums.Statu.Passive
            );
            return View(Category);
        }
     

    }
}
