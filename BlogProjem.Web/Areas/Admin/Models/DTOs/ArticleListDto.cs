using BlogProjem.Model.Entities.Concrete;

namespace BlogProjem.Web.Areas.Admin.Models.DTOs
{
    public class ArticleListDto
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public DateTime CreatTime { get; set; }

        public string Content { get; set; }

        public string ImagePath { get; set; }  // dosya yolunu tutacak
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        
        public List<ArticleCategory> ArticleCategories { get; set; } //burası eklendi
    }
}
