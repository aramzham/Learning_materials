namespace RawJsonImplementation.Models;

public class ChatMessage
{
    public required ChatRole Role { get; set; }
    public required string Content { get; set; }
}