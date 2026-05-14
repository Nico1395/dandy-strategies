namespace DandyStrategies;

public interface IStrategyMediator
{
    void Execute<TDefinition>(TDefinition definition) where TDefinition : IStrategyDefinition;
    TReturn Execute<TReturn>(IStrategyDefinition<TReturn> definition);
    Task ExecuteAsync<TDefinition>(TDefinition definition, CancellationToken cancellationToken = default) where TDefinition : IStrategyDefinition<Task>;
    Task<TReturn> ExecuteAsync<TReturn>(IStrategyDefinition<Task<TReturn>> definition, CancellationToken cancellationToken = default);
}
