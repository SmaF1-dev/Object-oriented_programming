namespace VendingLib;

public abstract class BaseVendingMachine<T> where T : IItem
{
    protected Dictionary<string, T> items = new();
    protected decimal balance = 0;
    protected decimal collectedMoney = 0;

    public abstract void InsertCoin(decimal value);
    public abstract IEnumerable<T> GetItems();
    public abstract bool BuyItem(string name, out T? product);
    public abstract Dictionary<string, int> Cancel();
    public abstract void RefillItem(string name, int quantity);
    public abstract Dictionary<string, int> CollectMoney();
}