using BlogProjem.Dal.Repositories.Interfaces.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace BlogProjem.Web.Areas.Admin.Views.Shared.Components.CategoryPassive
{
    [ViewComponent(Name = "CategoryPassive")]
    public class PassiveViewCompanent: ViewComponent
    {
        private readonly ICategoryRepository categoryRepository;

        public PassiveViewCompanent(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public IViewComponentResult Invoke()
        {
            var PassiveCategory= categoryRepository.GetDefaults(a => a.Statu == Model.Enums.Statu.Passive);
            return View(PassiveCategory);
        }


    }
}
