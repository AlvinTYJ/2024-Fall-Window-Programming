using System;
using System.Collections.Generic;
using System.Linq;

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

    static List<Product> products = new List<Product>();
    static int totalIncome = 0;

    static void Main(string[] args)
    {
        while (true)
        {
            ShowMenu();
            int choice = int.Parse(Console.ReadLine());

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
                default:
                    Console.WriteLine("無效的選擇，請重新輸入");
                    break;
            }
        }
    }

    static void ShowMenu()
    {
        Console.WriteLine("歡迎來到NCKU卡比商店交易系統");
        Console.WriteLine("==============================");
        Console.WriteLine("(1) 開店");
        Console.WriteLine("(2) 新增訂單");
        Console.WriteLine("(3) 查詢庫存");
        Console.WriteLine("(4) 查詢總收入");
        Console.WriteLine("(5) 計算人氣商品");
        Console.WriteLine("(6) 關店");
        Console.WriteLine("==============================");
        Console.Write("請輸入您現在想要進行的操作：");
    }

    static void OpenStore()
    {
        Console.Write("請輸入今日總共有幾種商品要販售：");
        int productCount = int.Parse(Console.ReadLine());

        Console.Write("請依序輸入每一種商品的名稱：");
        string[] productNames = Console.ReadLine().Split(' ');

        Console.WriteLine($"\n輸入完成！您總共有{productCount}個商品，每一個的商品名稱依序是：{string.Join(" ", productNames)}");

        Console.Write("接下來，請你依序輸入每一個商品的價格：");
        string[] productPrices = Console.ReadLine().Split(' ');

        Console.WriteLine("\n輸入完成！每一種商品的價格依序為：");
        for (int i = 0; i < productCount; i++)
        {
            Console.WriteLine($"{productNames[i]}: {productPrices[i]}");
        }

        Console.Write("\n最後，請你依序輸入每一個商品目前的庫存數量：");
        string[] productStocks = Console.ReadLine().Split(' ');

        Console.WriteLine("\n輸入完成！每一種商品的庫存數量依序為：");
        for (int i = 0; i < productCount; i++)
        {
            Console.WriteLine($"{productNames[i]}: {productStocks[i]}");
            products.Add(new Product(productNames[i], int.Parse(productPrices[i]), int.Parse(productStocks[i])));
        }

        Console.WriteLine("\n開店程序完成，已開店\n");
    }

    static void AddOrder()
    {
        if (products.Count == 0)
        {
            Console.WriteLine("請先開店後再新增訂單\n");
            return;
        }

        Console.Write("請依序輸入此訂單每一種類的商品各需要買幾個：");
        string[] orderQuantities = Console.ReadLine().Split(' ');

        int totalAmount = 0;
        for (int i = 0; i < products.Count; i++)
        {
            int quantity = int.Parse(orderQuantities[i]);
            if (quantity > products[i].Stock)
            {
                Console.WriteLine("\n庫存不足，此筆訂單不成立\n");
                return;
            }
            totalAmount += quantity * products[i].Price;
        }

        Console.Write($"\n訂單成立！總金額為：{totalAmount}，請輸入消費者付款金額：");
        int payment = int.Parse(Console.ReadLine());

        while (payment < totalAmount)
        {
            Console.Write($"\n付款金額不足，請再輸入一次（或輸入 -1 直接取消此筆訂單）：");
            payment = int.Parse(Console.ReadLine());
            if (payment == -1)
            {
                Console.WriteLine();
                return;
            }
        }

        Console.WriteLine($"\n付款完成！請找零消費者共 {payment - totalAmount} 元");

        for (int i = 0; i < products.Count; i++)
        {
            int quantity = int.Parse(orderQuantities[i]);
            products[i].Stock -= quantity;
            products[i].SoldQuantity += quantity;
        }
        totalIncome += totalAmount;
    }

    static void CheckStock()
    {
        int a = 0;
        foreach (var product in products)
        {
            Console.WriteLine($"{product.Name}: {product.Stock}");
            if (product.Stock <= 5)
            {
                a = 1;
            }
        }
        if (a == 1)
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
            Console.WriteLine($"第{i + 1}名:{popularProducts[i].Name}, 總共買數量共{popularProducts[i].SoldQuantity}次");
        }
        Console.WriteLine();
    }

    static void CloseStore()
    {
        Console.WriteLine("關店程序完成，程式結束。");
    }
}