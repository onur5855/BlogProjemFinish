using BlogProjem.Dal.Repositories.Interfaces.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace BlogProjem.Web.Areas.Admin.Views.Shared.Components.ArticlePassive
{
    [ViewComponent(Name = "ArticlePassive")]
    public class PassiveViewCompanent : ViewComponent
    {
        private readonly IArticleRepository articleRepository;

        public PassiveViewCompanent(IArticleRepository articleRepository)
        {
            this.articleRepository = articleRepository;
        }

        public IViewComponentResult Invoke()
        {
            var PassiveArticle = articleRepository.GetDefaults(a=>a.Statu==Model.Enums.Statu.Passive);
            return View(PassiveArticle);
        }



    }
}
