namespace InstagramCloneAPI.Dtos;

public class ChatDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public List<string> UserIds { get; set; }
    public List<MessageDto> Messages { get; set; }
}

public class MessageDto
{
    public string Id { get; set; }
    public string SenderId { get; set; }
    public string Content { get; set; }
    public DateTime Timestamp { get; set; }
}    
