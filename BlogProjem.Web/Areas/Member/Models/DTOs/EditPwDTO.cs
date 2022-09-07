namespace BlogProjem.Web.Areas.Member.Models.DTOs
{
    public class EditPwDTO
    {
        public int Id { get; set; }
        public string identityId { get; set; }
        public string PW { get; set; }
        public string NewPassword { get; set; }
        public string ConfirumNewPassword { get; set; }
    }
}
