using BlogProjem.Model.Entities.Concrete;
using BlogProjem.Web.Areas.Member.Models.DTOs;
using BlogProjem.Web.Areas.Member.Models.ValidasyonControl;

namespace BlogProjem.Web.Areas.Member.Models.VMs
{
    public class UpdateArticleVm
    {

        public int ID { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string? ImagePath { get; set; }

        public IFormFile? Image { get; set; }// resmini değiştirmek isterse diye
        //public List<SelectedCategoryDTO> selectedCategoryDTOs { get; set; }
        [CategoryValidation(ErrorMessage = "kategori seçiniz")]
        public List<GetCategoryDTO> getCategoryDTOs { get; set; }


        public int AppUserID { get; set; } //kullanıcının idsi bilinsinki updatede sıkntı çıkmasın diye düşündüm
    }
}
