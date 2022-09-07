using BlogProjem.Dal.Repositories.Interfaces.Concrete;
using BlogProjem.Model.Entities.Concrete;
using BlogProjem.Web.Areas.Member.Models.DTOs;
using BlogProjem.Web.Areas.Member.Models.VMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace BlogProjem.Web.Areas.Member.Controllers
{
   
    [Area("Member")]
    [Authorize(Roles = "Member")]
    public class AppUserController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IAppUserRepository appUserRepository;
        private readonly IUserFollowCategoryRepository userFollowCategoryRepository;
        private readonly IArticleCategoryRepository articleCategoryRepository;
        private readonly IArticleRepository articleRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IUserPasswordRepository userPasswordRepository;

        public AppUserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IAppUserRepository appUserRepository,
            IUserFollowCategoryRepository userFollowCategoryRepository, IArticleCategoryRepository articleCategoryRepository,
            IArticleRepository articleRepository,ICategoryRepository categoryRepository, IUserPasswordRepository userPasswordRepository)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.appUserRepository = appUserRepository;
            this.userFollowCategoryRepository = userFollowCategoryRepository;
            this.articleCategoryRepository = articleCategoryRepository;
            this.articleRepository = articleRepository;
            this.categoryRepository = categoryRepository;
            this.userPasswordRepository = userPasswordRepository;
        }
        //TODO: kayıtlı kullanıcı anasayfası takip ettiği kategori varsa o kategoriye ait makalelerin listelenmesini isteriz. yoksa en güncel 10 makale
        //listelensin isteriz
        // makalerde yorum varsa onlar gözükmeli like bilgileri gözükmeli 
        public async Task<IActionResult> Index()
        {
            //identity user kişisi
            IdentityUser identityUser =await userManager.GetUserAsync(User);
            AppUser appUser = appUserRepository.GetDefault(a=>a.IdentityId==identityUser.Id);
            if (appUser!= null)
            {
                var aaa = userFollowCategoryRepository.GetDefaults(a => a.AppUserId == appUser.ID);

                var article = articleRepository.GetByDefaults
                    (
                    selector: a => new ArticleVM
                    {
                        ID = a.ID,
                        AppUserId = a.AppUserId,
                        AppUser = a.AppUser,
                        Title = a.Title, //baslık
                        Content = a.Content,//açıklaması
                        ImagePath = a.ImagePath,// makalenin resmi
                        Likes = a.Likes,
                        Comments = a.Comments.Where(a=>a.Statu!=Model.Enums.Statu.Passive).ToList(),
                        ArticleCategories = a.ArticleCategories,
                        Views = a.views,
                        ReadingTime = a.Content.Length / 200,
                        CreatTime = a.CreatedDate,

                    },
                    expression: a => a.Statu != Model.Enums.Statu.Passive,
                    include: a => a.Include(a => a.AppUser).Include(a => a.ArticleCategories).Include(a => a.Likes).Include(a => a.Comments)
                    ) ;

                return View(article);

            }
           // return RedirectToAction("Index", "Home");//globeldeki home-index yani anasayfa  (2 side aynı işlevi yapıyor)
            return Redirect("~/");//areasız başlangıç sayfasına yani home-index

        }
    

        public async Task<IActionResult> LogOut()
        {
            await signInManager.SignOutAsync();
            return Redirect("~/"); // yani areanın dışındaki anasayfaya demiş olduk
        }
        public async Task<IActionResult> EditUserName()
        {
            IdentityUser identityUser = await userManager.GetUserAsync(User);
            AppUser appUser = appUserRepository.GetDefault(a => a.IdentityId == identityUser.Id);
            var UserNameEditProfil = appUserRepository.GetByDefault(selector: a => new EditUserNameDTO()
            {
               
                IdentityID = a.IdentityId,                              
                UserName = a.UserName,
                
            }, expression: a => a.IdentityId == identityUser.Id);
            return View(UserNameEditProfil);
        }
        [HttpPost]
        public async Task<IActionResult> EditUserName(EditUserNameDTO dto)
        {
            IdentityUser identityUser = await userManager.FindByIdAsync(dto.IdentityID);
            AppUser appUser = appUserRepository.GetDefault(a => a.IdentityId == dto.IdentityID);
            IdentityUser user = await userManager.FindByNameAsync(dto.UserName);
            if (user == null)
            {
                await userManager.SetUserNameAsync(identityUser, dto.UserName);                
                appUser.UserName = dto.UserName;
                appUserRepository.Update(appUser);
                await signInManager.SignOutAsync();
                return Redirect("~/");                
            }
            ModelState.AddModelError("UserName", $"{dto.UserName} bu isim kullanılıyor");            
            return View();
        }
        public async Task<IActionResult> EditPw()
        {
            IdentityUser identityUser = await userManager.GetUserAsync(User);
            AppUser appUser = appUserRepository.GetDefault(a => a.IdentityId == identityUser.Id);
            var userpw = appUserRepository.GetByDefault(selector: a=> new EditPwDTO()
            {
                Id=a.ID,
                identityId=a.IdentityId,
                PW=a.Password,//pw silinebilir kullanıcı giricek kukiden alıp kontrol edebiliriz gerek yok
            },expression:a=>a.IdentityId==identityUser.Id
            );
            return View(userpw);

        }
        [HttpPost]
        public async Task<IActionResult> EditPw(EditPwDTO dto)
        {
            IdentityUser identityUser = await userManager.GetUserAsync(User);
            AppUser appUser = appUserRepository.GetDefault(a => a.IdentityId == identityUser.Id);
            if (ModelState.IsValid)
            {
                if (dto.PW==appUser.Password)
                {
                    if (dto.NewPassword==dto.ConfirumNewPassword)
                    {
                        var pwlist= userPasswordRepository.GetDefaults(a => a.AppUserId == appUser.ID).OrderByDescending(a=>a.CreateTime).Take(3);
                        foreach (var item in pwlist)
                        {
                            if (item.pw == dto.NewPassword)
                            {
                                ModelState.AddModelError("NewPassword",$"yeni şifreniz son 3 şifreden farklı olmalıdır");
                                return View(dto);
                            }                                                      
                        }
                        appUser.Password=dto.NewPassword;
                        appUserRepository.Update(appUser);                       
                        identityUser.PasswordHash = userManager.PasswordHasher.HashPassword(identityUser,dto.NewPassword);                       
                        userPasswordRepository.Create(new UserPassword { pw = appUser.Password, CreateTime = DateTime.Now, AppUserId = appUser.ID, AppUser = appUser });
                        await signInManager.SignOutAsync();
                        return Redirect("~/");
                    }
                    ModelState.AddModelError("NewPassword",$"new password confirumNewPasswordla uyuşmuyor");
                    return View(dto);
                }
                ModelState.AddModelError("PW",$"girdiğiniz şifre mevcut şifre ile uyuşmuyor");
                return View(dto);
            }

            return View(dto);
        }
        public async Task<IActionResult> EditUser()
        {
            IdentityUser identityUser = await userManager.GetUserAsync(User);
            AppUser appUser = appUserRepository.GetDefault(a => a.IdentityId == identityUser.Id);
            var UserEditProfil = appUserRepository.GetByDefault(selector: a => new EditUserDTO()
            {    
                ID=a.ID,
                FirstName=a.FirstName,
                LastName=a.LastName,
                ImagePath=a.ImagePath,
                UserName=a.UserName,                

            }, expression: a => a.IdentityId == identityUser.Id);
            return View(UserEditProfil);
        }
        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserDTO dto)
        {
            IdentityUser identityUser = await userManager.GetUserAsync(User);
            AppUser user = appUserRepository.GetDefault(a => a.IdentityId == identityUser.Id);
            if (ModelState.IsValid)
            {
                if (dto.Image!=null)
                {
                    FileSystem.DeleteFile($"wwwroot{dto.ImagePath}");//var olan resmi silmesini bekliyorum
                    using var image = Image.Load(dto.Image.OpenReadStream()); // dosyayı oku al
                    image.Mutate(a => a.Resize(80, 80));   // mutate: değiştirmek , foto yeniden şekilediriliyor.
                    image.Save($"wwwroot/images/{dto.ID}.jpg");  // dosyayı images altına kaydet
                    user.ImagePath = $"/images/{dto.ID}.jpg"; // ama biz kaydettiğimiz yolu veritabanında tutuyoruz.
                }
                user.FirstName = dto.FirstName;
                user.LastName = dto.LastName;
                appUserRepository.Update(user);
                return RedirectToAction("Profil");
            }
            return View(dto);
        }
        public async Task<IActionResult> EditEmail() 
        {
            IdentityUser identityUser = await userManager.GetUserAsync(User);
            var EmailEditProfil= new EditEmailDTO() { Email=identityUser.Email};
            
            return View(EmailEditProfil);
        }
        [HttpPost]
        public async Task<IActionResult> EditEmail(EditEmailDTO dto)
        {
            IdentityUser identityUser = await userManager.GetUserAsync(User);
            if (await userManager.FindByEmailAsync(dto.Email)==null)
            {
                await userManager.SetEmailAsync(identityUser,dto.Email);
                await signInManager.SignOutAsync();
                return Redirect("~/");
            }
            ModelState.AddModelError("Email", $"{dto.Email} bu Email kullanılıyor");
            return View();
            //todo: bu kullanıyor döndüğünde değiştirmediğinde yani textte kullanılan emaile dönmeli en son yazdıgı kabul edilmeyen email yazmamalı
        }
        public async Task<IActionResult> Detail(int id)
        {
            AppUser appUser = appUserRepository.GetDefault(a => a.ID==id);
            IdentityUser identityUser = await userManager.FindByIdAsync(appUser.IdentityId);
            var UserProfil = appUserRepository.GetByDefault(selector: a => new IdentityUserDTO()
            {
                FirstName = a.FirstName,
                LastName = a.LastName,
                ImagePath = a.ImagePath,
                UserName = a.UserName,
                Email = identityUser.Email,
            }, expression: a => a.IdentityId == identityUser.Id);

            return View(UserProfil);
        }
        public async Task<IActionResult> Profil()
        {
            IdentityUser identityUser = await userManager.GetUserAsync(User);
            //AppUser appUser = appUserRepository.GetDefault(a => a.IdentityId == identityUser.Id);

            var UserProfil= appUserRepository.GetByDefault(selector:a=> new IdentityUserDTO() 
            {FirstName=a.FirstName,
            LastName=a.LastName,
            ImagePath=a.ImagePath,
            UserName=a.UserName,
            Email=identityUser.Email,
            } , expression: a => a.IdentityId == identityUser.Id);
            return View(UserProfil);
        }

    }
}
