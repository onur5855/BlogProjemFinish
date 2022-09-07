using BlogProjem.Model.Entities.Concrete;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjem.Dal.Repositories.Interfaces.Concrete
{
    public interface IArticleCategoryRepository
    {
       
        void Create(ArticleCategory entity);

        void Delete(ArticleCategory entity);
        void Deletee(ArticleCategory entity);
        void Update(ArticleCategory entity);


        

        // tek bir T tipli nesne dönecek.
        ArticleCategory GetDefault(Expression<Func<ArticleCategory, bool>> expression);

        // Aynı sorgudan dönen T tipli nesneleri barındıran liste yapısını döner.
        List<ArticleCategory> GetDefaults(Expression<Func<ArticleCategory, bool>> expression);  // expression=sorgu cümlesi



        // bildiğimiz entitylerden olmayan kendimizin oluşturduğu DTO yada VM tipinde TEK bir nesneyi döndürecek olan bir metottur.


        TResult GetByDefault<TResult>
            (
                Expression<Func<ArticleCategory, TResult>> selector,        // seçim
                Expression<Func<ArticleCategory, bool>> expression,         // sorgu /filtreleme
                Func<IQueryable<ArticleCategory>, IIncludableQueryable<ArticleCategory, object>> include = null  //içermesini istediğimiz tablolar/ include ettiğimiz,edeceğimiz tabloları burada söylüyoruz ki eğer böyle tablolar yoksa bu parametre null bırakılabilir bir parametre aynı zamanda.
            );


        // yine TResult tipinde nesneleri bu kez list yapısı içinde belli sorgular dahilinde dönen bir metottur.
        List<TResult> GetByDefaults<TResult>
            (
            Expression<Func<ArticleCategory, TResult>> selector,    // seçim
            Expression<Func<ArticleCategory, bool>> expression,     // sorgu
            Func<IQueryable<ArticleCategory>, IIncludableQueryable<ArticleCategory, object>> include = null,   // include etme - nullable
            Func<IQueryable<ArticleCategory>, IOrderedQueryable<ArticleCategory>> orderBy = null          //  sıralama - order- nullable

            );





    }
}
