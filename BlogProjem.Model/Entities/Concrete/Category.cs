using BlogProjem.Model.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjem.Model.Entities.Concrete
{
    public class Category:BaseEntity
    {

        public Category()
        {
            //Articles = new List<Article>();
            UserFollowCategories = new List<UserFollowCategory>();
            ArticleCategories = new List<ArticleCategory>(); // burası eklendi
        }

        public string Name { get; set; }
        public string Description { get; set; }

        // navigation Property

        // 1 kategorinin çokça makalesi olabilir.
        //public List<Article> Articles { get; set; }
        public List<ArticleCategory> ArticleCategories { get; set; } //burası eklendi 

        // 1 kategori çokça kullanıcı tarafından takip edilebilir.

        public List<UserFollowCategory> UserFollowCategories { get; set; }


    }
}
