using Grpc.Core;
using GrpcService;
using JsonTranscodingWith.Net7.GrpcService.Data;
using JsonTranscodingWith.Net7.GrpcService.Models;
using Microsoft.EntityFrameworkCore;
using ToDoStatus = JsonTranscodingWith.Net7.GrpcService.Models.ToDoStatus;

namespace JsonTranscodingWith.Net7.GrpcService.Services;

public class ToDoService : ToDoItService.ToDoItServiceBase
{
    private readonly AppDbContext _dbContext;

    public ToDoService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override async Task<CreateToDoResponse> CreateToDo(CreateToDoRequest request, ServerCallContext context)
    {
        if (string.IsNullOrEmpty(request.Title))
            throw new RpcException(new Status(StatusCode.InvalidArgument, nameof(request.Title)),
                "Input object is not valid");

        var todo = new ToDoItem()
        {
            Description = request.Description,
            Title = request.Title,
            ToDoStatus = ToDoStatus.InProgress
        };

        await _dbContext.ToDoItems.AddAsync(todo);
        await _dbContext.SaveChangesAsync();

        return new CreateToDoResponse()
        {
            Id = todo.Id
        };
    }

    public override async Task<ReadToDoResponse> ReadOneToDo(ReadToDoRequest request, ServerCallContext context)
    {
        if (request.Id <= 0)
            throw new RpcException(new Status(StatusCode.InvalidArgument, nameof(request.Id)),
                "Input object is not valid");

        var todoInDb = await _dbContext.ToDoItems.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (todoInDb is null)
            throw new RpcException(new Status(StatusCode.NotFound, nameof(request.Id)),
                "Input object is not valid");

        return new ReadToDoResponse()
        {
            Id = todoInDb.Id,
            Description = todoInDb.Description,
            Title = todoInDb.Title,
            ToDoStatus = (global::GrpcService.ToDoStatus)todoInDb.ToDoStatus
        };
    }

    public override async Task<ReadAllResponse> ReadAllToDo(ReadAllRequest request, ServerCallContext context)
    {
        var todosInDb = await _dbContext.ToDoItems.ToListAsync();

        return new ReadAllResponse()
        {
            Todos =
            {
                todosInDb.Select(x => new ReadToDoResponse()
                {
                    Description = x.Description,
                    Title = x.Title,
                    ToDoStatus = (global::GrpcService.ToDoStatus)x.ToDoStatus,
                    Id = x.Id
                })
            }
        };
    }

    public override async Task<UpdateToDoResponse> UpdateTodo(UpdateToDoRequest request, ServerCallContext context)
    {
        if (request.Id <= 0 || string.IsNullOrEmpty(request.Title))
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid input object"));
        
        var todoInDb = await  _dbContext.ToDoItems.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (todoInDb is null)
            throw new RpcException(new Status(StatusCode.NotFound, "Invalid input object"),
                "Invalid input object");

        todoInDb.Title = request.Title;
        todoInDb.Description = request.Description;
        todoInDb.ToDoStatus = (ToDoStatus)request.ToDoStatus;

        await _dbContext.SaveChangesAsync();

        return new UpdateToDoResponse()
        {
            Id = todoInDb.Id
        };
    }

    public override async Task<DeleteToDoResponse> DeleteToDo(DeleteToDoRequest request, ServerCallContext context)
    {
        if (request.Id <= 0)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid input object"));
        
        var todoInDb = await _dbContext.ToDoItems.FirstOrDefaultAsync(x => x.Id == request.Id);

        if (todoInDb is null)
            throw new RpcException(new Status(StatusCode.NotFound, "Invalid input object"),
                "Invalid input object");

        _dbContext.ToDoItems.Remove(todoInDb);
        await _dbContext.SaveChangesAsync();

        return new DeleteToDoResponse()
        {
            Id = todoInDb.Id
        };
    }
}