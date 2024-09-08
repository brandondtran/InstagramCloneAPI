using System.ComponentModel.DataAnnotations.Schema;

namespace InstagramCloneAPI.Models
{
    public class Chat
    {
        [Column(TypeName = "varchar(36)")]
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> UserIds { get; set; }
        public List<Message> Messages { get; set; }
    }

    public class Message
    {
        [Column(TypeName = "varchar(36)")]
        public string Id { get; set; }
        public string Content { get; set; }
        public string SenderId { get; set; }
        public DateTime Timestamp { get; set; }
        public string ChatId { get; set; } // Foreign key
        public Chat Chat { get; set; } // Navigation property
    }
}