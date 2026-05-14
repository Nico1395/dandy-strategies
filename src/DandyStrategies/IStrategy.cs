namespace DandyStrategies;

public interface IStrategy<TDefinition>
    where TDefinition : IStrategyDefinition
{
    void Execute(TDefinition definition);
}

public interface IStrategy<TDefinition, TReturn>
    where TDefinition : IStrategyDefinition<TReturn>
{
    TReturn Execute(TDefinition definition);
}
