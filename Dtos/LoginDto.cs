namespace InstagramCloneAPI.Dtos
{
    public class LoginDto
    {
        // LoginId can be username, email, or phone number
        public string LoginId { get; set; }
        public string Password { get; set; }
    }
}
