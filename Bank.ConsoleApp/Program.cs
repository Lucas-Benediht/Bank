using Bank.Core.Models;
using Bank.Core.Controller;
using Bank.Core.Interface;
using Bank.Infra.Context;
using Bank.Infra.Repository;
using System;

namespace Bank.ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Cria o banco se não existir
            using (var db = new BankContext())
            {
                db.Database.EnsureCreated();
            }

            using var context = new BankContext();
            var repository = new AccountRepository(context);
            var controller = new AccountController(repository);

            while (true)
            {
                Console.WriteLine("\n--- Bank Console ---");
                Console.WriteLine("1 - Create Account");
                Console.WriteLine("2 - List Accounts");
                Console.WriteLine("3 - Deposit");
                Console.WriteLine("4 - Withdraw");
                Console.WriteLine("0 - Exit");
                Console.Write("Select an option: ");

                var option = Console.ReadLine();

                try
                {
                    switch (option)
                    {
                        case "1":
                            Console.Write("Account number: ");
                            int number = int.Parse(Console.ReadLine()!);
                            Console.Write("Holder name: ");
                            string holder = Console.ReadLine()!;
                            // Passando balance inicial 0
                            controller.CreateAccount(number, holder, 0m);
                            Console.WriteLine("Account created successfully!");
                            break;

                        case "2":
                            foreach (var account in controller.GetAllAccounts())
                            {
                                // Corrigido nomes das propriedades
                                Console.WriteLine($"Account {account.AccountNumber} - Holder: {account.HolderName} - Balance: {account.Balance:C}");
                            }
                            break;

                        case "3":
                            Console.Write("Account number: ");
                            number = int.Parse(Console.ReadLine()!);
                            Console.Write("Deposit amount: ");
                            decimal depositAmount = decimal.Parse(Console.ReadLine()!);
                            controller.Deposit(number, depositAmount);
                            Console.WriteLine("Deposit successful!");
                            break;

                        case "4":
                            Console.Write("Account number: ");
                            number = int.Parse(Console.ReadLine()!);
                            Console.Write("Withdrawal amount: ");
                            decimal withdrawAmount = decimal.Parse(Console.ReadLine()!);
                            controller.Withdraw(number, withdrawAmount);
                            Console.WriteLine("Withdrawal successful!");
                            break;

                        case "0":
                            return;

                        default:
                            Console.WriteLine("Invalid option.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}
