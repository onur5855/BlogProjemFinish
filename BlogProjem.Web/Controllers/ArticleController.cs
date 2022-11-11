using BlogProjem.Dal.Repositories.Interfaces.Concrete;
using BlogProjem.Web.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogProjem.Web.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleRepository articleRepository;

        public ArticleController(IArticleRepository articleRepository)
        {
            this.articleRepository = articleRepository;
        }
        public IActionResult Detail(int id)
        {
            var articlee = articleRepository.GetByDefault(selector: a=>new ArticleDto()
            { AppUser=a.AppUser,
                AppUserId=a.AppUserId, 
                Title=a.Title,
                Content=a.Content,
                Comments=a.Comments.Where(a => a.Statu != Model.Enums.Statu.Passive).ToList(),
                ImagePath=a.ImagePath,
                views=a.views,
                Likes=a.Likes,
                CreatedDate=a.CreatedDate,
                
            },
            expression:a=>a.ID==id,
            include:a=>a.Include(a=>a.Comments).ThenInclude(a=>a.AppUser).Include(a=>a.AppUser)
            );
            return View(articlee);
        }
    }
}
