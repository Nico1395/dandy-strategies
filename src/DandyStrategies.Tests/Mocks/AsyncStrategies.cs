namespace DandyStrategies.Tests.Mocks;

internal static class AsyncStrategies
{
    public sealed class Definition(object key, StrategyAssertHelper helper) : AsyncStrategyDefinition(key)
    {
        public StrategyAssertHelper Helper { get; } = helper;
    }

    [StrategyKey("strat-a")]
    public sealed class StrategyA : IStrategy<Definition, Task>
    {
        public Task Execute(Definition definition)
        {
            definition.Helper.ExecutedStrategy = definition.Key;
            return Task.CompletedTask;
        }
    }

    [StrategyKey("strat-b")]
    public sealed class StrategyB : IStrategy<Definition, Task>
    {
        public Task Execute(Definition definition)
        {
            definition.Helper.ExecutedStrategy = definition.Key;
            return Task.CompletedTask;
        }
    }
}
