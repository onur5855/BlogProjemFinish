using BlogProjem.Dal.Context;
using BlogProjem.Dal.Repositories.Abstract;
using BlogProjem.Dal.Repositories.Interfaces.Concrete;
using BlogProjem.Model.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjem.Dal.Repositories.Concrete
{
    public class AppUserRepository : BaseRepository<AppUser>, IAppUserRepository
    {
        public AppUserRepository(ProjectContext context) : base(context)
        {
        }
    }
}
