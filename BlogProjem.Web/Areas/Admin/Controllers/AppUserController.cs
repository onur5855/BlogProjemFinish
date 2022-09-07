using BlogProjem.Dal.Repositories.Interfaces.Concrete;
using BlogProjem.Model.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogProjem.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AppUserController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IAppUserRepository appUserRepository;

        public AppUserController(UserManager<IdentityUser> userManager,IAppUserRepository appUserRepository)
        {
            this.userManager = userManager;
            this.appUserRepository = appUserRepository;
        }
        
        public async Task<IActionResult> Index()
        {

            //identity user kişisi
            IdentityUser identityUser = await userManager.GetUserAsync(User);

            AppUser appUser = appUserRepository.GetDefault(a => a.IdentityId == identityUser.Id);
            if (appUser != null)
            {
                return View(appUser);
            }
            return Redirect("~/");//areasız başlangıç sayfasına yani home-index
        }
    }
}
