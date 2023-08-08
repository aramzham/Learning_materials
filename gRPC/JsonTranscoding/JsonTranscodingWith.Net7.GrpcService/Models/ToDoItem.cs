namespace JsonTranscodingWith.Net7.GrpcService.Models;

public class ToDoItem
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public ToDoStatus ToDoStatus { get; set; }
}