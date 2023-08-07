using JsonTranscodingWith.Net7.GrpcService.Data;
using JsonTranscodingWith.Net7.GrpcService.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc().AddJsonTranscoding();
builder.Services.AddDbContextPool<AppDbContext>(o => o.UseSqlite("Data Source=ToDoDatabase.db"));

var app = builder.Build();

app.MapGrpcService<GreeterService>();
app.MapGrpcService<ToDoService>();

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
