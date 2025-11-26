namespace DeliveryLib.Pricing;

using DeliveryLib.Models;

public class DefaultPricingStrategy : IPricingStrategy
{
    private readonly decimal taxRate;
    private readonly decimal deliveryFee;

    public DefaultPricingStrategy(decimal taxRate = 0.10m, decimal deliveryFee = 50m)
    {
        this.taxRate = taxRate;
        this.deliveryFee = deliveryFee;
    }

    public decimal CalculateTotal(Order order)
    {
        if (order == null)
        {
            throw new ArgumentNullException(nameof(order));
        }
        decimal itemsTotal = order.Items.Sum(i => i.GetSubTotal());
        decimal subtotal = itemsTotal;
        decimal delivery = order.IsFastDelivery ? deliveryFee + 30m : deliveryFee;
        decimal tax = subtotal * taxRate;
        decimal total = subtotal + tax + delivery;
        return decimal.Round(total, 2, MidpointRounding.AwayFromZero);
    }
}
