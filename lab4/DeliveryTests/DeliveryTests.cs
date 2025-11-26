using Xunit;
using DeliveryLib.Models;
using DeliveryLib.Builders;
using DeliveryLib.Services;
using DeliveryLib.Pricing;
using DeliveryLib.State;
using DeliveryLib.Observer;
using DeliveryLib.Factories;

public class DeliveryTests
{
    [Fact]
    public void Builder_ShouldCreateStandardOrder()
    {
        // Arrange
        var item = new MenuItem("Пицца", 500m);

        // Act
        var order = new OrderBuilder()
            .CreateStandard("Адрес 1", fast: false)
            .AddItem(item, 2)
            .Build();

        // Assert
        Assert.Equal("Адрес 1", order.DeliveryAddress);
        Assert.Single(order.Items);
        Assert.Equal(1000m, order.Items.Sum(it => it.GetSubTotal()));
    }

    [Fact]
    public void Builder_ShouldCreateSpecialOrder()
    {
        // Arrange
        var item = new MenuItem("Суши", 300m);

        // Act
        var order = new OrderBuilder()
            .CreateSpecial("Адрес 2", "Без соевого соуса", fast: true)
            .AddItem(item, 3)
            .Build();

        // Assert
        Assert.Equal("Без соевого соуса", ((SpecialOrder)order).Preferences);
        Assert.Equal(900m, order.Items.Sum(i => i.GetSubTotal()));
        Assert.True(order.IsFastDelivery);
    }

    [Fact]
    public void Factory_ShouldCreateStandardOrder()
    {
        // Arrange & Act
        var order = OrderFactory.Create("standard", "Test Address", fast: false);

        // Assert
        Assert.IsType<StandardOrder>(order);
        Assert.False(order.IsFastDelivery);
    }

    [Fact]
    public void Factory_ShouldCreateSpecialOrder()
    {
        // Arrange & Act
        var order = OrderFactory.Create("special", "Test Address", preferences: "No onions");

        // Assert
        Assert.IsType<SpecialOrder>(order);
        Assert.Equal("No onions", ((SpecialOrder)order).Preferences);
    }

    [Fact]
    public void Pricing_DefaultStrategy_ShouldCalculateCorrectly()
    {
        // Arrange
        var order = new StandardOrder("Address", fast: false);
        order.AddItem(new OrderItem(new MenuItem("Burger", 200m), 2));
        order.PricingStrategy = new DefaultPricingStrategy(taxRate: 0.10m, deliveryFee: 50m);

        // Act
        decimal total = order.CalculateTotal();

        // Assert
        Assert.Equal(490m, total);
    }

    [Fact]
    public void Pricing_WithDiscountDecorator_ShouldApplyDiscount()
    {
        // Arrange
        var order = new StandardOrder("Address", fast: false);
        order.AddItem(new OrderItem(new MenuItem("Pasta", 300m), 1));
        var pricing = new DiscountDecorator(
            new DefaultPricingStrategy(0.1m, 20m),
            discountRate: 0.1m
        );
        order.PricingStrategy = pricing;

        // Act
        decimal total = order.CalculateTotal();

        // Assert
        Assert.Equal(315m, total);
    }

    [Fact]
    public void OrderState_ShouldAdvanceCorrectly()
    {
        // Arrange
        var order = new StandardOrder("Address", false);

        // Act
        var s1 = order.StateName;
        order.AdvanceState();
        var s2 = order.StateName;
        order.AdvanceState();
        var s3 = order.StateName;

        // Assert
        Assert.Equal("Preparing", s1);
        Assert.Equal("Delivering", s2);
        Assert.Equal("Completed", s3);
    }

    [Fact]
    public void CompletedState_ShouldNotAdvance()
    {
        // Arrange
        var order = new StandardOrder("Address", false);

        order.AdvanceState();
        order.AdvanceState();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() => order.AdvanceState());
    }

    private class TestObserver : IOrderObserver
    {
        public string? LastState;
        public void OnOrderStateChanged(Order order, string newState)
        {
            LastState = newState;
        }
    }

    [Fact]
    public void Observer_ShouldReceiveUpdates()
    {
        // Arrange
        var observer = new TestObserver();
        var order = new StandardOrder("Address", false);
        order.Subscribe(observer);

        // Act
        order.AdvanceState();

        // Assert
        Assert.Equal("Delivering", observer.LastState);
    }

    [Fact]
    public void OrderManager_ShouldBeSingleton()
    {
        // Arrange
        var m1 = OrderManager.Instance;
        var m2 = OrderManager.Instance;

        // Act & Assert
        Assert.Same(m1, m2);
    }
}
