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
        var strategyType = typeof(IStrategy<,>).MakeGenericType(definition.GetType(), typeof(TReturn));
        var strategy = _serviceProvider.GetRequiredKeyedService(strategyType, definition.Key);
        
        var executeMethod = strategyType.GetMethod(nameof(IStrategy<,>.Execute)) ?? throw new InvalidOperationException($"Strategy '{strategyType}' does not contain method with name '{nameof(IStrategy<,>.Execute)}'. This in an internal error, please report this error on GitHub");
        var result = executeMethod.Invoke(strategy, [definition]);

        if (result is not TReturn castedResult)
            throw new InvalidCastException($"Result of strategy '{strategyType}' is of type '{result?.GetType()}' but a result of type '{typeof(TReturn)}' was expected.");

        return castedResult;
    }

    public Task ExecuteAsync<TDefinition>(TDefinition definition, CancellationToken cancellationToken = default)
        where TDefinition : IStrategyDefinition<Task>
    {
        var strategyType = typeof(IStrategy<,>).MakeGenericType(definition.GetType(), typeof(Task));
        var strategy = _serviceProvider.GetRequiredKeyedService(strategyType, definition.Key);
        
        var executeMethod = strategyType.GetMethod(nameof(IStrategy<,>.Execute)) ?? throw new InvalidOperationException($"Strategy '{strategyType}' does not contain method with name '{nameof(IStrategy<,>.Execute)}'. This in an internal error, please report this error on GitHub");
        return executeMethod.Invoke(strategy, [definition, cancellationToken]) as Task ?? throw new InvalidCastException($"Invoking strategy of type '{strategyType}' should have returned a Task.");
    }

    public async Task<TReturn> ExecuteAsync<TReturn>(IStrategyDefinition<Task<TReturn>> definition, CancellationToken cancellationToken = default)
    {
        var strategyType = typeof(IStrategy<,>).MakeGenericType(definition.GetType(), typeof(Task<TReturn>));
        var strategy = _serviceProvider.GetRequiredKeyedService(strategyType, definition.Key);
        
        var executeMethod = strategyType.GetMethod(nameof(IStrategy<,>.Execute)) ?? throw new InvalidOperationException($"Strategy '{strategyType}' does not contain method with name '{nameof(IStrategy<,>.Execute)}'. This in an internal error, please report this error on GitHub");
        var task = executeMethod.Invoke(strategy, [definition, cancellationToken]) as Task<TReturn> ?? throw new InvalidCastException($"Invoking strategy of type '{strategyType}' should have returned a {typeof(Task<TReturn>)}.");

        var result = await task;
        if (result is not TReturn castedResult)
            throw new InvalidCastException($"Result of strategy '{strategyType}' is of type '{result?.GetType()}' but a result of type '{typeof(TReturn)}' was expected.");

        return castedResult;
    }
}
