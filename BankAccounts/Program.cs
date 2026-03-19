using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccounts
{
    // The BankAccount class holds the logic for the account
    internal class BankAccount
    {
        public string Owner;
        public int balance;

        public void ShowBalance() => Console.WriteLine("Your total balance is " + balance + " " + Owner);
        
        public void Deposit(int amount)
        {
            balance += amount;
            Console.WriteLine(Owner + " has $" + balance);
        }

        public void Withdraw(int amount)
        {
            if (balance >= amount)
            {
                balance -= amount;
                Console.WriteLine("You withdrew " + amount);
                Console.WriteLine("New balance: " + balance);
            }
            else
            {
                Console.WriteLine("You don't have enough money to withdraw that amount");
            }
        }
    }

    // The Program class contains the Main method, which is the entry point
    internal class Program
    {
        static void Main(string[] args)
        {
            BankAccount account = new BankAccount();
            account.Owner = "John Doe";
            account.balance = 500;

            bool running = true;
            while (running)
            {
                Console.WriteLine("1. Show Balance. \n2. Deposit. \n3. Withdraw. \n4. Exit.");
                int choice = Convert.ToInt32(Console.ReadLine());
                
                switch (choice)
                {
                    case 1:
                        account.ShowBalance();
                        break;
                    case 2:
                        Console.WriteLine("Enter amount to deposit:");
                        int depositAmount = Convert.ToInt32(Console.ReadLine());
                        account.Deposit(depositAmount);
                        break;
                    case 3:
                        Console.WriteLine("Enter amount to withdraw:");
                        int withdrawAmount = Convert.ToInt32(Console.ReadLine());
                        account.Withdraw(withdrawAmount);
                        break;
                    case 4:
                        running = false;
                        Console.WriteLine("Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}
