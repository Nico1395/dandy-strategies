# Whats DandyStrategies?
A small framework for streamlining the use of the strategy design pattern in C# .NET Core using yet another interpretation of the mediator design pattern.

# Setup and use
## Implementing strategies
Before implementing strategies you need to write a strategy definiton, by implementing one of these interfaces, depending on your need:
- `IStrategyDefinition` - synchronous returning nothing
- `IStrategyDefinition<TReturn>` - synchronous returning `TReturn`
- `IAsyncStrategyDefinition` - asynchronous returning nothing
- `IAsyncStrategyDefinition<TReturn>` - asynchronous returning `TReturn`

An implemented series of strategies for one definition could look like this:
```cs
internal static class MyStrategy
{
    public sealed record Definition(object Key) : IStrategyDefinition;

    [StrategyKey("a")]
    public sealed class StrategyA : IStrategy<Definition>
    {
        public void Execute(Definition definition)
        {
        }
    }
    
    [StrategyKey("b")]
    public sealed class StrategyB : IStrategy<Definition>
    {
        public void Execute(Definition definition)
        {
        }
    }
}
```

But of course you can structure your classes differently as well.

## Executing strategies
To execute the strategies you have to inject the `IStrategyExecutor` and instantiate a strategy definition. The executor allows you to throw in the strategy definition and figures out what strategy to run.

```cs
internal sealed class MyService(IStrategyExecutor executor)
{
    public void MyCoolMethod(string donaldTrumpIsInTheEpsteinFiles)
    {
        var definition = new MyStrategy.Definition(donaldTrumpIsInTheEpsteinFiles);
        executor.Execute(definition);
    }
}
```

## Setup
As expected there is an extension method for the `IServiceCollection` interface that registers required services and allows to configure automatic registration of strategies via assembly scanning (more on that down below).

The extension exposes an action for configuring the framework via the `StrategiesConfigurationBuilder`, which allows you to setup assembly scanning, but also allows registering strategies.

```cs
builder.ervices.AddDandyStrategies(configuration =>
{
    // Registering strategies or assembly scanning and alike...
);
```

## Registering strategies
You can register strategies in three ways:
1. Registering strategies using the `StrategiesConfigurationBuilder` during `AddDandyStrategies` at startup.
   ```cs
   services.AddDandyStrategies(configuration =>
    {
        configuration.AddStategyDefinition<MyStrategyDefinition>(def =>
        {
            def.AddStrategy<MyStrategyA>("strat-a");
            def.AddStrategy<MyStrategyB>("strat-b");
        });
    });
   ```
2. Registering strategies using extensions for the `IServiceCollection` interface.
   ```cs
   services.AddStategyDefinition<MyStrategyDefinition>(def =>
   {
       def.AddStrategy<MyStrategyA>("strat-a");
       def.AddStrategy<MyStrategyB>("strat-b");
   });
   ```
3. Registering strategies using assembly scanning and the `StrategyKeyAttribute` (down below).

No matter how you decide to register your strategies, they are always registered keyed for their respective interface type from above. So theoretically you could also manually register everything using just `IServiceCollection.AddKeyedTransient()` (even after already running through previous registrations for a given strategy definition).

## Assembly scanning and `StrategyKeyAttribute`
In order to scan for strategies and register them automatically they need to be adorned with the `StrategyKeyAttribute` and the respective key the strategy is supposed to be associated with.

Additionally you need to specify what assemblies to scan in and you should be set.

```cs
services.AddDandyStrategies(cfg =>
{
    cfg.ScanInAssemblies(GetType().Assembly);
});
```
