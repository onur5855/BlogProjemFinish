using AutoMapper;
using BlogProjem.Dal.Repositories.Interfaces.Concrete;
using BlogProjem.Model.Entities.Concrete;
using BlogProjem.Web.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace BlogProjem.Web.Controllers
{
    
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IMapper mapper;
        private readonly IAppUserRepository appUserRepository;
        private readonly IUserPasswordRepository userPasswordRepository;

        public UserController(UserManager<IdentityUser> userManager, IMapper mapper, IAppUserRepository appUserRepository,IUserPasswordRepository userPasswordRepository)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.appUserRepository = appUserRepository;
            this.userPasswordRepository = userPasswordRepository;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserDTO dto)
        {
            if (ModelState.IsValid) // validasyonlar tamamsa
            {
                if (await userManager.FindByEmailAsync(dto.Mail)==null)
                {
                    if (await userManager.FindByNameAsync(dto.UserName) == null)
                    {
                        string newId = Guid.NewGuid().ToString();
                        IdentityUser identityUser = new IdentityUser { Email = dto.Mail, UserName = dto.UserName, Id = newId };

                        IdentityResult result = await userManager.CreateAsync(identityUser, dto.Password);
                        if (result.Succeeded) // identity tarafında kişi oluşmuşsa
                        {
                            await userManager.AddToRoleAsync(identityUser, "Member");

                            //AppUser appUser = new AppUser();
                            //appUser.FirstName = dto.FirstName;
                            //appUser.LastName = dto.LastName;

                            var user = mapper.Map<AppUser>(dto);
                            user.IdentityId = newId;  // identity kişisi ile appUser kişisini bağladık.

                            using var image = Image.Load(dto.Image.OpenReadStream()); // dosyayı oku al 
                            image.Mutate(a => a.Resize(80,80));  // mutate: değiştirmek , foto yeniden şekilediriliyor.
                            image.Save($"wwwroot/images/{user.IdentityId}.jpg");  // dosyayı images altına kaydet
                            user.ImagePath = $"/images/{user.IdentityId}.jpg"; // ama biz kaydettiğimiz yolu veritabanında tutuyoruz.

                            appUserRepository.Create(user);
                            userPasswordRepository.Create(new UserPassword { pw = user.Password, CreateTime = user.CreatedDate, AppUserId = user.ID, AppUser = user });

                            return RedirectToAction("Login", "Home");
                        }
                        
                    }
                    ModelState.AddModelError("UserName", "bu UserName kullanılıyor");
                    return View(dto);
                }
                ModelState.AddModelError("mail","bu mail kullanılıyor");
                return View(dto);
            }
            return View(dto);
        }


        public async Task<IActionResult> Detail(int id)
        {
            AppUser appUser = appUserRepository.GetDefault(a => a.ID == id);
            IdentityUser identityUser = await userManager.FindByIdAsync(appUser.IdentityId);
            var UserProfil = appUserRepository.GetByDefault(selector: a => new UserDto()
            {
                FirstName = a.FirstName,
                LastName = a.LastName,
                ImagePath = a.ImagePath,
                UserName = a.UserName,
                Email = identityUser.Email,
            }, expression: a => a.IdentityId == identityUser.Id);

            return View(UserProfil);
        }







    }
}
