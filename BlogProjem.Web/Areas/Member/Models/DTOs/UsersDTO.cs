namespace BlogProjem.Web.Areas.Member.Models.DTOs
{
    public class UsersDTO
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => FirstName + " " + LastName;
        public string ImagePath { get; set; }
    }
}
