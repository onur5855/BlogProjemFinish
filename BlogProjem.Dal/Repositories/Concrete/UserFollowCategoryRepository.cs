using BlogProjem.Dal.Context;
using BlogProjem.Dal.Repositories.Interfaces.Concrete;
using BlogProjem.Model.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjem.Dal.Repositories.Concrete
{
    public class UserFollowCategoryRepository : IUserFollowCategoryRepository
    {
        private readonly ProjectContext context;
        private readonly DbSet<UserFollowCategory> _table;

        public UserFollowCategoryRepository(ProjectContext context)
        {
            this.context = context;
            _table = context.Set<UserFollowCategory>();
        }


        public void Create(UserFollowCategory entity)
        {
            _table.Add(entity);

            context.SaveChanges();
        }

        public void Delete(UserFollowCategory entity)
        {
            _table.Remove(entity);
            context.SaveChanges();
        }

        public TResult GetByDefault<TResult>(Expression<Func<UserFollowCategory, TResult>> selector, Expression<Func<UserFollowCategory, bool>> expression, Func<IQueryable<UserFollowCategory>, IIncludableQueryable<UserFollowCategory, object>> include = null)
        {
            IQueryable<UserFollowCategory> query = _table;   // tablomuuzu sorgulanabilir T tipini barındıran bir tablo olarak atadık.
            if (include != null)    // include parametresi varsa
            {
                query = include(query);
            }
            if (expression != null)  // expression parametresi varsa
            {
                query = query.Where(expression);
            }
            return query.Select(selector).First();  // en son dönen tablo/tablolardan seçim işlemi de yapıp TEK bir TResult nesnesi döndürür.

            // önce dahil etme işlemi varsa yapar sonra filtreler sonra seçer ve sonucu döndürür.
        }

        public List<TResult> GetByDefaults<TResult>(Expression<Func<UserFollowCategory, TResult>> selector, Expression<Func<UserFollowCategory, bool>> expression, Func<IQueryable<UserFollowCategory>, IIncludableQueryable<UserFollowCategory, object>> include = null, Func<IQueryable<UserFollowCategory>, IOrderedQueryable<UserFollowCategory>> orderBy = null)
        {
            IQueryable<UserFollowCategory> query = _table;   // tablomuuzu sorgulanabilir T tipini barındıran bir tablo olarak atadık.
            if (include != null)    // include parametresi varsa
            {
                query = include(query);
            }
            if (expression != null)  // expression parametresi varsa
            {
                query = query.Where(expression);
            }
            if (orderBy != null)  // orderBy parametrsi varsa
            {
                return orderBy(query).Select(selector).ToList();
            }
            return query.Select(selector).ToList();

            //NOT => orderBy varsa sırala -seç - liste döndür
            //       orderBy yoksa         seç - liste döndür 
        }

        public UserFollowCategory GetDefault(Expression<Func<UserFollowCategory, bool>> expression)
        {
            return _table.Where(expression).FirstOrDefault();
        }

        public List<UserFollowCategory> GetDefaults(Expression<Func<UserFollowCategory, bool>> expression)
        {
            return _table.Where(expression).ToList();
        }
    }
}
