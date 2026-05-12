namespace DandyStrategies;

internal sealed class StrategyMediator : IStrategyMediator
{
    public void Execute(IStrategyKey key)
    {
        throw new NotImplementedException();
    }

    public TReturnValue Execute<TReturnValue>(IStrategyKey<TReturnValue> key)
    {
        throw new NotImplementedException();
    }

    public Task ExecuteAsync(IAsyncStrategyKey key, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<TReturnValue> ExecuteAsync<TReturnValue>(IAsyncStrategyKey<TReturnValue> key, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}