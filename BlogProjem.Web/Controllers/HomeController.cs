using BlogProjem.Dal.Repositories.Interfaces.Concrete;
using BlogProjem.Web.Models;
using BlogProjem.Web.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BlogProjem.Web.Controllers
{
   
    public class HomeController : Controller
    {


        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IAppUserRepository appUserRepository;

        public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IAppUserRepository appUserRepository)
        {
            _logger = logger;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.appUserRepository = appUserRepository;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            if (ModelState.IsValid) // validasyon tamamsa
            {
                IdentityUser identityUser = await userManager.FindByEmailAsync(dto.Mail);

                if (identityUser != null) // kullanıcı var - bu maile sahip biri var
                {                   
                                        
                    await signInManager.SignOutAsync();  // içerde biri varsa cookiede silinsn
                    Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(identityUser, dto.Password, true, true);
                    if (result.Succeeded) // şifrede doğru ise
                    {
                        var a= appUserRepository.GetDefault(a => a.IdentityId == identityUser.Id);
                        if (a.Statu != Model.Enums.Statu.Passive) //statusu passif değilse 
                        {
                            string role = (await userManager.GetRolesAsync(identityUser)).FirstOrDefault();
                            return RedirectToAction("Index", "AppUser", new { area = role });  // localHost/member/appuser/index - KAYITLI KULLANICI ANASAYFASI
                        }                                                 
                            ModelState.AddModelError("Password", "Hesabınızın onaylanmasını bekleyin");
                            return View(dto);                                                
                    }

                }
            }
            ModelState.AddModelError("Password", "Email veya şifre yanlış");
            return View(dto);
            
        }


        public async Task<IActionResult> Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                
                IdentityUser identityUser = await userManager.FindByNameAsync(User.Identity.Name) ;
                string role = (await userManager.GetRolesAsync(identityUser)).FirstOrDefault();
                return RedirectToAction("Index", "AppUser",new { area = role }  );
            }            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}