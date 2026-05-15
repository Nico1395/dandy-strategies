using Microsoft.Extensions.DependencyInjection;

namespace DandyStrategies.Tests;

public class StrategyRegistrarTests
{
#region Sync tests

    private sealed class TestDefinition(object key) : StrategyDefinition(key);
    private sealed class TestDefinitionStrategy : IStrategy<TestDefinition>
    {
        public void Execute(TestDefinition definition) => throw new NotImplementedException();
    }

    private sealed class RetTestDefinition(object key) : StrategyDefinition<int>(key);
    private sealed class RetTestDefinitionStrategy : IStrategy<RetTestDefinition, int>
    {
        public int Execute(RetTestDefinition definition) => throw new NotImplementedException();
    }

    [Fact]
    public void AddStrategy_Sync_ValidType_Passes()
    {
        var registrar = new StrategyRegistrar<TestDefinition>(new ServiceCollection());
        registrar.AddStrategy("valid-type", typeof(TestDefinitionStrategy));
    }

    [Fact]
    public void AddStrategy_Sync_InvalidType_Throws()
    {
        var registrar = new StrategyRegistrar<TestDefinition>(new ServiceCollection());
        Assert.Throws<InvalidOperationException>(() => registrar.AddStrategy("invalid-type", typeof(string)));
    }

    [Fact]
    public void AddStrategyRet_Sync_ValidType_Passes()
    {
        var registrar = new StrategyRegistrar<RetTestDefinition, int>(new ServiceCollection());
        registrar.AddStrategy("valid-type", typeof(RetTestDefinitionStrategy));
    }

    [Fact]
    public void AddStrategyRet_Sync_InvalidType_Throws()
    {
        var registrar = new StrategyRegistrar<RetTestDefinition, int>(new ServiceCollection());
        Assert.Throws<InvalidOperationException>(() => registrar.AddStrategy("invalid-type", typeof(string)));
    }

#endregion

#region Async tests

    private sealed class AsyncTestDefinition(object key) : AsyncStrategyDefinition(key);
    private sealed class AsyncTestDefinitionStrategy : IAsyncStrategy<AsyncTestDefinition>
    {
        public Task ExecuteAsync(AsyncTestDefinition definition, CancellationToken cancellationToken) => throw new NotImplementedException();
    }

    private sealed class AsyncRetTestDefinition(object key) : AsyncStrategyDefinition<int>(key);
    private sealed class AsyncRetTestDefinitionStrategy : IAsyncStrategy<AsyncRetTestDefinition, int>
    {
        public Task<int> ExecuteAsync(AsyncRetTestDefinition definition, CancellationToken cancellationToken) => throw new NotImplementedException();
    }

    [Fact]
    public void AddStrategy_Async_ValidType_Passes()
    {
        var registrar = new AsyncStrategyRegistrar<AsyncTestDefinition>(new ServiceCollection());
        registrar.AddStrategy("valid-type", typeof(AsyncTestDefinitionStrategy));
    }

    [Fact]
    public void AddStrategy_Async_InvalidType_Throws()
    {
        var registrar = new AsyncStrategyRegistrar<AsyncTestDefinition>(new ServiceCollection());
        Assert.Throws<InvalidOperationException>(() => registrar.AddStrategy("invalid-type", typeof(string)));
    }

    [Fact]
    public void AddStrategyRet_Async_ValidType_Passes()
    {
        var registrar = new AsyncStrategyRegistrar<AsyncRetTestDefinition, int>(new ServiceCollection());
        registrar.AddStrategy("valid-type", typeof(AsyncRetTestDefinitionStrategy));
    }

    [Fact]
    public void AddStrategyRet_Async_InvalidType_Throws()
    {
        var registrar = new AsyncStrategyRegistrar<AsyncRetTestDefinition, int>(new ServiceCollection());
        Assert.Throws<InvalidOperationException>(() => registrar.AddStrategy("invalid-type", typeof(string)));
    }

#endregion
}
