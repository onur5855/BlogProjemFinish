namespace BlogProjem.Web.Areas.Admin.Models.DTOs
{
    public class AdminsDTO
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => FirstName + " " + LastName;
        public string ImagePath { get; set; }
    }
}
