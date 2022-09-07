using BlogProjem.Dal.Repositories.Interfaces.Concrete;
using BlogProjem.Web.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogProjem.Web.Views.Shared.Components.Articles
{
    [ViewComponent(Name ="Articles")]
    public class ArticlesViewComponent:ViewComponent
    {
        private readonly IArticleRepository articleRepository;

        public ArticlesViewComponent(IArticleRepository articleRepository)
        {
            this.articleRepository = articleRepository;
        }
        //bu componentte en guncel Yazılan 10 makaleyi basalım.
        public IViewComponentResult Invoke()
        {
            
            var list = articleRepository.GetByDefaults
                ( selector:a=>new GetArticleForComponentDTO() 
                { 
                    Title=a.Title,
                    Content=a.Content,
                    ArticleID=a.ID,
                    UserId=a.AppUser.ID,
                    UserFullName=a.AppUser.FullName,
                    UserImage=a.AppUser.ImagePath,
                    Image=a.ImagePath,
                    CreatedDate=a.CreatedDate,
                    Like=a.Likes,
                    Comments=a.Comments.OrderByDescending(a=>a.CreatedDate).Take(3).ToList()
                    //CategoryName=a.Category.Name
                },
                expression:a=>a.Statu!=Model.Enums.Statu.Passive,
                include:a=>a.Include(a=>a.AppUser),
                orderBy: a=>a.OrderByDescending(a=>a.CreatedDate)
                );
            return View(list.Take(10).ToList());
        }


    }
}
