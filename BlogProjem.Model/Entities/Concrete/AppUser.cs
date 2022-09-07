using BlogProjem.Model.Entities.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProjem.Model.Entities.Concrete
{
    public class AppUser:BaseEntity
    {

        public AppUser()
        {
            Articles = new List<Article>();
            Comments = new List<Comment>();
            UserFollowCategories = new List<UserFollowCategory>();
            Likes = new List<Like>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        // aynı kullanıcının hem identity kütüphanei tarafında hemde burada kimliği var bunları beraberce eşlemek için kişinin idenetityId sini burada prop olarak kullandık.

        public string IdentityId { get; set; }
        public string FullName => FirstName + " " + LastName;
        public string ImagePath { get; set; }

        [NotMapped] // NotMapped => bu prop sql tarafında kolon olmayacak !
        public IFormFile Image { get; set; }

        //NAVİGATİON PROPERTİES
        // navigation propertyde defaultta eager yaptığı düşünülür ancak VİRTUAL anahtar kelimesi ile LAZY LOADİNG yapılacağı anlaşılır.
        // Biz bu projede EAGER LOADING yapacağımız için  virtualı kullanmayacağız.

        // 1 kullanıcı çokça makale
        public List<Article> Articles { get; set; }

        //1 kullaıcının çokça yorumu olabilir

        public List<Comment> Comments { get; set; }

        //1 kullanıcının çokça beeğeniis olabilir.

        public List<Like> Likes { get; set; }

        // 1 user çokça kategoriyi takip etmek isteyebilir.

        public List<UserFollowCategory> UserFollowCategories { get; set; }

       // public int UserPaswordId { get; set; }
        public List< UserPassword> userPasswords{ get; set; }

    }
}
