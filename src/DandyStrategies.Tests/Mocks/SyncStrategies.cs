namespace DandyStrategies.Tests.Mocks;

internal static class SyncStrategies
{
    public sealed class Definition(object key, StrategyAssertHelper helper) : StrategyDefinition(key)
    {
        public StrategyAssertHelper Helper { get; } = helper;
    }

    [StrategyKey("strat-a")]
    public sealed class StrategyA : IStrategy<Definition>
    {
        public void Execute(Definition definition)
        {
            definition.Helper.ExecutedStrategy = definition.Key;
        }
    }
    
    [StrategyKey("strat-b")]
    public sealed class StrategyB : IStrategy<Definition>
    {
        public void Execute(Definition definition)
        {
            definition.Helper.ExecutedStrategy = definition.Key;
        }
    }
}
