namespace DeliveryLib.State;

using DeliveryLib.Models;

public class CompletedState : IOrderState
{
    public string Name => "Completed";

    public void Advance(Order order)
    {
        throw new InvalidOperationException("Заказ уже выполнен.");
    }
}
