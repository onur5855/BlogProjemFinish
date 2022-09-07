using AutoMapper;
using BlogProjem.Dal.Repositories.Interfaces.Concrete;
using BlogProjem.Model.Entities.Concrete;
using BlogProjem.Web.Areas.Member.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using BlogProjem.Model.Enums;
using Microsoft.AspNetCore.Identity;
using BlogProjem.Web.Areas.Member.Models.VMs;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace BlogProjem.Web.Areas.Member.Controllers
{
    [Area("Member")]
    [Authorize(Roles = "Member")]
    public class CategoryController : Controller
    {
        private readonly IMapper mapper;
        private readonly ICategoryRepository categoryRepository;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IAppUserRepository appUserRepository;
        private readonly IUserFollowCategoryRepository userFollowCategoryRepository;

        public CategoryController(IMapper mapper, ICategoryRepository categoryRepository, UserManager<IdentityUser> userManager, IAppUserRepository appUserRepository, IUserFollowCategoryRepository userFollowCategoryRepository)
        {
            this.mapper = mapper;
            this.categoryRepository = categoryRepository;
            this.userManager = userManager;
            this.appUserRepository = appUserRepository;
            this.userFollowCategoryRepository = userFollowCategoryRepository;
        }


        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateCategoryDTO dto)
        {
            if (ModelState.IsValid) //validasyon tamamsa
            {
                var category = mapper.Map<Category>(dto);
                categoryRepository.Create(category);
                return RedirectToAction("List");
            }
            return View(dto);

        }
        public IActionResult Update(int id)
        {
            var updateCategoryDTO = categoryRepository.GetByDefault
                (
                selector: a => new UpdateCategoryDTO() { ID = a.ID, Description = a.Description, Name = a.Name },
                expression: a => a.ID == id
                );
            return View(updateCategoryDTO);
        }
        [HttpPost]
        public IActionResult Update(UpdateCategoryDTO updateCategoryDTO)
        {
            if (ModelState.IsValid)
            {
                var category = mapper.Map<Category>(updateCategoryDTO);
                categoryRepository.Update(category);
                return RedirectToAction("List");
            }
            return View(updateCategoryDTO);
        }
        public IActionResult Delete(int id)
        {
            var category = categoryRepository.GetDefault(a => a.ID == id);
            categoryRepository.Delete(category);
            return RedirectToAction("List");
        }


        public async Task<IActionResult> List()
        {

            //List<Category> list = categoryRepository.GetDefaults(a => a.Statu != Statu.Passive);
            //return View(list);

            IdentityUser identityUser = await userManager.GetUserAsync(User);
            AppUser appUser = appUserRepository.GetDefault(a => a.IdentityId == identityUser.Id);

            var list = categoryRepository.GetByDefaults
                 (
                 selector: a => new UserFollowCategoryVM()
                 {
                     ID = a.ID,
                     Name = a.Name,
                     Description = a.Description,
                     //UserFollowCategories=userFollowCategoryRepository.
                     UserFollowCategories = a.UserFollowCategories.Where(a => a.AppUserId == appUser.ID).ToList()

                 }, expression: a => a.Statu != Statu.Passive
                 );
            return View(list);
        }
        //toDo: Bu controllerın Detay Sil, Güncellesi Sizdedir vievde task liste bakarsan bu satırı görürsin (görev listesi gibi bi ibare)

        public IActionResult Detail(int id)
        {
            var detailCategoryDTO = categoryRepository.GetByDefault
                (
                selector: a => new DetailCategoryDTO() { ID = a.ID, Name = a.Name, Description = a.Description },
                expression: a => a.ID == id
                );
            return View(detailCategoryDTO);
        }


        //TODO: kullanıcı bu kategoriyi takip ediyorsa takibi bırak etmiyorsa takip et yazmalı list yapısının viewında
        public async Task<IActionResult> Follow(int id)
        {
            IdentityUser identityUser = await userManager.GetUserAsync(User);
            AppUser appUser = appUserRepository.GetDefault(a => a.IdentityId == identityUser.Id);

            Category category = categoryRepository.GetDefault(a => a.ID == id);

            category.UserFollowCategories.Add(new UserFollowCategory { Category = category, CategoryId = category.ID, AppUser = appUser, AppUserId = appUser.ID });

            categoryRepository.Update(category);
            return RedirectToAction("List");
        }
        public async Task<IActionResult> UnFollow(int id)
        {
            IdentityUser identityUser = await userManager.GetUserAsync(User);
            AppUser appUser = appUserRepository.GetDefault(a => a.IdentityId == identityUser.Id);

            Category category = categoryRepository.GetDefault(a => a.ID == id);

            //userFollowCategoryRepository.Delete()
            FollowDTO followDTO = new FollowDTO();
            followDTO.AppUserId = appUser.ID;
            followDTO.CategoryId = id;
            var aa = mapper.Map<UserFollowCategory>(followDTO);
            userFollowCategoryRepository.Delete(aa);
            
            return RedirectToAction("List");
        }




    }
}
