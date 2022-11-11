using BlogProjem.Dal.Repositories.Interfaces.Concrete;
using BlogProjem.Web.Areas.Admin.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BlogProjem.Web.Areas.Admin.Views.Shared.Compenents.Users
{
    [ViewComponent(Name = "Admins")]
    public class AdminViewComponent: ViewComponent
    {
        private readonly IAppUserRepository appUserRepository;

        public AdminViewComponent(IAppUserRepository appUserRepository)
        {
            this.appUserRepository = appUserRepository;
        }



        public IViewComponentResult Invoke()
        {
            var adminsDTO = appUserRepository.GetByDefault
                (
                selector: a => new AdminsDTO()
                {
                    ID = a.ID,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    ImagePath = a.ImagePath
                },
                expression: a => a.UserName == User.Identity.Name
                );
            return View(adminsDTO);
        }






    }
}
