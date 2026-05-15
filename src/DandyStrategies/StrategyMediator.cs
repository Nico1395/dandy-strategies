using Microsoft.Extensions.DependencyInjection;

namespace DandyStrategies;

internal sealed class StrategyMediator(IServiceProvider _serviceProvider) : IStrategyMediator
{
    public void Execute<TDefinition>(TDefinition definition)
        where TDefinition : IStrategyDefinition
    {
        var strategy = _serviceProvider.GetRequiredKeyedService<IStrategy<TDefinition>>(definition.Key);
        strategy.Execute(definition);
    }

    public TReturn Execute<TReturn>(IStrategyDefinition<TReturn> definition)
    {
        var interfaceType = typeof(IStrategy<,>).MakeGenericType(definition.GetType(), typeof(TReturn));
        var strategy = _serviceProvider.GetRequiredKeyedService(interfaceType, definition.Key);
        
        var executeMethod = interfaceType.GetMethod(nameof(IStrategy<,>.Execute)) ?? throw new InvalidOperationException($"Strategy '{interfaceType}' does not contain method with name '{nameof(IStrategy<,>.Execute)}'. This in an internal error, please report this error on GitHub.");
        var result = executeMethod.Invoke(strategy, [definition]);

        if (result is not TReturn castedResult)
            throw new InvalidCastException($"Result of strategy '{interfaceType}' is of type '{result?.GetType()}' but a result of type '{typeof(TReturn)}' was expected.");

        return castedResult;
    }

    public Task ExecuteAsync<TDefinition>(TDefinition definition, CancellationToken cancellationToken = default)
        where TDefinition : IAsyncStrategyDefinition
    {
        var strategy = _serviceProvider.GetRequiredKeyedService<IAsyncStrategy<TDefinition>>(definition.Key);
        return strategy.ExecuteAsync(definition, cancellationToken);
    }

    public Task<TReturn> ExecuteAsync<TReturn>(IAsyncStrategyDefinition<TReturn> definition, CancellationToken cancellationToken = default)
    {
        var interfaceType = typeof(IAsyncStrategy<,>).MakeGenericType(definition.GetType(), typeof(TReturn));
        var strategy = _serviceProvider.GetRequiredKeyedService(interfaceType, definition.Key);
        
        var executeMethod = interfaceType.GetMethod(nameof(IAsyncStrategy<,>.ExecuteAsync)) ?? throw new InvalidOperationException($"Strategy '{interfaceType}' does not contain method with name '{nameof(IAsyncStrategy<,>.ExecuteAsync)}'. This in an internal error, please report this error on GitHub.");
        var task = executeMethod.Invoke(strategy, [definition, cancellationToken]) as Task<TReturn> ?? throw new InvalidCastException($"Invoking strategy of type '{interfaceType}' should have returned a {typeof(Task<TReturn>)}.");

        return task;
    }
}
