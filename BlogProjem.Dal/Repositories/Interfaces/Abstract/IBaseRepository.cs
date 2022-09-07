using BlogProjem.Model.Entities.Abstract;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjem.Dal.Repositories.Interfaces.Abstract
{
    // bu interfacesimizi genericType yapmış olduk ve  T bu interface için BaseEntitydir.
    public interface IBaseRepository<T> where T : BaseEntity
    {

        //CRUD

        void Create(T entity);

        void Update(T entity);

        void Delete(T entity);

        // tek bir T tipli nesne dönecek.
        T GetDefault(Expression<Func<T, bool>> expression);

        // Aynı sorgudan dönen T tipli nesneleri barındıran liste yapısını döner.
        List<T> GetDefaults(Expression<Func<T, bool>> expression);  // expression=sorgu cümlesi



        // bildiğimiz entitylerden olmayan kendimizin oluşturduğu DTO yada VM tipinde TEK bir nesneyi döndürecek olan bir metottur.


        TResult GetByDefault<TResult>
            (
                Expression<Func<T, TResult>> selector,        // seçim
                Expression<Func<T, bool>> expression,         // sorgu /filtreleme
                Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null  //içermesini istediğimiz tablolar/ include ettiğimiz,edeceğimiz tabloları burada söylüyoruz ki eğer böyle tablolar yoksa bu parametre null bırakılabilir bir parametre aynı zamanda.
            );


        // yine TResult tipinde nesneleri bu kez list yapısı içinde belli sorgular dahilinde dönen bir metottur.
        List<TResult> GetByDefaults<TResult>
            (
            Expression<Func<T, TResult>> selector,    // seçim
            Expression<Func<T, bool>> expression,     // sorgu
            Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,   // include etme - nullable
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null          //  sıralama - order- nullable

            );



    }
}
