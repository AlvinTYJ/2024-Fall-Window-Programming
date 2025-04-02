using System;
using System.Collections.Generic;
using System.Linq;

class Account
{
    public string AccountNumber { get; set; }
    public decimal Balance { get; set; }
    public int HeartPoints { get; set; }
    public List<string> TransactionHistory { get; set; }

    public Account(string accountNumber, decimal balance = 0m)
    {
        AccountNumber = accountNumber;
        Balance = balance;
        HeartPoints = 0;
        TransactionHistory = new List<string>();
    }

    public void AddTransaction(string operationCode)
    {
        TransactionHistory.Add(operationCode + " - " + Balance);
    }
}

class ATM
{
    static Dictionary<string, Account> accounts = new Dictionary<string, Account>();
    static Account currentUser;

    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to NiCKU ATM");
        InitializePredefinedAccounts();
        CreateNewAccount();
        bool exit = false;

        while (!exit)
        {
            ShowMenu();
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "0":
                    CheckBalance();
                    break;
                case "1":
                    Withdraw();
                    break;
                case "2":
                    Deposit();
                    break;
                case "3":
                    Transfer();
                    break;
                case "4":
                    Donate();
                    break;
                case "5":
                    ShowTransactionHistory();
                    break;
                case "65304":
                    HiddenFunction();
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

    static void InitializePredefinedAccounts()
    {
        for (int i = 10000; i < 20000; i+=1000)
        {
            accounts.Add(i.ToString(), new Account(i.ToString(), 10000m));
        }
    }

    static void CreateNewAccount()
    {
        while (true)
        {
            Console.Write("Enter your account\n");
            string newAccountNumber = Console.ReadLine();

            if (!int.TryParse(newAccountNumber, out _))
            {
                Console.WriteLine("Please enter a number");
                continue;
            }

            if (!IsFiveDigits(newAccountNumber))
            {
                Console.WriteLine("Account should be five digits");
                continue;
            }

            if (accounts.ContainsKey(newAccountNumber))
            {
                Console.WriteLine("Already have this account, please try another one");
                continue;
            }

            currentUser = new Account(newAccountNumber, 10000m);
            accounts.Add(newAccountNumber, currentUser);
            break;
        }
    }

    static void ShowMenu()
    {
        Console.WriteLine("\nWhat do you want to do?");
        Console.WriteLine("       (0) Check balance");
        Console.WriteLine("       (1) Withdraw money");
        Console.WriteLine("       (2) Deposit money");
        Console.WriteLine("       (3) Transfer money");
        Console.WriteLine("       (4) Donate");
        Console.WriteLine("       (5) History");
        Console.WriteLine("       (8) Exit");
    }

    static void CheckBalance()
    {
        Console.WriteLine($"Balance: {currentUser.Balance}");
    }

    static void Withdraw()
    {
        Console.Write("Enter amount : ");
        string input = Console.ReadLine();

        if (decimal.TryParse(input, out decimal amount) && IsValidAmount(amount))
        {
            if (amount <= currentUser.Balance)
            {
                currentUser.Balance -= amount;
                Console.WriteLine("Successfully withdraw");
                Console.WriteLine($"Balance : {currentUser.Balance}");
                currentUser.AddTransaction("1");
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

    static void Deposit()
    {
        Console.Write("Enter amount : ");
        string input = Console.ReadLine();

        if (decimal.TryParse(input, out decimal amount) && IsValidAmount(amount))
        {
            currentUser.Balance += amount;
            Console.WriteLine($"Balance : {currentUser.Balance}");
            currentUser.AddTransaction("2");
        }
        else
        {
            Console.WriteLine("Exceed the valid amount 0~100000");
        }
    }

    static void Transfer()
    {
        Console.Write("Enter transfer account : ");
        string targetAccountNumber = Console.ReadLine();

        if (!int.TryParse(targetAccountNumber, out _))
        {
            Console.WriteLine("Please enter a number");
            return;
        }

        if (!IsFiveDigits(targetAccountNumber))
        {
            Console.WriteLine("Account should be five digits");
            return;
        }

        if (targetAccountNumber == currentUser.AccountNumber)
        {
            Console.WriteLine("You can't transfer to yourself");
            return;
        }

        if (!accounts.ContainsKey(targetAccountNumber))
        {
            Console.WriteLine("This is not a exist account, press 1 if you want to open one and keep going");
            string response = Console.ReadLine();
            if (response == "1")
            {
                accounts.Add(targetAccountNumber, new Account(targetAccountNumber, 0m));
            }
            else
            {
                return;
            }
        }

        Console.Write("Enter amount : ");
        string input = Console.ReadLine();

        if (decimal.TryParse(input, out decimal amount) && IsValidAmount(amount))
        {
            decimal transferFee = Math.Truncate(amount * 0.1m);
            decimal totalDeduction = amount + transferFee;

            if (totalDeduction > currentUser.Balance)
            {
                Console.WriteLine("Exceeds the existing amount");
                return;
            }

            if (currentUser.HeartPoints > 0)
            {
                Console.WriteLine($"You have {currentUser.HeartPoints} points, do you want to use 1 point to save handling fee?");
                Console.WriteLine("       Press 1 if you want to use");
                string usePoint = Console.ReadLine();
                if (usePoint == "1")
                {
                    currentUser.HeartPoints -= 1;
                    totalDeduction = amount;
                    currentUser.Balance -= totalDeduction;
                    Console.WriteLine($"Final cost (+0%) = {totalDeduction}");
                    Console.WriteLine($"Successfully withdraw");
                    Console.WriteLine($"Balance : {currentUser.Balance}");
                    currentUser.AddTransaction("3");
                    return;
                }
            }

            currentUser.Balance -= totalDeduction;
            accounts[targetAccountNumber].Balance += amount;

            Console.WriteLine($"Final cost (+10%) = {totalDeduction}");
            Console.WriteLine($"Successfully withdraw");
            Console.WriteLine($"Balance : {currentUser.Balance}");
            currentUser.AddTransaction("3");
        }
        else
        {
            Console.WriteLine("Exceed the valid amount 0~100000");
        }
    }

    static void Donate()
    {
        Console.Write("Enter amount : ");
        string input = Console.ReadLine();

        if (decimal.TryParse(input, out decimal amount) && IsValidAmount(amount))
        {
            if (amount > currentUser.Balance)
            {
                Console.WriteLine("Exceeds the existing amount");
                return;
            }

            currentUser.Balance -= amount;
            int heartPointsEarned = (int)(amount / 1000);
            currentUser.HeartPoints += heartPointsEarned;

            Console.WriteLine("Successfully withdraw");
            Console.WriteLine($"Balance : {currentUser.Balance}");
            currentUser.AddTransaction("4");
        }
        else
        {
            Console.WriteLine("Exceed the valid amount 0~100000");
        }
    }

    static void ShowTransactionHistory()
    {
        if (currentUser.TransactionHistory.Count == 0)
        {
            Console.WriteLine("No transactions found");
            return;
        }

        Console.WriteLine("transaction history");
        foreach (var transaction in currentUser.TransactionHistory)
        {
            Console.WriteLine("       " + transaction);
        }
    }

    static void HiddenFunction()
    {
        Console.WriteLine("Welcome to the backend system");
        Console.WriteLine("Below are the existing accounts and their balances");
        foreach (var account in accounts.Values)
        {
            Console.WriteLine($"       Account : {account.AccountNumber} - {account.Balance}");
        }
    }

    static bool IsValidAmount(decimal amount)
    {
        return amount >= 0 && amount <= 100000;
    }

    static bool IsFiveDigits(string input)
    {
        return input.Length == 5 && input.All(char.IsDigit);
    }
}