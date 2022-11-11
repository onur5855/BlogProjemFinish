using BlogProjem.Dal.Repositories.Interfaces.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogProjem.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        public IActionResult List()
        {
            var category = categoryRepository.GetDefaults(a => a.Statu == Model.Enums.Statu.Passive);
            return View(category);
        }
        public IActionResult Active(int id)
        {
            var categori=categoryRepository.GetDefault(a => a.ID == id);
            categori.Statu = Model.Enums.Statu.Active;
            categoryRepository.Update(categori);
            return RedirectToAction("Index","AppUser");
        }
    }
}
