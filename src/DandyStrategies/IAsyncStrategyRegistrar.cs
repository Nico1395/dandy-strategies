namespace DandyStrategies;

public interface IAsyncStrategyRegistrar<TDefinition>
    where TDefinition : IAsyncStrategyDefinition
{
    IAsyncStrategyRegistrar<TDefinition> AddStrategy(object key, Type strategyType);
    IAsyncStrategyRegistrar<TDefinition> AddStrategy<TStrategy>(object key) where TStrategy : class, IAsyncStrategy<TDefinition>;
}

public interface IAsyncStrategyRegistrar<TDefinition, TReturn>
    where TDefinition : IAsyncStrategyDefinition<TReturn>
{
    IAsyncStrategyRegistrar<TDefinition, TReturn> AddStrategy(object key, Type strategyType);
    IAsyncStrategyRegistrar<TDefinition, TReturn> AddStrategy<TStrategy>(object key) where TStrategy : class, IAsyncStrategy<TDefinition, TReturn>;
}
