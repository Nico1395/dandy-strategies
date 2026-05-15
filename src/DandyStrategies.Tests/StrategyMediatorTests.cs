using DandyStrategies.Tests.Mocks;
using DandyStrategies.Tests.Setups;

namespace DandyStrategies.Tests;

public class StrategyMediatorTests(StrategyMediatorSetup _setup) : IClassFixture<StrategyMediatorSetup>
{
    [Fact]
    public void IStrategyMediator_Execute_Passes()
    {
        var helper = new StrategyAssertHelper();
        var strategyMediator = _setup.GetStrategyMediator();
        var def = new SyncStrategies.Definition("strat-b", helper);

        strategyMediator.Execute(def);

        Assert.NotNull(helper.ExecutedStrategy);
        Assert.Equal("strat-b", helper.ExecutedStrategy);
    }

    [Fact]
    public void IStrategyMediator_ExecuteWithReturning_Passes()
    {
        var strategyMediator = _setup.GetStrategyMediator();
        var def = new SyncReturningStrategies.Definition("strat-b");

        Assert.Equal("strat-b", strategyMediator.Execute(def));
    }

    [Fact]
    public async Task IStrategyMediator_ExecuteAsync_Passes()
    {
        var helper = new StrategyAssertHelper();
        var strategyMediator = _setup.GetStrategyMediator();
        var def = new AsyncStrategies.Definition("strat-a", helper);

        await strategyMediator.ExecuteAsync(def, cancellationToken: CancellationToken.None);

        Assert.NotNull(helper.ExecutedStrategy);
        Assert.Equal("strat-a", helper.ExecutedStrategy);
    }

    [Fact]
    public async Task IStrategyMediator_ExecuteAsyncWithReturning_Passes()
    {
        var strategyMediator = _setup.GetStrategyMediator();
        var def = new AsyncReturningStrategies.Definition("strat-a");

        var resultingKey = await strategyMediator.ExecuteAsync(def, cancellationToken: CancellationToken.None);

        Assert.Equal("strat-a", resultingKey);
    }
}
