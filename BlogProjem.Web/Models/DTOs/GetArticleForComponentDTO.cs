using BlogProjem.Model.Entities.Concrete;

namespace BlogProjem.Web.Models.DTOs
{
    public class GetArticleForComponentDTO
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public List<Like> Like { get; set; }
        public List<Comment> Comments { get; set; }
        public string CategoryName { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Image { get; set; }

        public string UserFullName { get; set; }

        public string UserImage { get; set; }

        public int UserId { get; set; }//eğer bu yapıdan yazarı profil sayfasına gitmek istersek id bilgisini tutmalıyız.

        public int ArticleID { get; set; } //Eğer Bu yapıdan makalenin tümü okumak için detay sayfasına gitmek istersek makale id de tutmalı

    }
}
