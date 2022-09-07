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
    public class Article:BaseEntity
    {
        public Article()
        {
            Comments = new List<Comment>();
            Likes = new List<Like>();
            ArticleCategories = new List<ArticleCategory>();
        }
        public string Title { get; set; }

        public string Content { get; set; }

        public string ImagePath { get; set; }  // dosya yolunu tutacak

        [NotMapped]
        public IFormFile Image { get; set; } // dosyayı okuyup,kaydedecek.

        public int views { get; set; }

        //naviigation Property

        // 1 makalenin 1 oluşturucusu
        public int AppUserId { get; set; }

        public AppUser AppUser { get; set; }

        // 1 makalenin 1 kategorisi // 1 makalenin çokca kategorisi olur
        //public int CategoryId { get; set; }
       // public Category Category { get; set; }
        public List<ArticleCategory> ArticleCategories { get; set; } //burası eklendi

        // 1 makalenin çokça yorumu
        public List<Comment> Comments { get; set; }

        // 1 makalenin çokça beğenisi
        public List<Like> Likes { get; set; }


    }
}
