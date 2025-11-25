namespace DeliveryLib.Factories;

using DeliveryLib.Models;

public static class OrderFactory
{
    public static Order Create(string type, string address, bool fast = false, string preferences = "")
    {
        return type.ToLower() switch
        {
            "standard" => new StandardOrder(address, fast),
            "special" => new SpecialOrder(address, preferences, fast),
            _ => throw new ArgumentException("Unknown order type", nameof(type))
        };
    }
}
