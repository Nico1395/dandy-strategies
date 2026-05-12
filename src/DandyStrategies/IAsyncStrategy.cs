namespace DandyStrategies;

public interface IAsyncStrategy<TKey>
    where TKey : IAsyncStrategyKey
{
    Task ExecuteAsync(TKey strategy, CancellationToken cancellationToken);
}

public interface IAsyncStrategy<TKey, TReturnValue>
    where TKey : IAsyncStrategyKey<TReturnValue>
{
    Task<TReturnValue> ExecuteAsync(TKey strategy, CancellationToken cancellationToken);
}
