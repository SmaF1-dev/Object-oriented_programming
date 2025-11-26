namespace DeliveryLib.Models;

public class SpecialOrder : Order
{
    public string Preferences { get; set; } = "";

    public SpecialOrder(string address, string preferences, bool fast = false)
    {
        DeliveryAddress = address;
        Preferences = preferences;
        IsFastDelivery = fast;
    }
}
