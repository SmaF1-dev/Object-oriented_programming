using DeliveryLib.Builders;
using DeliveryLib.Models;
using DeliveryLib.Pricing;
using DeliveryLib.Services;
using Xunit;

public class PricingTests
{
    [Fact]
    public void DefaultPricingStrategy_CalculatesTaxesAndDelivery()
    {
        // Arrange
        var item = new MenuItem("Burger", 100m);
        var builder = new OrderBuilder();

        // Act
        var order = builder.CreateStandard("A", fast: false)
            .AddItem(item, 2)
            .WithPricingStrategy(new DefaultPricingStrategy(0.1m, 20m))
            .Build();

        // Assert
        Assert.Equal(240m, order.CalculateTotal());
    }

    [Fact]
    public void DiscountDecorator_AppliesRate()
    {
        // Arrange
        var item = new MenuItem("Pizza", 200m);

        // Act
        var order = new OrderBuilder().CreateStandard("A")
            .AddItem(item, 1)
            .WithPricingStrategy(new DiscountDecorator(new DefaultPricingStrategy(0.1m, 20m), discountRate: 0.1m))
            .Build();

        // Assert
        Assert.Equal(216m, order.CalculateTotal());
    }
}
