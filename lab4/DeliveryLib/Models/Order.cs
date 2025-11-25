namespace DeliveryLib.Models;

using DeliveryLib.State;
using DeliveryLib.Observer;
using DeliveryLib.Pricing;

public abstract class Order : IOrderObservable
{
    public Guid Id { get; } = Guid.NewGuid();
    public List<OrderItem> Items { get; } = new();
    public string DeliveryAddress { get; set; } = "";
    public bool IsFastDelivery { get; set; } = false;
    private IOrderState state = new PreparingState();
    public string StateName => state.Name;

    private readonly List<IOrderObserver> observers = new();

    public IPricingStrategy PricingStrategy { get; set; } = new DefaultPricingStrategy();

    public void AddItem(OrderItem item) => Items.Add(item);

    public void SetState(IOrderState newState)
    {
        state = newState;
        NotifyStateChanged(state.Name);
    }

    public void AdvanceState() => state.Advance(this);

    public decimal CalculateTotal() => PricingStrategy.CalculateTotal(this);

    public void Subscribe(IOrderObserver observer)
    {
        if (observer == null) return;
        if (!observers.Contains(observer)) observers.Add(observer);
    }

    public void Unsubscribe(IOrderObserver observer)
    {
        if (observer == null) return;
        observers.Remove(observer);
    }

    public void NotifyStateChanged(string newState)
    {
        foreach (var o in observers.ToArray())
            o.OnOrderStateChanged(this, newState);
    }
}
