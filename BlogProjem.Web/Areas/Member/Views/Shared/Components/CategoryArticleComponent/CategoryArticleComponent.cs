using BlogProjem.Dal.Repositories.Interfaces.Concrete;
using BlogProjem.Web.Areas.Member.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BlogProjem.Web.Areas.Member.Views.Shared.Components.CategoryArticleComponent
{
    [ViewComponent(Name = "CategoryArticleComponent")]
    public class CategoryArticleComponent: ViewComponent
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryArticleComponent(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }


        public IViewComponentResult Invoke()
        {
            var Category = categoryRepository.GetByDefaults(selector: a => new GetCategoryCompanentDto()
            {
                ID = a.ID,
                Name = a.Name,
            },
            expression: a => a.Statu != Model.Enums.Statu.Passive
            );
            return View(Category);
        }
    }
}
