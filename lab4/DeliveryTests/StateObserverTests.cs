using DeliveryLib.Builders;
using DeliveryLib.Models;
using DeliveryLib.Services;
using DeliveryLib.Observer;
using Xunit;

public class StateObserverTests
{
    private class TestObserver : IOrderObserver
    {
        public List<string> Events { get; } = new();
        public void OnOrderStateChanged(Order order, string newState) => Events.Add(newState);
    }

    [Fact]
    public void StateTransition_NotifiesObserver()
    {
        // Arrange
        var mgr = OrderManager.Instance;

        var order = new OrderBuilder().CreateStandard("Addr").Build();
        mgr.CreateOrder(order);

        // Act
        var obs = new TestObserver();
        order.Subscribe(obs);

        // Assert
        Assert.Equal("Preparing", order.StateName);
        order.AdvanceState();
        Assert.Equal("Delivering", order.StateName);
        order.AdvanceState();
        Assert.Equal("Completed", order.StateName); 
        Assert.Equal(2, obs.Events.Count);
        Assert.Contains("Delivering", obs.Events);
        Assert.Contains("Completed", obs.Events);
    }
}
