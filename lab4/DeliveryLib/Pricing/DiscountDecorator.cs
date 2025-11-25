namespace DeliveryLib.Pricing;

using DeliveryLib.Models;

public class DiscountDecorator : IPricingStrategy
{
    private readonly IPricingStrategy inner;
    private readonly decimal discountAmount;
    private readonly decimal discountRate;

    public DiscountDecorator(IPricingStrategy inner, decimal discountAmount = 0m, decimal discountRate = 0m)
    {
        this.inner = inner ?? throw new ArgumentNullException(nameof(inner));
        this.discountAmount = discountAmount;
        this.discountRate = discountRate;
    }

    public decimal CalculateTotal(Order order)
    {
        var baseTotal = inner.CalculateTotal(order);
        decimal byRate = baseTotal * discountRate;
        decimal total = baseTotal - discountAmount - byRate;
        if (total < 0)
        {
            total = 0;
        }
        return decimal.Round(total, 2, MidpointRounding.AwayFromZero);
    }
}
