using AutoMapper;
using BlogProjem.Dal.Repositories.Interfaces.Concrete;
using BlogProjem.Model.Entities.Concrete;
using BlogProjem.Model.Enums;
using BlogProjem.Web.Areas.Member.Models.DTOs;
using BlogProjem.Web.Areas.Member.Models.VMs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.Linq;

namespace BlogProjem.Web.Areas.Member.Controllers
{
    [Area("Member")]
    [Authorize(Roles = "Member")]
    public class ArticleController : Controller
    {
        private readonly IArticleRepository articleRepository;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IAppUserRepository appUserRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;
        private readonly IArticleCategoryRepository articleCategoryRepository;

        public ArticleController(IArticleRepository articleRepository, UserManager<IdentityUser> userManager,
            IAppUserRepository appUserRepository, ICategoryRepository categoryRepository, IMapper mapper,
            IArticleCategoryRepository articleCategoryRepository)
        {
            this.articleRepository = articleRepository;
            this.userManager = userManager;
            this.appUserRepository = appUserRepository;
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
            this.articleCategoryRepository = articleCategoryRepository;
        }
        public async Task<IActionResult> Create()
        {

            IdentityUser identityUser = await userManager.GetUserAsync(User);
            AppUser appUser = appUserRepository.GetDefault(a => a.IdentityId == identityUser.Id);

            CreateArticleVM vm = new CreateArticleVM()
            {
                GetCategories = categoryRepository.GetByDefaults
                 (
                     selector: a => new GetCategoryDTO()
                     {
                         ID = a.ID,
                         Name = a.Name
                     },
                 expression: a => a.Statu != Statu.Passive),
                AppUserID = appUser.ID
            };
            return View(vm);
        }
        //Todo: tuple yapısı ile gönderirsek neler olur: article propları boş liste dolarsa onaylar articlede get metodunda appuserıd yi dolu göndericez
        [HttpPost]
        public IActionResult Create(CreateArticleVM vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.GetCategories.Any(a => a.IsSelected))
                {
                    //kategori seçilmezse hata mesajı vermiyor
                    //any içeriyorsa anlamına gelirse içeriyorsa ıssselectedlardan biri bile true ise if e girer yani kategorilerden
                    //biri seçiliyse girer hiç biri seçilmediyse girmez                   
                    var article = mapper.Map<Article>(vm);

                    using var image = Image.Load(vm.Image.OpenReadStream()); // dosyayı oku al
                    image.Mutate(a => a.Resize(80, 80));   // mutate: değiştirmek , foto yeniden şekilediriliyor.
                    Guid guid = Guid.NewGuid();
                    image.Save($"wwwroot/images/{guid}.jpg");  // dosyayı images altına kaydet
                    article.ImagePath = $"/images/{guid}.jpg"; // ama biz kaydettiğimiz yolu veritabanında tutuyoruz.

                    articleRepository.Create(article);

                    foreach (GetCategoryDTO item in vm.GetCategories.Where(a => a.IsSelected))
                    {
                        ArticleCategoryDTO ac = new ArticleCategoryDTO();
                        ac.CategoryId = item.ID;
                        //ac.Category = categoryRepository.GetDefault(a => a.ID == item.ID);
                        ac.ArticleId = article.ID;
                        //ac.Article = articleRepository.GetDefault(a => a.ID == article.ID);
                        var aa = mapper.Map<ArticleCategory>(ac);
                        articleCategoryRepository.Create(aa);
                    }
                    return RedirectToAction("List");
                }

            }
            //TODO: validasyon sağlanmadıgında hataya düşmemek için httpget gibi categories ve appuser id dolmalı ki , null exception hatası vermesin (select optionslarda)
            return View(vm);
        }

        public async Task<IActionResult> List()
        {
            IdentityUser identityUser = await userManager.GetUserAsync(User);
            AppUser appUser = appUserRepository.GetDefault(a => a.IdentityId == identityUser.Id);
            //cookide olan  kişinin kendi makalelerini çekeceğimiz için öncee kişiyi yakaladık
                       
            var articleList = articleRepository.GetByDefaults
                (
                    selector: a => new GetArticleVm()
                    {
                        ArticleID = a.ID,
                        Title = a.Title,
                        Content = a.Content,
                        ImagePath = a.ImagePath,
                        UserFullName = a.AppUser.FullName,
                        CategoryName = a.ArticleCategories.Where(a => a.CategoryId == a.Category.ID).Where(a=>a.Category.Statu!=Statu.Passive).Select(a => a.Category.Name).ToList()

                    },

                    expression: a => a.AppUserId == appUser.ID && a.Statu != Statu.Passive ,
                    include: a => a.Include(a => a.AppUser).Include(a => a.ArticleCategories).ThenInclude(a => a.Category),
                    orderBy: a => a.OrderByDescending(a => a.CreatedDate)
                );
            for (int i = 0; i < articleList.Count; i++)
            {
                if (articleList[i].CategoryName.Count==0)
                {
                    articleList.RemoveAt(i);
                    i--;//sildiğinde indexi 1 düşürdüğü için fordada 1 arttırdıgı için o arada kontrol etmeden atladıgı index oluyor
                        //ondan dolayı if e girerse i yi düşürdük
                }
            }
                       
            return View(articleList);
        }
        //TODO: detay sayfasında değişikler olucak kaç kişi takip ediyor kaç like var vs gibi şeyler ekleyebiliriz 
        public IActionResult Detail(int id)
        {
            
            //listede aktif article var detaya aktiflerden ulaşıcak            
            var articleList = articleRepository.GetByDefault
                (
                    selector: a => new GetArticleVm()
                    {
                        ArticleID = a.ID,
                        Title = a.Title,
                        Content = a.Content,
                        ImagePath = a.ImagePath,
                        UserFullName = a.AppUser.FullName,
                        CategoryName = a.ArticleCategories.Where(a => a.Category.ID == a.Category.ID).Where(a => a.Category.Statu != Statu.Passive).Select(a => a.Category.Name).ToList()

                    },
                    expression: a => a.ID == id,

                include: a => a.Include(a => a.AppUser).Include(a => a.ArticleCategories).ThenInclude(a=>a.Category)
                );
            return View(articleList);
        }
        public IActionResult Update(int id)
        {
            var CategoryList = categoryRepository.GetByDefaults
                (
                selector: a => new GetCategoryDTO() { ID = a.ID, Name = a.Name }
                , expression: a => a.Statu != Statu.Passive);

            // tüm kategoriler geldi
            var SelectedCategoryList = articleCategoryRepository.GetByDefaults
                (
                selector: a => new SelectedCategoryDTO() { ID = a.CategoryId, Name = a.Category.Name, IsActive = true },
                expression: a => a.ArticleId == id
                );
            //ara toplada articleid bizim updatede etmek istediğimiz articleid si ne eşitse vericek yani secili categoriler
            //seçili kategorileri getircem
            //tüm categorilerden secili kategorileri işaretledik
            for (int i = 0; i < CategoryList.Count; i++)
            {
                for (int a = 0; a < SelectedCategoryList.Count; a++)
                {
                    if (CategoryList[i].ID == SelectedCategoryList[a].ID)
                    {
                        CategoryList[i].IsSelected = true;
                    }
                }
            }

            //şimdi articın bilgilerini getirmem lazım
            var articleList = articleRepository.GetByDefault
                (
                    selector: a => new UpdateArticleVm()
                    {
                        ID = a.ID,
                        Title = a.Title,
                        Content = a.Content,
                        ImagePath = a.ImagePath,
                        AppUserID = a.AppUser.ID,
                        // selectedCategoryDTOs = SelectedCategoryList,
                        getCategoryDTOs = CategoryList
                        //categorilerin listesi yukarda  GetCategoriListte geldi
                        //secili Categorilerde SelectedCategoryListte geldi

                    },
                    expression: a => a.ID == id,

                include: a => a.Include(a => a.AppUser).Include(a => a.ArticleCategories).ThenInclude(a => a.Category)
                );



            return View(articleList /*Tuple.Create< List<UpdateArticleVm>, List<GetCategoryDTO> >( articleList,CategoryList)*/ );


        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateArticleVm articleList)
        {
            if (ModelState.IsValid)
            {
                //başlış değişebilir
                //contenti değişebilir
                //resim değişebilir
                //categoriler değişebilir
                if (articleList.Image != null)//eğer yeni resim eklemişse buraya girmeli eski resmi bilup silmeli ve yeni resmi eklemeli veri
                                              //veri tabanındaki resmin idsi yeni resme gelmeli veya veritabanındaki resmin idside silinip yeni id atanmalı
                {
                    FileSystem.DeleteFile($"wwwroot{articleList.ImagePath}");//var olan resmi silmesini bekliyorum
                    using var image = Image.Load(articleList.Image.OpenReadStream()); // dosyayı oku al                    
                    image.Mutate(a => a.Resize(80, 80));   // mutate: değiştirmek , foto yeniden şekilediriliyor.
                    Guid guid = Guid.NewGuid();
                    image.Save($"wwwroot/images/{guid}.jpg");  // dosyayı images altına kaydet
                    articleList.ImagePath = $"/images/{guid}.jpg"; // ama biz kaydettiğimiz yolu veritabanında tutuyoruz.

                }
                var article = mapper.Map<Article>(articleList);
                articleRepository.Update(article);

                var selectArticleCategori = articleCategoryRepository.GetByDefaults
                     (
                     selector: a => new ArticleCategoryDTO { ArticleId = a.ArticleId, CategoryId = a.CategoryId },
                     expression: a => a.ArticleId == articleList.ID
                     );
                //önce aratoblodaki tüm articleidleri eşit olanları silelim
                foreach (var item in selectArticleCategori)
                {
                    ArticleCategoryDTO articleCategoriDto = new ArticleCategoryDTO();
                    articleCategoriDto.CategoryId = item.CategoryId;
                    articleCategoriDto.ArticleId = item.ArticleId;
                    ArticleCategory aaa = mapper.Map<ArticleCategory>(articleCategoriDto);
                    articleCategoryRepository.Delete(aaa);

                }
                //articlelistdeki catagorilerden seçili olanları eklicem

                foreach (GetCategoryDTO item in articleList.getCategoryDTOs.Where(a => a.IsSelected))
                {
                    ArticleCategoryDTO articleCategoriDto = new ArticleCategoryDTO();
                    articleCategoriDto.ArticleId = articleList.ID;
                    articleCategoriDto.CategoryId = item.ID;
                    var aaa = mapper.Map<ArticleCategory>(articleCategoriDto);
                    articleCategoryRepository.Create(aaa);
                }

                return RedirectToAction("List");
            }

            return View(articleList);
        }
        public IActionResult Delete(int id)
        {
            var a = articleRepository.GetDefault(a => a.ID == id);
            articleRepository.Delete(a);
            return RedirectToAction("List");
        }






    }
}
