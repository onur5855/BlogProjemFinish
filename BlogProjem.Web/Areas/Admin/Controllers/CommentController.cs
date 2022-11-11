using BlogProjem.Dal.Repositories.Interfaces.Concrete;
using BlogProjem.Web.Areas.Admin.Models.VMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogProjem.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CommentController : Controller
    {
        private readonly IArticleRepository articleRepository;
        private readonly ICommentRepository commentRepository;

        public CommentController(IArticleRepository articleRepository,ICommentRepository commentRepository)
        {
            this.articleRepository = articleRepository;
            this.commentRepository = commentRepository;
        }
        public IActionResult List(int id)
        {
            var article = articleRepository.GetByDefault
                (
                selector: a=> new CommentArticleVm()
                {
                    ArticleId = id,
                    AppUser=a.AppUser,
                    AppUserId=a.AppUserId,
                    Comments=a.Comments.Where(a=>a.Statu!=Model.Enums.Statu.Passive).ToList(),
                    Title=a.Title,
                    Content=a.Content,
                    ImagePath=a.ImagePath,
                    CreatedDate=a.CreatedDate,
                    Likes=a.Likes,
                    views=a.views
                },
                expression:a=>a.ID==id,
                 include: a => a.Include(a => a.Comments).ThenInclude(a => a.AppUser).Include(a => a.AppUser)
                );

            return View(article);
        }
        public IActionResult Passive(int id,int id2)
        {
            var comment = commentRepository.GetDefault(a=>a.ID==id);
            //comment.Statu=Model.Enums.Statu.Passive;
            commentRepository.Delete(comment);
            return RedirectToAction("List", new { id = id2 });
        }

        


    }
}
