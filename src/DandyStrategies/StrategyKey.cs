namespace DandyStrategies;

public sealed record StrategyKey(string Name) : IStrategyKey;
public sealed record StrategyKey<TReturnValue>(string Name) : IStrategyKey<TReturnValue>;
