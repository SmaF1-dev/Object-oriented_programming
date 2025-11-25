namespace DeliveryLib.State;

using DeliveryLib.Models;

public class PreparingState : IOrderState
{
    public string Name => "Preparing";

    public void Advance(Order order)
    {
        if (order == null)
        {
            throw new ArgumentNullException(nameof(order));
        }
        order.SetState(new DeliveringState());
    }
}
