namespace DandyStrategies;

public interface IStrategyMediator
{
    void Execute(IStrategyKey key);
    TReturnValue Execute<TReturnValue>(IStrategyKey<TReturnValue> key);
    Task ExecuteAsync(IAsyncStrategyKey key, CancellationToken cancellationToken);
    Task<TReturnValue> ExecuteAsync<TReturnValue>(IAsyncStrategyKey<TReturnValue> key, CancellationToken cancellationToken);
}
