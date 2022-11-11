using BlogProjem.Model.Entities.Concrete;

namespace BlogProjem.Web.Areas.Admin.Models.VMs
{
    public class ArticleVm
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public DateTime CreatTime { get; set; }

        public string Content { get; set; }

        public string ImagePath { get; set; }  // dosya yolunu tutacak
        public int AppUserId { get; set; }
        public int Views { get; set; }
        public int ReadingTime { get; set; }

        public AppUser AppUser { get; set; }
        public List<Comment> Comments { get; set; }

        // 1 makalenin çokça beğenisi
        public List<Like> Likes { get; set; }
        public List<ArticleCategory> ArticleCategories { get; set; } //burası eklendi
    }
}
