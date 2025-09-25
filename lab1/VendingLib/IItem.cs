namespace VendingLib;
public interface IItem
{
    string Name { get; }
    decimal Price { get; }
    int Quantity { get; set; }

}