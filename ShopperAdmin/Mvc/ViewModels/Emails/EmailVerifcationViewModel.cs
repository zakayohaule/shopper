namespace ShopperAdmin.Mvc.ViewModels.Emails
{
    public class EmailVerificationViewModel
    {
        public long UserId { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string VerificationLink { get; set; }
    }
}
