using BlogProjem.Dal.Repositories.Interfaces.Concrete;
using BlogProjem.Model.Entities.Concrete;
using BlogProjem.Web.Areas.Admin.Models.DTOs;
using BlogProjem.Web.Areas.Admin.Models.VMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogProjem.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AppUserController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IAppUserRepository appUserRepository;
        private readonly IArticleRepository articleRepository;
        private readonly SignInManager<IdentityUser> signInManager;

        public AppUserController(UserManager<IdentityUser> userManager,IAppUserRepository appUserRepository,IArticleRepository articleRepository, 
            SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.appUserRepository = appUserRepository;
            this.articleRepository = articleRepository;
            this.signInManager = signInManager;
        }
        public IActionResult Abaut()
        {
            return View();
        }
       
        public async Task<IActionResult> Index()
        {

            //identity user kişisi
            IdentityUser identityUser = await userManager.GetUserAsync(User);
            AppUser appUser = appUserRepository.GetDefault(a => a.IdentityId == identityUser.Id);
            if (appUser != null)
            {
                var article = articleRepository.GetByDefaults
                    (selector: a=>new ArticleVm()
                    {
                      ID=a.ID,
                      AppUser=a.AppUser,
                      AppUserId=a.AppUserId,
                      Comments=a.Comments,
                      Content=a.Content,
                      CreatTime=a.CreatedDate,
                      ImagePath=a.ImagePath,
                      Likes=a.Likes,
                      Title=a.Title,
                      Views= a.views,
                      ReadingTime= a.Content.Length / 200,
                    },
                    expression:a=>a.Statu!=Model.Enums.Statu.Passive,
                    include:a=>a.Include(a=>a.AppUser).Include(a=>a.Comments).ThenInclude(a=>a.AppUser)
                    );
                return View(article);
            }
            return Redirect("~/");//areasız başlangıç sayfasına yani home-index
        }
        
        public IActionResult GetArticleIndex(int id)
        {

            //identity user kişisi
            
                var article = articleRepository.GetByDefaults
                    (selector: a => new ArticleVm()
                    {
                        ID = a.ID,
                        AppUser = a.AppUser,
                        AppUserId = a.AppUserId,
                        Comments = a.Comments.Where(a => a.Statu != Model.Enums.Statu.Passive).ToList(),
                        Content = a.Content,
                        CreatTime = a.CreatedDate,
                        ImagePath = a.ImagePath,
                        Likes = a.Likes,
                        Title = a.Title,
                        Views = a.views,
                        ReadingTime = a.Content.Length / 200,
                    },
                    expression: a => a.ArticleCategories.Any(a=>a.CategoryId==id),
                    include: a => a.Include(a => a.AppUser).Include(a => a.Comments).ThenInclude(a => a.AppUser).Include(a=>a.ArticleCategories).ThenInclude(a=>a.Category)
                    );
                return View(article);
            
           
        }
        [HttpGet]
        public async Task< IActionResult> UserDetail(int id)
        {
            AppUser appUser = appUserRepository.GetDefault(a => a.ID == id);
            IdentityUser identityUser = await userManager.FindByIdAsync(appUser.IdentityId);

            var UserProfil = appUserRepository.GetByDefault(selector: a => new GetUserDto()
            {
                id=a.ID,
                FirstName = a.FirstName,
                LastName = a.LastName,
                ImagePath = a.ImagePath,
                UserName = a.UserName,
                Email = identityUser.Email,
            }, expression: a => a.ID == id);

            return View(UserProfil);
        }
        [HttpPost]
        public IActionResult UserDetail(GetUserDto getUserDto)
        {
            var user = appUserRepository.GetDefault(a => a.ID == getUserDto.id);
            appUserRepository.Delete(user);
            return RedirectToAction("List");
        }

        public async Task< IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return Redirect("~/"); // yani areanın dışındaki anasayfaya demiş olduk
            
        }
        public IActionResult Edit()
        {
            return View();
        }
        public IActionResult List()
        {
            var AppUserList = appUserRepository.GetByDefaults(selector: a => new PassiveUserListDTO()
            {
                ID = a.ID,
                FirstName=a.FirstName,
                LastName=a.LastName,
                ImagePath=a.ImagePath,
                UserName=a.UserName
            },
            expression:a=>a.Statu==Model.Enums.Statu.Passive
            ) ;
            return View(AppUserList);
        }
        public IActionResult Active(int id)
        {
            var AppUser = appUserRepository.GetDefault(a => a.ID == id);
            AppUser.Statu = Model.Enums.Statu.Active;
            appUserRepository.Update(AppUser);
            return RedirectToAction("List");
        }

        public async Task< IActionResult> Profil()
        {
            IdentityUser identityUser = await userManager.GetUserAsync(User);
            var Admin=appUserRepository.GetDefault(a => a.IdentityId == identityUser.Id);

            var AdminProfil = appUserRepository.GetByDefault(selector: a => new GetUserDto()
            {
                id = a.ID,
                FirstName = a.FirstName,
                LastName = a.LastName,
                ImagePath = a.ImagePath,
                UserName = a.UserName,
                Email = identityUser.Email,
            }, expression: a => a.ID == Admin.ID );

            return View(AdminProfil);
        }
    }
}
