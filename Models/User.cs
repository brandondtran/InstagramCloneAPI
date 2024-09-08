using System.ComponentModel.DataAnnotations.Schema;

namespace InstagramCloneAPI.Models
{
    public class User
    {
        [Column(TypeName = "varchar(36)")]
        public string Id { get; set; }
        public string PasswordHash { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        // public string PhoneNumber { get; set; }
        //
        // public bool EmailVerified { get; set; }
        //
        // public bool PhoneNumberVerified { get; set; }
        //
        // public string FirstName { get; set; }
        //
        // public string LastName { get; set; }
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