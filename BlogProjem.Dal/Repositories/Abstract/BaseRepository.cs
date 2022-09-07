using BlogProjem.Dal.Context;
using BlogProjem.Dal.Repositories.Interfaces.Abstract;
using BlogProjem.Model.Entities.Abstract;
using BlogProjem.Model.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjem.Dal.Repositories.Abstract
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {


        // işlemleri gerçekleştrirken bizim context sınıfına ihtiyacımız var bu yüzden context sınıfını constructordan eneksiyon ile alacağız. (Dependency Injection - DI)

        // PROTECTED !!! 
        private readonly ProjectContext _context;
        private readonly DbSet<T> _table;

        public BaseRepository(ProjectContext context)
        {
            _context = context;
            _table = _context.Set<T>();// context sınıfının içindeki DBSETlere ulaşacağız.
        }

        public void Create(T entity)
        {
            _table.Add(entity);          
            
            _context.SaveChanges();
        }

        public void Delete(T entity)
        {
            entity.Statu = Statu.Passive;
            entity.RemovedDate = DateTime.Now;
            _context.SaveChanges();
        }

        public TResult GetByDefault<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> expression, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = _table;   // tablomuuzu sorgulanabilir T tipini barındıran bir tablo olarak atadık.
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

        public List<TResult> GetByDefaults<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> expression, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            IQueryable<T> query = _table;   // tablomuuzu sorgulanabilir T tipini barındıran bir tablo olarak atadık.
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
        public T GetDefault(Expression<Func<T, bool>> expression)
        {
            return _table.Where(expression).FirstOrDefault();
        }

        public List<T> GetDefaults(Expression<Func<T, bool>> expression)
        {
            return _table.Where(expression).ToList();
        }

        public void Update(T entity)
        {
            entity.Statu = Statu.Modified;
            entity.ModifiedDate = DateTime.Now;
            _table.Update(entity);
            _context.SaveChanges();
        }



    }
}
