namespace DeliveryLib.Observer;

using DeliveryLib.Models;

public interface IOrderObserver
{
    void OnOrderStateChanged(Order order, string newState);
}
