namespace Minimal.Api.Endpoints;

public interface IEndpoint
{
    public static abstract void DefineEndpoints(IEndpointRouteBuilder app);
    public static abstract void AddServices(IServiceCollection services, IConfiguration configuration);
}