namespace DandyStrategies;

public abstract class StrategyDefinition : IStrategyDefinition
{
    public StrategyDefinition(object key)
    {
        Key = key;
    }

    public object Key { get; }
}

public abstract class StrategyDefinition<TReturn> : IStrategyDefinition<TReturn>
{
    public StrategyDefinition(object key)
    {
        Key = key;
    }

    public object Key { get; }
}
