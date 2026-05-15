namespace DandyStrategies;

public interface IAsyncStrategy<TDefinition>
    where TDefinition : IAsyncStrategyDefinition
{
    Task ExecuteAsync(TDefinition definition, CancellationToken cancellationToken);
}

public interface IAsyncStrategy<TDefinition, TReturn>
    where TDefinition : IAsyncStrategyDefinition<TReturn>
{
    Task<TReturn> ExecuteAsync(TDefinition definition, CancellationToken cancellationToken);
}
