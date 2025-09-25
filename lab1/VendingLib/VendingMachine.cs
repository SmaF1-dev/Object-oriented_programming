
namespace VendingLib;

public class VendingMachine : BaseVendingMachine<Product>
{
    private static readonly decimal[] allowedCoins = [1, 2, 5, 10];

    public override void InsertCoin(decimal value)
    {
        if (allowedCoins.Any(val => val == value))
        {
            balance += value;
            Console.WriteLine($"\n>Внесено {value} руб., ваш баланс: {balance} руб.\n");
        }
        else
        {
            Console.WriteLine($"\n>Ошибка, монета недопустимого номинала.\n");
        }
    }

    public void AddProduct(Product product)
    {
        items[product.Name] = product;
    }

    public override IEnumerable<Product> GetItems() => items.Values;

    public override bool BuyItem(string name, out Product? product)
    {
        if (items.ContainsKey(name))
        {
            var item = items[name];
            if (item.Quantity > 0 && balance >= item.Price)
            {
                balance -= item.Price;
                collectedMoney += item.Price;
                item.Quantity--;
                product = item;
                return true;
            }
        }
        product = null;
        return false;
    }

    private Dictionary<string, int> GetChange(decimal amount) {

        Dictionary<string, int> result = new();

        var sortedCoins = allowedCoins.OrderByDescending(elem => elem);

        foreach (var coin in sortedCoins)
        {
            string key = coin.ToString("N0");

            while (amount >= coin)
            {
                amount -= coin;
                if (!result.ContainsKey(key))
                {
                    result[key] = 0;   
                }
                result[key] += 1;
            }
        }
        return result;
    }

    public override Dictionary<string, int> Cancel()
    {
        var refund = balance;
        var coins = GetChange(refund);
        balance = 0;

        return coins;
    }

    public override Dictionary<string, int> CollectMoney()
    {
        decimal money = collectedMoney;
        var coins = GetChange(money);
        collectedMoney = 0;
        return coins;
    }

    public override void RefillItem(string name, int quantity)
    {
        if (items.ContainsKey(name))
        {
            items[name].Quantity += quantity;
            Console.WriteLine($"\n>Товар {name} успешно пополнен. Теперь его {items[name].Quantity} единиц\n");
        }
        else
        {
            Console.WriteLine($"\n>Товара {name} нет :(\n");
        }
    }

}