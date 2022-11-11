using BlogProjem.Model.Entities.Concrete;

namespace BlogProjem.Web.Areas.Admin.Models.VMs
{
    public class CommentArticleVm
    {
    
        public int ArticleId { get; set; }
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
