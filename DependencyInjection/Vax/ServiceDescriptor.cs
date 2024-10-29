namespace Vax;

public class ServiceDescriptor
{
    public Type ServiceType { get; set; }
    public Type ImplementationType { get; set; }
    public object? ImplementationInstance { get; set; }
    public ServiceLifetime Lifetime { get; set; }
    public Func<ServiceProvider, object>? ImplementationFactory { get; set; }
}