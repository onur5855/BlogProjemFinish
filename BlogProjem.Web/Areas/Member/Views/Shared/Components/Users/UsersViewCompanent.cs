using BlogProjem.Dal.Repositories.Interfaces.Concrete;
using BlogProjem.Web.Areas.Member.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BlogProjem.Web.Areas.Member.Views.Shared.Components.Users
{
    [ViewComponent(Name = "Users")]
    public class UsersViewCompanent:ViewComponent
    {
        private readonly IAppUserRepository appUserRepository;

        public UsersViewCompanent(IAppUserRepository appUserRepository)
        {
            this.appUserRepository = appUserRepository;
        }

        public IViewComponentResult Invoke()
        {
            var userdto = appUserRepository.GetByDefault
                (
                selector: a=> new UsersDTO() 
                {
                    ID=a.ID,
                    FirstName=a.FirstName,
                    LastName=a.LastName,
                    ImagePath=a.ImagePath
                },
                expression:a=>a.UserName==User.Identity.Name                
                );
            return View(userdto);
        }

    }
}
