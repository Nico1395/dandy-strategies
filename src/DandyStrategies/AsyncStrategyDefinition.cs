namespace DandyStrategies;

public abstract class AsyncStrategyDefinition : IStrategyDefinition<Task>
{
    public AsyncStrategyDefinition(object key)
    {
        Key = key;
        CancellationToken = CancellationToken.None;
    }

    public AsyncStrategyDefinition(object key, CancellationToken cancellationToken)
    {
        Key = key;
        CancellationToken = cancellationToken;
    }

    public object Key { get; }
    public CancellationToken CancellationToken { get; }
}

public abstract class AsyncStrategyDefinition<TReturn> : IStrategyDefinition<Task<TReturn>>
{
    public AsyncStrategyDefinition(object key)
    {
        Key = key;
        CancellationToken = CancellationToken.None;
    }

    public AsyncStrategyDefinition(object key, CancellationToken cancellationToken)
    {
        Key = key;
        CancellationToken = cancellationToken;
    }

    public object Key { get; }
    public CancellationToken CancellationToken { get; }
}
