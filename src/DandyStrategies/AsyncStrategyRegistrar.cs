using Microsoft.Extensions.DependencyInjection;

namespace DandyStrategies;

public sealed class AsyncStrategyRegistrar<TDefinition>(IServiceCollection _services) : IAsyncStrategyRegistrar<TDefinition>
    where TDefinition : IAsyncStrategyDefinition
{
    public IAsyncStrategyRegistrar<TDefinition> AddStrategy(object key, Type strategyType)
    {
        var interfaceType = typeof(IAsyncStrategy<TDefinition>);
        if (!strategyType.IsAssignableTo(interfaceType))
            throw new InvalidOperationException($"Type '{strategyType}' does not implement '{interfaceType}'.");

        _services.AddKeyedTransient(interfaceType, key, strategyType);
        return this;
    }

    public IAsyncStrategyRegistrar<TDefinition> AddStrategy<TStrategy>(object key)
        where TStrategy : class, IAsyncStrategy<TDefinition>
    {
        _services.AddKeyedTransient<IAsyncStrategy<TDefinition>, TStrategy>(key);
        return this;
    }
}

public sealed class AsyncStrategyRegistrar<TDefinition, TReturn>(IServiceCollection _services) : IAsyncStrategyRegistrar<TDefinition, TReturn>
    where TDefinition : IAsyncStrategyDefinition<TReturn>
{
    public IAsyncStrategyRegistrar<TDefinition, TReturn> AddStrategy(object key, Type strategyType)
    {
        var interfaceType = typeof(IAsyncStrategy<TDefinition, TReturn>);
        if (!strategyType.IsAssignableTo(interfaceType))
            throw new InvalidOperationException($"Type '{strategyType}' does not implement '{interfaceType}'.");

        _services.AddKeyedTransient(interfaceType, key, strategyType);
        return this;
    }

    public IAsyncStrategyRegistrar<TDefinition, TReturn> AddStrategy<TStrategy>(object key)
        where TStrategy : class, IAsyncStrategy<TDefinition, TReturn>
    {
        _services.AddKeyedTransient<IAsyncStrategy<TDefinition, TReturn>, TStrategy>(key);
        return this;
    }
}
