namespace DeliveryLib.Models;

public class MenuItem
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Name { get; }
    public decimal Price { get; }

    public MenuItem(string name, decimal price)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        if (price < 0)
        {
            throw new ArgumentException("Стоимость должна быть >= 0", nameof(price));
        }
        Price = price;
    }
}
