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
    public interface IUserPasswordRepository
    {

        void Create(UserPassword entity);

        // tek bir T tipli nesne dönecek.
        UserPassword GetDefault(Expression<Func<UserPassword, bool>> expression);

        // Aynı sorgudan dönen T tipli nesneleri barındıran liste yapısını döner.
        List<UserPassword> GetDefaults(Expression<Func<UserPassword, bool>> expression);  // expression=sorgu cümlesi



        // bildiğimiz entitylerden olmayan kendimizin oluşturduğu DTO yada VM tipinde TEK bir nesneyi döndürecek olan bir metottur.


        TResult GetByDefault<TResult>
            (
                Expression<Func<UserPassword, TResult>> selector,        // seçim
                Expression<Func<UserPassword, bool>> expression,         // sorgu /filtreleme
                Func<IQueryable<UserPassword>, IIncludableQueryable<UserPassword, object>> include = null  //içermesini istediğimiz tablolar/ include ettiğimiz,edeceğimiz tabloları burada söylüyoruz ki eğer böyle tablolar yoksa bu parametre null bırakılabilir bir parametre aynı zamanda.
            );


        // yine TResult tipinde nesneleri bu kez list yapısı içinde belli sorgular dahilinde dönen bir metottur.
        List<TResult> GetByDefaults<TResult>
            (
            Expression<Func<UserPassword, TResult>> selector,    // seçim
            Expression<Func<UserPassword, bool>> expression,     // sorgu
            Func<IQueryable<UserPassword>, IIncludableQueryable<UserPassword, object>> include = null,   // include etme - nullable
            Func<IQueryable<UserPassword>, IOrderedQueryable<UserPassword>> orderBy = null          //  sıralama - order- nullable

            );


    }
}
