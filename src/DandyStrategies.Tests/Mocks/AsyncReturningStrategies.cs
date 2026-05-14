namespace DandyStrategies.Tests.Mocks;

internal static class AsyncReturningStrategies
{
    public sealed class Definition(object key) : AsyncStrategyDefinition<object>(key);

    [StrategyKey("strat-a")]
    public sealed class StrategyA : IStrategy<Definition, Task<object>>
    {
        public Task<object> Execute(Definition definition)
        {
            return Task.FromResult(definition.Key);
        }
    }

    [StrategyKey("strat-b")]
    public sealed class StrategyB : IStrategy<Definition, Task<object>>
    {
        public Task<object> Execute(Definition definition)
        {
            return Task.FromResult(definition.Key);
        }
    }
}
