using JsonTranscodingWith.Net7.GrpcService.Models;
using Microsoft.EntityFrameworkCore;

namespace JsonTranscodingWith.Net7.GrpcService.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<ToDoItem> ToDoItems { get; set; }
}