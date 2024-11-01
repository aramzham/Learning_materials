using System.Reflection;

namespace Minimal.Api.Endpoints;

public static class EndpointExtensions
{
    public static void AddMinimalEndpoints<TMarker>(this IServiceCollection services, IConfiguration configuration)
    {
        AddMinimalEndpoints(services, typeof(TMarker), configuration);
    }

    public static void AddMinimalEndpoints(this IServiceCollection services, Type typeMarker,
        IConfiguration configuration)
    {
        var endpointTypes = GetEndpointTypesFromAssemblyContaining(typeMarker);

        foreach (var endpointType in endpointTypes)
        {
            endpointType.GetMethod(nameof(IEndpoint.AddServices))!
                .Invoke(null, [services, configuration]);
        }
    }

    public static void UseMinimalEndpoints<TMarker>(this IApplicationBuilder app)
    {
        UseMinimalEndpoints(app, typeof(TMarker));
    }

    public static void UseMinimalEndpoints(this IApplicationBuilder app, Type typeMarker)
    {
        var endpointTypes = GetEndpointTypesFromAssemblyContaining(typeMarker);

        foreach (var endpointType in endpointTypes)
        {
            endpointType.GetMethod(nameof(IEndpoint.DefineEndpoints))!
                .Invoke(null, [app]);
        }
    }

    private static IEnumerable<TypeInfo> GetEndpointTypesFromAssemblyContaining(Type typeMarker)
    {
        var endpointTypes = typeMarker.Assembly.DefinedTypes
            .Where(t => t is { IsAbstract: false, IsInterface: false } && typeof(IEndpoint).IsAssignableFrom(t));
        return endpointTypes;
    }
}