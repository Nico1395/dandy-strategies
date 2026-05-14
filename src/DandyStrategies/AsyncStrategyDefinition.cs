namespace DandyStrategies;

public abstract class AsyncStrategyDefinition : IStrategyDefinition<Task>
{
    public AsyncStrategyDefinition(object key)
    {
        Key = key;
    }

    public object Key { get; }
}

public abstract class AsyncStrategyDefinition<TReturn> : IStrategyDefinition<Task<TReturn>>
{
    public AsyncStrategyDefinition(object key)
    {
        Key = key;
    }

    public object Key { get; }
}
