using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

class StoreManagement
{
    class Product
    {
        public string Name;
        public int Price;
        public int Stock;
        public int SoldQuantity;

        public Product(string name, int price, int stock)
        {
            Name = name;
            Price = price;
            Stock = stock;
            SoldQuantity = 0;
        }
    }

    class Consumer
    {
        public string Name;
        public List<int> Orders = new List<int>();
    }

    static List<Product> products = new List<Product>();
    static int totalIncome = 0;
    static bool isOpen = false;
    static Dictionary<string, Consumer> consumers = new Dictionary<string, Consumer>();

    static void Main(string[] args)
    {
        while (true)
        {
            ShowMenu();
            string input = Console.ReadLine();

            if (!int.TryParse(input, out int choice) || choice < 1 || choice > 6)
            {
                Console.WriteLine("無效的選擇，請重新輸入\n");
                continue;
            }

            if (!isOpen && choice != 1 && choice != 6)
            {
                Console.WriteLine("請先開店後再進行其他操作\n");
                continue;
            }

            if (isOpen && choice == 1)
            {
                Console.WriteLine("您已經開店，請進行其他操作\n");
                continue;
            }

            switch (choice)
            {
                case 1:
                    OpenStore();
                    break;
                case 2:
                    AddOrder();
                    break;
                case 3:
                    CheckStock();
                    break;
                case 4:
                    CheckTotalIncome();
                    break;
                case 5:
                    CalculatePopularProducts();
                    break;
                case 6:
                    CloseStore();
                    return;
            }
        }
    }

    static void ShowMenu()
    {
        Console.WriteLine("歡迎來到NCKU卡比商店交易系統");
        Console.WriteLine("======================================");
        Console.WriteLine("(1) 開店");
        Console.WriteLine("(2) 新增訂單");
        Console.WriteLine("(3) 查詢庫存");
        Console.WriteLine("(4) 查詢總收入");
        Console.WriteLine("(5) 計算人氣商品");
        Console.WriteLine("(6) 關店");
        Console.WriteLine("======================================");
        Console.Write("請輸入您現在想要進行的操作: ");
    }

    static void OpenStore()
    {
        while (true)
        {
            Console.Write("請輸入今日總共有幾種商品要販售: ");
            if (!int.TryParse(Console.ReadLine(), out int productCount) || productCount <= 0)
            {
                Console.WriteLine("商品數量無效，請重新輸入\n");
                continue;
            }

            Console.Write("請依序輸入每一種商品的名稱: ");
            string[] productNames = Console.ReadLine().Split(' ');
            if (productNames.Length != productCount)
            {
                Console.WriteLine("輸入的商品名稱數量不符合商品總數，請重新輸入\n");
                continue;
            }
            Console.WriteLine($"\n輸入完成！您總共有{productCount}個商品，每一個的商品名稱依序是：{string.Join(" ", productNames)}");

            Console.Write("接下來，請依序輸入每一個商品的價格：");
            string[] productPrices = Console.ReadLine().Split(' ');
            if (productPrices.Length != productCount)
            {
                Console.WriteLine("輸入的商品價格不符合商品總數，請重新輸入\n");
                continue;
            }

            bool invalidInputA = false;
            for (int i = 0; i < productCount; i++)
            {
                if (!int.TryParse(productPrices[i], out int price) || price <= 0)
                {
                    Console.WriteLine("價格輸入無效，請重新輸入\n");
                    invalidInputA = true;
                    break;
                }
            }
            if (invalidInputA)
            {
                continue;
            }
            Console.WriteLine("\n輸入完成！每一種商品的價格依序為: ");
            for (int i = 0; i < productCount; i++)
            {
                Console.WriteLine($"{productNames[i]}: {productPrices[i]}");
            }

            Console.Write("\n最後，請你依序輸入每一個商品目前的庫存數量: ");
            string[] productStocks = Console.ReadLine().Split(' ');
            if (productStocks.Length != productCount)
            {
                Console.WriteLine("輸入的商品庫存不符合商品總數，請重新輸入\n");
                continue;
            }
            bool invalidInputB = false;
            for (int i = 0; i < productCount; i++)
            {
                if (!int.TryParse(productStocks[i], out int stock) || stock < 0)
                {
                    Console.WriteLine("庫存輸入無效，請重新輸入\n");
                    invalidInputB = true;
                    break;
                }
                products.Add(new Product(productNames[i], int.Parse(productPrices[i]), stock));
            }
            if (invalidInputB)
            {
                continue;
            }
            Console.WriteLine("\n輸入完成！每一種商品的庫存數量依序為: ");
            for (int i = 0; i < productCount; i++)
            {
                Console.WriteLine($"{productNames[i]}: {productStocks[i]}");
            }

            isOpen = true;
            Console.WriteLine("開店程序完成，已開店\n");
            break;
        }
    }

    static void AddOrder()
    {
        Console.Write("請依序輸入此訂單每一種類的商品各需要買幾個: ");
        bool canExit = false;
        while (true)
        {
            string[] orderQuantities = Console.ReadLine().Split(' ');

            if (canExit && orderQuantities.Length == 1 && orderQuantities[0] == "-1")
            {
                Console.WriteLine("訂單已取消\n");
                return;
            }

            if (orderQuantities.Length != products.Count)
            {
                Console.Write("輸入的購買數量與商品總數不符，請重新輸入或輸入-1取消訂單: ");
                canExit = true;
                continue;
            }

            int totalAmount = 0;
            bool invalidInput = false;
            bool allZero = true;
            for (int i = 0; i < products.Count; i++)
            {
                if (!int.TryParse(orderQuantities[i], out int quantity) || quantity < 0 || quantity > products[i].Stock)
                {
                    invalidInput = true;
                    break;
                }
                totalAmount += quantity * products[i].Price;

                if (quantity != 0)
                {
                    allZero = false;
                }
            }

            if (invalidInput)
            {
                Console.Write("購買數量無效或超過庫存，請重新輸入或輸入-1取消訂單: ");
                canExit = true;
                continue;
            }

            if (allZero)
            {
                Console.Write("請至少選擇一種商品，購買數量不能全部為零，請重新輸入請重新輸入或輸入-1取消訂單: ");
                canExit = true;
                continue;
            }

            Console.Write($"\n訂單成立！總金額為: {totalAmount} ");

            if (totalAmount >= 1000)
            {
                Random random = new Random();
                int discount = random.Next(1, 10);
                totalAmount = totalAmount * discount / 10;
                Console.Write($"因訂單滿1000元，因此總金額打{discount}折，打折後為{totalAmount}元");
            }

            Console.Write($"請輸入消費者付款金額: ");
            string cash = Console.ReadLine();
            while (true)
            {
                if (!int.TryParse(cash, out int payment) || payment < totalAmount)
                {
                    Console.Write("付款金額無效或不足，請重新輸入或輸入-1取消訂單: ");
                    cash = Console.ReadLine();
                    if (cash == "-1")
                    {
                        Console.WriteLine("訂單已取消\n");
                        return;
                    }
                    continue;
                }
                Console.WriteLine($"\n付款完成！請找零消費者共 {payment - totalAmount} 元");
                break;
            }

            Console.Write("請輸入消費者的名字: ");
            while (true)
            {
                string customerName = Console.ReadLine();
                if (!Regex.IsMatch(customerName, @"^[a-zA-Z\s]+$"))
                {
                    Console.Write("名字無效，請輸入只包含英文字母和空格的名字: ");
                    continue;
                }
                if (!consumers.ContainsKey(customerName))
                {
                    consumers[customerName] = new Consumer { Name = customerName };
                }
                consumers[customerName].Orders.Add(totalAmount);

                var consumer = consumers[customerName];
                Console.WriteLine($"消費者 {customerName} 歷史資料:\n");
                Console.WriteLine($"此消費者歷史訂單中，最大金額為: {consumer.Orders.Max()}");
                for (int i = consumer.Orders.Count - 1, j = 1; j <= 3; i--, j++)
                {
                    int transactionAmount = (i >= 0) ? consumer.Orders[i] : 0;
                    Console.WriteLine($"近期交易{j}: {transactionAmount}");
                }
                Console.WriteLine();
                break;
            }

            for (int i = 0; i < products.Count; i++)
            {
                int quantity = int.Parse(orderQuantities[i]);
                products[i].Stock -= quantity;
                products[i].SoldQuantity += quantity;
            }
            totalIncome += totalAmount;
            break;
        }
    }

    static void CheckStock()
    {
        bool insufficient = false;
        foreach (var product in products)
        {
            Console.WriteLine($"{product.Name}: {product.Stock}");
            if (product.Stock <= 5)
            {
                insufficient = true;
            }
        }
        if (insufficient)
        {
            Console.WriteLine("有商品的庫存數量不足!!!");
        }
        Console.WriteLine();
    }

    static void CheckTotalIncome()
    {
        Console.WriteLine($"總收入為：{totalIncome}\n");
    }

    static void CalculatePopularProducts()
    {
        var popularProducts = products.OrderByDescending(p => p.SoldQuantity).ThenBy(p => products.IndexOf(p)).ToList();

        Console.WriteLine("人氣商品排行榜：");
        for (int i = 0; i < popularProducts.Count; i++)
        {
            Console.WriteLine($"第{i + 1}名: {popularProducts[i].Name}, 總共買數量共{popularProducts[i].SoldQuantity}次");
        }
        Console.WriteLine();
    }

    static void CloseStore()
    {
        Console.WriteLine("謝謝惠顧。");
        Environment.Exit(0);
    }
}