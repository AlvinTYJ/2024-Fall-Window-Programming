using System;

class ATM
{
    static void Main(string[] args)
    {
        decimal balance = 10000m;
        bool exit = false;
        Console.WriteLine("Welcome to NiCKU ATM");

        while (!exit)
        {
            Console.WriteLine("\nWhat do you want to do?");
            Console.WriteLine("(0) Check balance");
            Console.WriteLine("(1) Withdraw money");
            Console.WriteLine("(2) Deposit money");
            Console.WriteLine("(3) Transfer money");
            Console.WriteLine("(8) Exit");
            
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "0":
                    CheckBalance(balance);
                    break;
                case "1":
                    balance = Withdraw(balance);
                    break;
                case "2":
                    balance = Deposit(balance);
                    break;
                case "3":
                    balance = Transfer(balance);
                    break;
                case "8":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("There's no option");
                    break;
            }
        }
    }

    static void CheckBalance(decimal balance)
    {
        Console.WriteLine($"Balance: {balance}");
    }

    static decimal Withdraw(decimal balance)
    {
        Console.Write("Enter amount: ");
        string input = Console.ReadLine();

        if (int.TryParse(input, out int amount) && IsValidAmount(amount))
        {
            if (amount <= balance)
            {
                balance -= amount;
                Console.WriteLine($"Successfully withdraw");
                Console.WriteLine($"Balance: {balance}");
            }
            else
            {
                Console.WriteLine("Exceed the existing amount");
            }
        }
        else
        {
            Console.WriteLine("Exceed the valid amount 0~100000");
        }
        return balance;
    }

    static decimal Deposit(decimal balance)
    {
        Console.Write("Enter the amount to deposit: ");
        string input = Console.ReadLine();

        if (int.TryParse(input, out int amount) && IsValidAmount(amount))
        {
            balance += amount;
            Console.WriteLine($"Balance: {balance}");
        }
        else
        {
            Console.WriteLine("Exceed the valid amount 0~100000");
        }
        return balance;
    }

    static decimal Transfer(decimal balance)
    {
        Console.Write("Enter transfer account: ");
        string accountNumber = Console.ReadLine();

        if (int.TryParse(accountNumber, out _))
        {
            Console.Write("Enter amount: ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int amount) && IsValidAmount(amount))
            {
                decimal transferFee = Math.Truncate(amount * 0.1m);
                decimal totalDeduction = amount + transferFee;

                if (totalDeduction <= balance)
                {
                    balance -= totalDeduction;
                    Console.WriteLine($"Final cost (+10%) = {totalDeduction}");
                    Console.WriteLine($"Successfully withdraw");
                    Console.WriteLine($"Your new balance is: {balance}");
                }
                else
                {
                    Console.WriteLine("Exceed the existing amount");
                }
            }
            else
            {
                Console.WriteLine("Exceed the valid amount 0~100000");
            }
        }
        else
        {
            Console.WriteLine("Account should be an integer");
        }
        return balance;
    }
    static bool IsValidAmount(int amount)
    {
        return amount >= 0 && amount <= 100000;
    }
}