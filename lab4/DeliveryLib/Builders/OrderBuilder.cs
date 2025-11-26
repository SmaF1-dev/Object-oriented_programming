namespace DeliveryLib.Builders;

using DeliveryLib.Models;
using DeliveryLib.Pricing;

public class OrderBuilder
{
    private Order? order;

    public OrderBuilder CreateStandard(string address, bool fast = false)
    {
        order = new StandardOrder(address, fast);
        return this;
    }

    public OrderBuilder CreateSpecial(string address, string preferences, bool fast = false)
    {
        order = new SpecialOrder(address, preferences, fast);
        return this;
    }

    public OrderBuilder AddItem(MenuItem menuItem, int quantity = 1)
    {
        if (order == null)
        {
            throw new InvalidOperationException("Сначала нужно создать заказ.");
        }
        order.AddItem(new OrderItem(menuItem, quantity));
        return this;
    }

    public OrderBuilder WithPricingStrategy(IPricingStrategy strategy)
    {
        if (order == null)
        {
            throw new InvalidOperationException("Сначала нужно создать заказ.");
        }
        order.PricingStrategy = strategy;
        return this;
    }

    public Order Build()
    {
        if (order == null)
        {
            throw new InvalidOperationException("Сначала нужно создать заказ.");
        }
        var built = order;
        order = null;
        return built;
    }
}
