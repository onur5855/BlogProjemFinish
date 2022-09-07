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
    public class ArticleCategoryRepository :  IArticleCategoryRepository
    {


        private readonly ProjectContext context;
        private readonly DbSet<ArticleCategory> _table;

        public ArticleCategoryRepository(ProjectContext context)
        {
            this.context = context;
            _table = context.Set<ArticleCategory>();
        }

        
        public void Create(ArticleCategory entity)
        {
            _table.Add(entity);
            
            context.SaveChanges();
        }

        public void Delete(ArticleCategory entity)
        {
            _table.Remove(entity);
            context.SaveChanges();
        }
        public void Deletee(ArticleCategory entity)
        {
            using (context)
            {
                context.ArticleCategories.Remove(entity);                
                context.SaveChanges();
            }
           
        }
        public void Update(ArticleCategory entity)
        {
            _table.Update(entity);
            context.SaveChanges();
        }

       

       


        public TResult GetByDefault<TResult>(Expression<Func<ArticleCategory, TResult>> selector, Expression<Func<ArticleCategory, bool>> expression, Func<IQueryable<ArticleCategory>, IIncludableQueryable<ArticleCategory, object>> include = null)
        {
            IQueryable<ArticleCategory> query = _table;   // tablomuuzu sorgulanabilir T tipini barındıran bir tablo olarak atadık.
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

        public List<TResult> GetByDefaults<TResult>(Expression<Func<ArticleCategory, TResult>> selector, Expression<Func<ArticleCategory, bool>> expression, Func<IQueryable<ArticleCategory>, IIncludableQueryable<ArticleCategory, object>> include = null, Func<IQueryable<ArticleCategory>, IOrderedQueryable<ArticleCategory>> orderBy = null)
        {
            IQueryable<ArticleCategory> query = _table;   // tablomuuzu sorgulanabilir T tipini barındıran bir tablo olarak atadık.
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
        public ArticleCategory GetDefault(Expression<Func<ArticleCategory, bool>> expression)
        {
            return _table.Where(expression).FirstOrDefault();
        }

        public List<ArticleCategory> GetDefaults(Expression<Func<ArticleCategory, bool>> expression)
        {
            return _table.Where(expression).ToList();
            
        }

        
    }
}
