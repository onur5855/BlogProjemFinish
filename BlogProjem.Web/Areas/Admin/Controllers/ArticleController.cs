using BlogProjem.Dal.Repositories.Interfaces.Concrete;
using BlogProjem.Web.Areas.Admin.Models.DTOs;
using BlogProjem.Web.Areas.Admin.Models.VMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogProjem.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ArticleController : Controller
    {
        private readonly IArticleRepository articleRepository;

        public ArticleController(IArticleRepository articleRepository)
        {
            this.articleRepository = articleRepository;
        }


        public IActionResult List()
        {
            var article = articleRepository.GetByDefaults
                   (selector: a => new ArticleListDto()
                   {
                       ID = a.ID,
                       AppUser = a.AppUser,
                       AppUserId = a.AppUserId,
                       Content = a.Content,
                       CreatTime = a.CreatedDate,
                       ImagePath = a.ImagePath,
                       Title = a.Title,
                       ArticleCategories=a.ArticleCategories
                   },
                   expression: a => a.Statu == Model.Enums.Statu.Passive,
                   include: a => a.Include(a => a.AppUser).Include(a=>a.ArticleCategories).ThenInclude(a=>a.Category).Include(a => a.Comments).ThenInclude(a => a.AppUser)
                   );
            return View(article);
            //var Article =  articleRepository.GetDefaults(a=>a.Statu==Model.Enums.Statu.Passive);
            //return View(Article);
        }
        public IActionResult Detail(int id)
        {
            var Article = articleRepository.GetByDefault
                (selector: a => new ArticleDto()
                { ID=a.ID,
                  AppUser=a.AppUser,
                  AppUserId=a.AppUserId,
                  ArticleCategories = a.ArticleCategories,
                  Content = a.Content,
                  CreatTime=a.CreatedDate,
                  ImagePath=a.ImagePath,
                  Title=a.Title
                },
                expression:a=>a.ID==id,
                include:a=>a.Include(a=>a.ArticleCategories).ThenInclude(a=>a.Category)
                );
            return View(Article);
        }

        public IActionResult Active(int id)
        {
            var article=articleRepository.GetDefault(a=>a.ID==id);
            article.Statu=Model.Enums.Statu.Active;
            articleRepository.Update(article);
            return RedirectToAction ("Index","AppUser");
        }


       public IActionResult Delete(int id)
        {
            var DeleteArticle = articleRepository.GetDefault(a => a.ID == id);
            articleRepository.Delete(DeleteArticle);
            return RedirectToAction("Index", "AppUser");
        }
    }
}
