namespace VendingLib;

public class Product : IItem
{
    public string Name { get; }
    public decimal Price { get; }
    public int Quantity { get; set; }

    public Product(string name, decimal price, int quantity)
    {
        Name = name;
        Price = price;
        Quantity = quantity;
    }

    public override string ToString()
    {
        return $"{Name} - {Price} рублей. Осталось {Quantity} единиц товара.";
    }
}
