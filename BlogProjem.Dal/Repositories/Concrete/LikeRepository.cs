using BlogProjem.Dal.Context;
using BlogProjem.Dal.Repositories.Interfaces.Concrete;
using BlogProjem.Model.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjem.Dal.Repositories.Concrete
{
    public class LikeRepository : ILikeRepository
    {
        private readonly ProjectContext _context;
        private readonly DbSet<Like> _table;

        public LikeRepository(ProjectContext context)
        {
            _context = context;
            _table = context.Set<Like>();
        }
        public void Create(Like entity)
        {
            _table.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Like entity)
        {
            _table.Remove(entity);
            _context.SaveChanges();
        }
    }
}
