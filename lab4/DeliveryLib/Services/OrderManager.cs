namespace DeliveryLib.Services;

using DeliveryLib.Models;

public class OrderManager
{
    private static readonly Lazy<OrderManager> instance = new(() => new OrderManager());
    public static OrderManager Instance => instance.Value;

    private readonly List<Order> orders = new();

    private OrderManager() { }

    public Order CreateOrder(Order order)
    {
        if (order == null)
        {
            throw new ArgumentNullException(nameof(order));
        }
        orders.Add(order);
        return order;
    }

    public IEnumerable<Order> GetAllOrders() => orders.ToList();

    public Order? GetOrderById(Guid id) => orders.FirstOrDefault(o => o.Id == id);

    public void RemoveOrder(Guid id)
    {
        var o = GetOrderById(id);
        if (o != null)
        {
            orders.Remove(o);
        }
    }

    public void AdvanceOrderState(Guid id)
    {
        var o = GetOrderById(id);
        if (o == null)
        {
            throw new InvalidOperationException("Заказ не найден.");
        }
        o.AdvanceState();
    }

    internal void Clear() => orders.Clear();
}
