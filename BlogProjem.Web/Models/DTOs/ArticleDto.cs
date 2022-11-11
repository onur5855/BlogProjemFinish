using BlogProjem.Model.Entities.Concrete;

namespace BlogProjem.Web.Models.DTOs
{
    public class ArticleDto
    {

        
        public int ArticleId { get; set; }
        public Article Article { get; set; }
        public string Title { get; set; }
        public int views { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ImagePath { get; set; }

        public int AppUserId { get; set; }

        public AppUser AppUser { get; set; }

        //public List<CommentDTO> commentDTOs { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Like> Likes { get; set; }

    }
}
