namespace DeliveryLib.Models;

public class StandardOrder : Order
{
    public StandardOrder(string address, bool fast = false)
    {
        DeliveryAddress = address;
        IsFastDelivery = fast;
    }
}
