using VendingLib;

class VendingApp
{
    static void Main()
    {
        var vendingMachine = new VendingMachine();
        vendingMachine.AddProduct(new Product(name: "Bounty", price: 100, quantity: 12));
        vendingMachine.AddProduct(new Product(name: "Sneakers", price: 80, quantity: 10));
        vendingMachine.AddProduct(new Product(name: "CoolCola", price: 70, quantity: 8));

        while (true)
        {
            Console.WriteLine("\n");
            Console.WriteLine("==============================");
            Console.WriteLine("1. Посмотреть товары");
            Console.WriteLine("2. Внести монету");
            Console.WriteLine("3. Купить товар");
            Console.WriteLine("4. Сдача/вернуть деньги");
            Console.WriteLine("5. Админ-режим");
            Console.WriteLine("0. Выход");
            Console.WriteLine("==============================");
            Console.WriteLine("\n");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("\n");
                    foreach (var item in vendingMachine.GetItems())
                        Console.WriteLine(item);
                    break;

                case "2":
                    Console.WriteLine("\n>Внесите монету номинала 1, 2, 5 или 10 рублей.\n");
                    string? coinString = Console.ReadLine();
                    if (decimal.TryParse(coinString, out decimal coin))
                    {
                        vendingMachine.InsertCoin(coin);
                    }
                    else
                    {
                        Console.WriteLine("\n>Не могу распознать, что вы ввели :(\n");
                    }
                    break;

                case "3":
                    Console.WriteLine("\n>Введите наименование товара:\n");
                    string? name = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(name))
                    {
                        Console.WriteLine("\n>Ошибка ввода\n");
                    }
                    else
                    {
                        if (vendingMachine.BuyItem(name!, out var product))
                        {
                            Console.WriteLine($"\n>Вы купили {product!.Name}\n");
                        }
                        else
                        {
                            Console.WriteLine("\n>Такого товара нет :(\n");
                        }
                    }
                    break;
                case "4":
                    var refund = vendingMachine.Cancel();
                    var keysList = new List<string>(refund.Keys);
                    foreach (var key in keysList)
                    {
                        Console.WriteLine($">Выдано {refund[key]} монет номинала {key} руб.");
                    }
                    break;

                case "5":
                    Console.WriteLine("\n-------------------------");
                    Console.WriteLine("Админ-панель:");
                    Console.WriteLine("1 - Пополнить товар.");
                    Console.WriteLine("2 - Собрать деньги.");
                    Console.WriteLine("3 - Добавить новый товар.");
                    Console.WriteLine("0 - Выход.");
                    Console.WriteLine("---------------------------\n");
                    string? adminChoice = Console.ReadLine();
                    switch (adminChoice)
                    {
                        case "1":
                            Console.Write("\n>Название: ");
                            string? nameProduct = Console.ReadLine();
                            Console.Write("\n>Количество: ");
                            string? stringCount = Console.ReadLine();

                            if (string.IsNullOrWhiteSpace(nameProduct))
                            {
                                Console.WriteLine("\n>Ошибка ввода\n");
                            }
                            else
                            {
                                if (int.TryParse(stringCount, out int count))
                                {
                                    vendingMachine.RefillItem(nameProduct, count);
                                }
                            }
                            break;

                        case "2":
                            var money = vendingMachine.CollectMoney();
                            var keysCollectList = money.Keys;
                            foreach (var key in keysCollectList)
                            {
                                Console.WriteLine($">Выдано {money[key]} монет номинала {key} руб.");
                            }
                            break;

                        case "3":
                            Console.WriteLine("\n>Введите наименование товара:");
                            string? newProduct = Console.ReadLine();
                            var oldProducts = vendingMachine.GetItems();

                            if (oldProducts.Any(elem => elem.Name == newProduct))
                            {
                                Console.WriteLine("\n>Извините, такой товар уже есть");
                            }
                            else if (string.IsNullOrWhiteSpace(newProduct))
                            {
                                Console.WriteLine("\n>Ошибка ввода");
                            }
                            else
                            {
                                Console.Write("\n>Введите стоимость товара:");
                                string? priceString = Console.ReadLine();

                                if (string.IsNullOrWhiteSpace(priceString))
                                {
                                    Console.WriteLine("\n>Ошибка ввода\n");
                                }
                                else
                                {
                                    var price = decimal.Parse(priceString);
                                    Console.Write("\n>Введите количество товара:");
                                    string? quantityString = Console.ReadLine();
                                    if (string.IsNullOrWhiteSpace(quantityString))
                                    {
                                        Console.WriteLine("\nОшибка ввода\n");
                                    }
                                    else
                                    {
                                        var quantity = int.Parse(quantityString);
                                        vendingMachine.AddProduct(new Product(newProduct, price, quantity));
                                        Console.WriteLine("\n>Товар успешно добавлен.");
                                    }
                                }
                            }

                            break;

                        case "0":
                            Console.WriteLine("\n>Вы вышли из панели администратора.");
                            break;                    
                    }
                    break;

                case "0":
                    Console.WriteLine("\n>До свидания!");
                    return;
            }
        }
    }
}