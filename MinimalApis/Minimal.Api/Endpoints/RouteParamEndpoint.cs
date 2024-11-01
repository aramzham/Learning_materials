using Minimal.Api.Services;

namespace Minimal.Api.Endpoints;

public class RouteParamEndpoint : IEndpoint
{
    public static void DefineEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("hello/{name}", HelloName)
            .WithName("Hello");
        app.MapGet("add/{num1:int}/{num2:int}", Add);
        app.MapGet("regex/{name:regex(^[a-z0-9]+$)}", NameRegex);
        app.MapGet("length/{str:length(5)}", Length);
        app.MapGet("optional/{name?}", Optional);
        app.MapGet("default/{name=world}", Default);
        app.MapGet("multi-segment/{**slug}", MultiSegment);
            // /multi-segment/abc                    → slug = "abc"
            // /multi-segment/abc/def                → slug = "abc/def"
            // /multi-segment/abc/def/ghi            → slug = "abc/def/ghi"
            // /multi-segment/products/categories/123 → slug = "products/categories/123"

        app.MapGet("service", Service);
    }

    public static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<ISimpleService, SimpleService>();
    }
    
    private static IResult HelloName(string name)
    {
        return TypedResults.Ok($"Hello, {name}");
    }
    
    private static IResult Add(int num1, int num2)
    {
        // if you don't add the route constraint and provide a string or anything other than a number, you'll get 404
        return TypedResults.Ok($"{num1} + {num2} = {num1 + num2}");
    }
    
    private static IResult NameRegex(string name)
    {
        return TypedResults.Ok($"Hello, {name}");
    }
    
    private static IResult Length(string str)
    {
        return TypedResults.Ok($"Length of {str} is {str.Length}");
    }
    
    private static IResult Optional(string? name)
    {
        return TypedResults.Ok($"Hello, {name ?? "Guest"}");
    }
    
    private static IResult Default(string name)
    {
        return TypedResults.Ok($"Hello, {name}");
    }
    
    private static IResult MultiSegment(string slug)
    {
        return TypedResults.Ok($"Slug: {slug}");
    }
    
    private static IResult Service(ISimpleService service)
    {
        return TypedResults.Ok($"Message from service: {service.GetMessage()}");
    }
}