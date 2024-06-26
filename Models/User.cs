namespace InstagramCloneAPI.Models
{
    public class User
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string PasswordHash { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // public string PhoneNumber { get; set; } = string.Empty;
        //
        // public bool EmailVerified { get; set; } = false;
        //
        // public bool PhoneNumberVerified { get; set; } = false;
        //
        // public string FirstName { get; set; } = string.Empty;
        //
        // public string LastName { get; set; } = string.Empty;
        //
        // public DateTime DateOfBirth { get; set; }
        //
        // public DateTime DateCreated { get; set; }
        //
        // public DateTime LastLogin { get; set; }
        //
        // public string ProfilePictureURL { get; set; }

    }
}