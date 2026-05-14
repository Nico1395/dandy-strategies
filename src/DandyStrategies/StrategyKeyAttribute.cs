namespace DandyStrategies;

[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
public sealed class StrategyKeyAttribute(object key) : Attribute
{
    public object Key { get; } = key;
}
