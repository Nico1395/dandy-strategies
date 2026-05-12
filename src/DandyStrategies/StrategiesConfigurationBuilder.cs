using System.Reflection;

namespace DandyStrategies;

public sealed class StrategiesConfigurationBuilder
{
    private readonly StrategiesConfiguration _configuration = new();

    public StrategiesConfigurationBuilder ScanInAssemblies(params IEnumerable<Assembly> assemblies)
    {
        _configuration.Assemblies = assemblies.ToList();
        return this;
    }

    internal StrategiesConfiguration Build() => _configuration;
}
