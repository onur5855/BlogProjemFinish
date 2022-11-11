namespace BlogProjem.Web.Areas.Admin.Models.DTOs
{
    public class PassiveUserListDTO
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => FirstName + " " + LastName;
        public string ImagePath { get; set; }
        public string UserName { get; set; }
    }
}
