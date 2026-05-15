namespace DandyStrategies;

public abstract class AsyncStrategyDefinition : IAsyncStrategyDefinition
{
    public AsyncStrategyDefinition(object key)
    {
        Key = key;
    }

    public object Key { get; }
}

public abstract class AsyncStrategyDefinition<TReturn> : IAsyncStrategyDefinition<TReturn>
{
    public AsyncStrategyDefinition(object key)
    {
        Key = key;
    }

    public object Key { get; }
}
