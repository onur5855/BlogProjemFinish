using BlogProjem.Dal.Repositories.Interfaces.Concrete;
using BlogProjem.Model.Entities.Concrete;
using BlogProjem.Web.Areas.Member.Models.DTOs;
using BlogProjem.Web.Areas.Member.Models.VMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogProjem.Web.Areas.Member.Controllers
{
    [Area("Member")]
    [Authorize(Roles = "Member")]
    public class CommentController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IAppUserRepository appUserRepository;
        private readonly IUserFollowCategoryRepository userFollowCategoryRepository;
        private readonly IArticleCategoryRepository articleCategoryRepository;
        private readonly IArticleRepository articleRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly ICommentRepository commentRepository;
        private readonly ILikeRepository likeRepository;

        public CommentController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IAppUserRepository appUserRepository,
            IUserFollowCategoryRepository userFollowCategoryRepository, IArticleCategoryRepository articleCategoryRepository,
            IArticleRepository articleRepository, ICategoryRepository categoryRepository, ICommentRepository commentRepository, ILikeRepository likeRepository)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.appUserRepository = appUserRepository;
            this.userFollowCategoryRepository = userFollowCategoryRepository;
            this.articleCategoryRepository = articleCategoryRepository;
            this.articleRepository = articleRepository;
            this.categoryRepository = categoryRepository;
            this.commentRepository = commentRepository;
            this.likeRepository = likeRepository;
        }
        public async Task<IActionResult> Create(int id)
        {
            IdentityUser identityUser = await userManager.GetUserAsync(User);
            AppUser appUser = appUserRepository.GetDefault(a => a.IdentityId == identityUser.Id);

            var articleComment = articleRepository.GetByDefault(
                selector: a => new CommentArticleVm()
                {
                    IdentitId = appUser.ID,
                    AppUser = a.AppUser,
                    AppUserId = a.AppUserId,
                    ArticleId = a.ID,
                    ImagePath = a.ImagePath,
                    Title = a.Title,
                    Likes = a.Likes,
                    UserFullName = a.AppUser.FullName,
                    views = a.views + 1,
                    Content = a.Content,

                    Comments = a.Comments.Where(a => a.Statu != Model.Enums.Statu.Passive).ToList(),
                    CreatedDate = a.CreatedDate,

                },
                expression: a => a.ID == id,
                include: a => a.Include(a => a.AppUser).Include(a => a.Likes).Include(a => a.Comments).ThenInclude(a => a.AppUser)
                );
            var article = new Article { ID = id, views = articleComment.views, Title = articleComment.Title, Content = articleComment.Content, ImagePath = articleComment.ImagePath, AppUserId = articleComment.AppUserId, CreatedDate = articleComment.CreatedDate };
            articleRepository.Update(article);


            return View(articleComment);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CommentArticleVm vm)
        {
            IdentityUser identityUser = await userManager.GetUserAsync(User);
            AppUser appUser = appUserRepository.GetDefault(a => a.IdentityId == identityUser.Id);


            var comment = new Comment { AppUserId = appUser.ID, ArticleId = vm.ArticleId, Text = vm.Text, Statu = Model.Enums.Statu.Active };
            commentRepository.Create(comment);
            return RedirectToAction("Create");
        }
        //[Route("Edit/{vm}/{id}")]
        [HttpPost]
        public IActionResult Edit(string yorum,int id,int id2)
        {
            var comment=commentRepository.GetDefault(a => a.ID == id);
            comment.Text=yorum;
            commentRepository.Update(comment);
            return RedirectToAction("Create", new { id = id2 });
          
        }
        [Route("Delete/{id}/{id2}")]
        public IActionResult Delete(int id,int id2)
        {
            var Comment = commentRepository.GetDefault(a=>a.ID==id);
            commentRepository.Delete(Comment);
            return RedirectToAction("Create", new { id = id2 });
        }
        public async Task<IActionResult> Like(int id)
        {
            IdentityUser identityUser = await userManager.GetUserAsync(User);
            AppUser appUser = appUserRepository.GetDefault(a => a.IdentityId == identityUser.Id);

            var article=articleRepository.GetDefault(a => a.ID == id);
            var like=new Like { AppUserId=appUser.ID,ArticleId=article.ID};
            likeRepository.Create(like);

            return RedirectToAction("Create", new { id = id});
        }
        public async Task<ActionResult> DisLike(int id)
        {
            IdentityUser identityUser = await userManager.GetUserAsync(User);
            AppUser appUser = appUserRepository.GetDefault(a => a.IdentityId == identityUser.Id);
            var article = articleRepository.GetDefault(a => a.ID == id);
            var like = new Like { AppUserId = appUser.ID, ArticleId = article.ID };
            likeRepository.Delete(like);
            return RedirectToAction("Create", new { id = id });
        }





    }
}
