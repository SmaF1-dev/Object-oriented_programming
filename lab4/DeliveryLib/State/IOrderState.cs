namespace DeliveryLib.State;

using DeliveryLib.Models;

public interface IOrderState
{
    string Name { get; }
    void Advance(Order order);
}
