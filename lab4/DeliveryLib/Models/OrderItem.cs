namespace DeliveryLib.Models;

public class OrderItem
{
    public MenuItem MenuItem { get; }
    public int Quantity { get; }

    public OrderItem(MenuItem menuItem, int quantity)
    {
        MenuItem = menuItem ?? throw new ArgumentNullException(nameof(menuItem));
        if (quantity <= 0)
        {
           throw new ArgumentException("Количество должно быть > 0", nameof(quantity)); 
        }
        Quantity = quantity;
    }

    public decimal GetSubTotal() => MenuItem.Price * Quantity;
}
