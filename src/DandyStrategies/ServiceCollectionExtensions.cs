using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace DandyStrategies;

public static class ServiceCollectionExtensions
{
    private static readonly IReadOnlyList<Type> _serviceTypes =
    [
        typeof(IStrategy<>),
        typeof(IStrategy<,>),
    ];

    public static IServiceCollection AddDandyStrategies(this IServiceCollection services, Action<StrategiesConfigurationBuilder>? configuration = null)
    {
        var builder = new StrategiesConfigurationBuilder(services);
        configuration?.Invoke(builder);
        var cfg = builder.Build();

        services.AddTransient<IStrategyMediator, StrategyMediator>();
        AddServicesFromAssemblies(services, cfg.Assemblies);

        return services;
    }

    private static void AddServicesFromAssemblies(IServiceCollection services, IReadOnlyList<Assembly> assemblies)
    {
        var handlerTypes = assemblies.SelectMany(a => a.DefinedTypes).Where(t => t.IsClass && !t.IsAbstract && !t.IsGenericTypeDefinition);
        foreach (var implementationType in handlerTypes)
        {
            var interfaces = implementationType.ImplementedInterfaces;
            foreach (var @interface in interfaces)
            {
                if (!@interface.IsGenericType)
                    continue;

                var genericDefinition = @interface.GetGenericTypeDefinition();
                if (_serviceTypes.Contains(genericDefinition))
                    services.AddTransient(@interface, implementationType);
            }
        }
    }

    public static IServiceCollection AddStategyDefinition<TDefinition>(this IServiceCollection services, Action<IStrategyRegistrar<TDefinition>> definition)
        where TDefinition : IStrategyDefinition
    {
        definition(new StrategyRegistrar<TDefinition>(services));
        return services;
    }

    public static IServiceCollection AddStategyDefinition<TDefinition, TReturn>(this IServiceCollection services, Action<IStrategyRegistrar<TDefinition, TReturn>> definition)
        where TDefinition : IStrategyDefinition<TReturn>
    {
        definition(new StrategyRegistrar<TDefinition, TReturn>(services));
        return services;
    }
}
