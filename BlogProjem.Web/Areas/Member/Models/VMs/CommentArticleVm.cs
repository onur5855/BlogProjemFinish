using BlogProjem.Model.Entities.Concrete;
using BlogProjem.Web.Areas.Member.Models.DTOs;
using System.ComponentModel.DataAnnotations;

namespace BlogProjem.Web.Areas.Member.Models.VMs
{
    public class CommentArticleVm
    {
        public int IdentitId { get; set; }
        public int ArticleId { get; set; }
        public Article Article { get; set; }
        public string Title { get; set; }
        public int views { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ImagePath { get; set; }

        public int AppUserId { get; set; }

        public string UserFullName { get; set; }

        //[Required(ErrorMessage ="bu alan boş olamaz")]
        public string? Text { get; set; }

        public AppUser AppUser { get; set; }

        //public List<CommentDTO> commentDTOs { get; set; }
        //[Required(ErrorMessage = "bu alan boş olamaz")]
        public List<Comment>? Comments { get; set; }
        public List<Like> Likes { get; set; }

    }
}
