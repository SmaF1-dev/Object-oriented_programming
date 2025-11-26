namespace DeliveryLib.State;

using DeliveryLib.Models;

public class DeliveringState : IOrderState
{
    public string Name => "Delivering";

    public void Advance(Order order)
    {
        if (order == null)
        {
            throw new ArgumentNullException(nameof(order));
        }
        order.SetState(new CompletedState());
    }
}
