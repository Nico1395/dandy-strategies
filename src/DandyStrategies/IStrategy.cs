namespace DandyStrategies;

public interface IStrategy<TKey>
    where TKey : IStrategyKey
{
    void Execute(TKey strategy);
}

public interface IStrategy<TKey, TReturnValue>
    where TKey : IStrategyKey<TReturnValue>
{
    TReturnValue Execute(TKey strategy);
}
