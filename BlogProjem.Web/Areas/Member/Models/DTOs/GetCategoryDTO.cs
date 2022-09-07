using System.ComponentModel.DataAnnotations;

namespace BlogProjem.Web.Areas.Member.Models.DTOs
{
    public class GetCategoryDTO
    {
        //her bir kategori için o kategoriiye ait yalnızca id ve isim bilgisinin taşınması için bu dto yu oluşturmayı tercih ettik

        public int ID { get; set; } //Categoryıd

        public string Name { get; set; }// CategoryName
        
        public bool IsSelected { get; set; }



    }
}
