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
    public class UserPasswordRepository : IUserPasswordRepository
    {
        private readonly ProjectContext context;
        private readonly DbSet<UserPassword> _table;

        public UserPasswordRepository(ProjectContext context)
        {
            this.context = context;
            _table = context.Set<UserPassword>();
        }
        public void Create(UserPassword entity)
        {
            _table.Add(entity);          
            context.SaveChanges();
        }

        public TResult GetByDefault<TResult>(Expression<Func<UserPassword, TResult>> selector, Expression<Func<UserPassword, bool>> expression, Func<IQueryable<UserPassword>, IIncludableQueryable<UserPassword, object>> include = null)
        {
            IQueryable<UserPassword> query = _table;   // tablomuuzu sorgulanabilir T tipini barındıran bir tablo olarak atadık.
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

        public List<TResult> GetByDefaults<TResult>(Expression<Func<UserPassword, TResult>> selector, Expression<Func<UserPassword, bool>> expression, Func<IQueryable<UserPassword>, IIncludableQueryable<UserPassword, object>> include = null, Func<IQueryable<UserPassword>, IOrderedQueryable<UserPassword>> orderBy = null)
        {
            IQueryable<UserPassword> query = _table;   // tablomuuzu sorgulanabilir T tipini barındıran bir tablo olarak atadık.
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

        public UserPassword GetDefault(Expression<Func<UserPassword, bool>> expression)
        {
            return _table.Where(expression).FirstOrDefault();
        }

        public List<UserPassword> GetDefaults(Expression<Func<UserPassword, bool>> expression)
        {
            return _table.Where(expression).ToList();
        }
    }
}
