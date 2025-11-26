namespace DeliveryLib.Pricing;

using DeliveryLib.Models;

public interface IPricingStrategy
{
    decimal CalculateTotal(Order order);
}
