using DeliveryLib.Models;
using DeliveryLib.Builders;
using DeliveryLib.Pricing;
using DeliveryLib.Services;
using DeliveryLib.Observer;


var pizza = new MenuItem("Маргарита", 300m);
var sushi = new MenuItem("Сет 'Император'", 800m);
var burger = new MenuItem("Бургер", 250m);

var manager = OrderManager.Instance;


var builder1 = new OrderBuilder();
var order1 = builder1
    .CreateStandard(
        address: "ул. Ломоносова, 16",
        fast: true
    )
    .AddItem(pizza, 1)
    .AddItem(sushi, 2)
    .WithPricingStrategy(
        new DiscountDecorator(
            new DefaultPricingStrategy(taxRate: 0.10m, deliveryFee: 50m),
            discountRate: 0.10m
        )
    )
    .Build();

var builder2 = new OrderBuilder();
var order2 = builder2
    .CreateSpecial(
        address: "пр. Науки, 55",
        fast: false,
        preferences: "Без лука, только острое"
    )
    .AddItem(burger, 3)
    .AddItem(pizza, 1)
    .WithPricingStrategy(
        new DefaultPricingStrategy(taxRate: 0.15m, deliveryFee: 70m)
    )
    .Build();

var observer = new ConsoleObserver();
order1.Subscribe(observer);
order2.Subscribe(observer);

manager.CreateOrder(order1);
manager.CreateOrder(order2);

Console.WriteLine("\n=== Стоимость заказов ===");
Console.WriteLine($"Заказ {order1.Id}: {order1.CalculateTotal()} руб.");
Console.WriteLine($"Заказ {order2.Id}: {order2.CalculateTotal()} руб.\n");

Console.WriteLine("=== Изменение состояния заказов ===");
order1.AdvanceState();
order1.AdvanceState();

order2.AdvanceState();


Console.WriteLine("\n=== Все заказы ===");
foreach (var o in manager.GetAllOrders())
{
    Console.WriteLine($"ID: {o.Id}");
    Console.WriteLine($"Тип: {(o is SpecialOrder ? "Специальный" : "Стандартный")}");
    Console.WriteLine($"Адрес: {o.DeliveryAddress}");
    Console.WriteLine($"Быстрая доставка: {o.IsFastDelivery}");
    Console.WriteLine($"Статус: {o.StateName}");
    Console.WriteLine($"Позиции: {string.Join(", ", o.Items.Select(i => $"{i.MenuItem.Name} x{i.Quantity}"))}");
    Console.WriteLine($"Итоговая сумма: {o.CalculateTotal()}");
    Console.WriteLine();
}

public class ConsoleObserver : IOrderObserver
{
    public void OnOrderStateChanged(Order order, string newState)
    {
        Console.WriteLine($"[Observer] Заказ {order.Id}: состояние → {newState}");
    }
}
