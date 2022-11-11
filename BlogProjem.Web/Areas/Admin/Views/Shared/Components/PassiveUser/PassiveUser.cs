using BlogProjem.Dal.Repositories.Interfaces.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace BlogProjem.Web.Areas.Admin.Views.Shared.Components.PassiveUser
{
    [ViewComponent(Name = "PassiveUser")]
    public class PassiveUser: ViewComponent
    {
        private readonly IAppUserRepository appUserRepository;

        public PassiveUser(IAppUserRepository appUserRepository)
        {
            this.appUserRepository = appUserRepository;
        }
        public IViewComponentResult Invoke()
        {
            var PassiveUser = appUserRepository.GetDefaults(a => a.Statu == Model.Enums.Statu.Passive);
            return View(PassiveUser);
        }
    }
}
