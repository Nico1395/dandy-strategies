using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace DandyStrategies;

public sealed class StrategiesConfigurationBuilder(IServiceCollection _services)
{
    private readonly StrategiesConfiguration _configuration = new();

    public StrategiesConfigurationBuilder ScanInAssemblies(params IEnumerable<Assembly> assemblies)
    {
        _configuration.Assemblies = [.. assemblies];
        return this;
    }

    public StrategiesConfigurationBuilder AddStrategyDefinition<TDefinition>(Action<IStrategyRegistrar<TDefinition>> definition)
        where TDefinition : IStrategyDefinition
    {
        definition(new StrategyRegistrar<TDefinition>(_services));
        return this;
    }

    public StrategiesConfigurationBuilder AddStrategyDefinition<TDefinition, TReturn>(Action<IStrategyRegistrar<TDefinition, TReturn>> definition)
        where TDefinition : IStrategyDefinition<TReturn>
    {
        definition(new StrategyRegistrar<TDefinition, TReturn>(_services));
        return this;
    }

    public StrategiesConfigurationBuilder AddStrategyDefinition<TDefinition>(Action<IAsyncStrategyRegistrar<TDefinition>> definition)
        where TDefinition : IAsyncStrategyDefinition
    {
        definition(new AsyncStrategyRegistrar<TDefinition>(_services));
        return this;
    }

    public StrategiesConfigurationBuilder AddStrategyDefinition<TDefinition, TReturn>(Action<IAsyncStrategyRegistrar<TDefinition, TReturn>> definition)
        where TDefinition : IAsyncStrategyDefinition<TReturn>
    {
        definition(new AsyncStrategyRegistrar<TDefinition, TReturn>(_services));
        return this;
    }

    internal StrategiesConfiguration Build() => _configuration;
}
