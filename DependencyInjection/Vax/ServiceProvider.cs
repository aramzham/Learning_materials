namespace Vax;

public class ServiceProvider
{
    private Dictionary<Type, Func<object>> _transientServices = new();
    private Dictionary<Type, Lazy<object>> _singletonServices = new();

    internal ServiceProvider(ServiceCollection serviceCollection)
    {
        GenerateServices(serviceCollection);
    }

    public T? GetService<T>()
    {
        return (T?)GetService(typeof(T));
    }

    public object? GetService(Type serviceType)
    {
        return _singletonServices.TryGetValue(serviceType, out var lazy) 
            ? lazy.Value 
            : _transientServices.TryGetValue(serviceType, out var factory) 
                ? factory?.Invoke() 
                : null;
    }

    private void GenerateServices(ServiceCollection services)
    {
        foreach (var serviceDescriptor in services)
        {
            switch (serviceDescriptor.Lifetime)
            {
                case ServiceLifetime.Transient:
                    if (serviceDescriptor.ImplementationFactory is not null)
                    {
                        _transientServices[serviceDescriptor.ServiceType] =
                            () => serviceDescriptor.ImplementationFactory!(this);
                        continue;
                    }
                    
                    _transientServices[serviceDescriptor.ServiceType] =
                        () => Activator.CreateInstance(serviceDescriptor.ImplementationType,
                            GetConstructorParameters(serviceDescriptor))!;
                    continue;
                case ServiceLifetime.Singleton:
                    if (serviceDescriptor.ImplementationInstance is not null)
                    {
                        _singletonServices[serviceDescriptor.ServiceType] =
                            new Lazy<object>(() => serviceDescriptor.ImplementationInstance!);
                        continue;
                    }
                    
                    if (serviceDescriptor.ImplementationFactory is not null)
                    {
                        _singletonServices[serviceDescriptor.ServiceType] =
                            new Lazy<object>(() => serviceDescriptor.ImplementationFactory!(this));
                        continue;
                    }

                    _singletonServices[serviceDescriptor.ServiceType] = new Lazy<object>(() =>
                        Activator.CreateInstance(serviceDescriptor.ImplementationType,
                            GetConstructorParameters(serviceDescriptor))!);
                    continue;
            }
        }
    }

    private object?[] GetConstructorParameters(ServiceDescriptor serviceDescriptor)
    {
        var constructorInfo = serviceDescriptor.ImplementationType.GetConstructors().First();
        var parameters = constructorInfo.GetParameters();
        var parameterValues = new object?[parameters.Length];

        for (int i = 0; i < parameters.Length; i++)
        {
            var parameter = parameters[i];
            var parameterValue = GetService(parameter.ParameterType);
            parameterValues[i] = parameterValue;
        }

        return parameterValues;
    }
}