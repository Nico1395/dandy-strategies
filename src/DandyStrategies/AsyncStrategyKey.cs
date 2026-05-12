namespace DandyStrategies;

public sealed record AsyncStrategyKey(string Name) : IAsyncStrategyKey;
public sealed record AsyncStrategyKey<TReturnValue>(string Name) : IAsyncStrategyKey<TReturnValue>;
