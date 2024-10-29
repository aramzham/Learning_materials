namespace Vax;

public class ServiceCollection : List<ServiceDescriptor>
{
    public ServiceProvider BuildServiceProvider()
    {
        return new ServiceProvider(this);
    }

    public ServiceCollection AddService(ServiceDescriptor descriptor)
    {
        Add(descriptor);
        return this;
    }

    public ServiceCollection AddSingleton<TService>()
        where TService : class
    {
        Add(typeof(TService), typeof(TService), ServiceLifetime.Singleton);
        return this;
    }

    public ServiceCollection AddSingleton<TService, TImplementation>()
        where TService : class
        where TImplementation : class, TService
    {
        Add(typeof(TService), typeof(TImplementation), ServiceLifetime.Singleton);
        return this;
    }

    public ServiceCollection AddSingleton(object implementation)
    {
        Add(new ServiceDescriptor()
        {
            Lifetime = ServiceLifetime.Singleton,
            ImplementationInstance = implementation,
            ServiceType = implementation.GetType(),
            ImplementationType = implementation.GetType()
        });
        return this;
    }

    public ServiceCollection AddSingleton<TService>(Func<ServiceProvider, TService> factory)
        where TService : class
    {
        Add(new ServiceDescriptor()
        {
            Lifetime = ServiceLifetime.Singleton,
            ServiceType = typeof(TService),
            ImplementationType = typeof(TService),
            ImplementationFactory = factory
        });
        return this;
    }
    
    public ServiceCollection AddTransient<TService>(Func<ServiceProvider, TService> factory)
        where TService : class
    {
        Add(new ServiceDescriptor()
        {
            Lifetime = ServiceLifetime.Transient,
            ServiceType = typeof(TService),
            ImplementationType = typeof(TService),
            ImplementationFactory = factory
        });
        return this;
    }

    public ServiceCollection AddTransient<TService>()
        where TService : class
    {
        Add(typeof(TService), typeof(TService), ServiceLifetime.Transient);
        return this;
    }

    public ServiceCollection AddTransient<TService, TImplementation>()
        where TService : class
        where TImplementation : class, TService
    {
        Add(typeof(TService), typeof(TImplementation), ServiceLifetime.Transient);
        return this;
    }

    private void Add(Type serviceType, Type implementationType, ServiceLifetime lifetime)
    {
        Add(new ServiceDescriptor()
        {
            Lifetime = lifetime,
            ServiceType = serviceType,
            ImplementationType = implementationType
        });
    }
}