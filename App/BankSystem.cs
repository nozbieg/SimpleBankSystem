using System;
using System.Linq;
using Application.Features.Operations.Command;
using Application.Features.Users.Commands;
using Domain;
using MediatR;

namespace App
{
    public class BankSystem
    {
        BankUser? currentUser;
        readonly IMediator mediator;
        public BankSystem(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public void PrintWelocmeScreen()
        {

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("    ____              __                      __                         ___ ____ ");
            Console.WriteLine("   / __ )____ _____  / /__   _______  _______/ /____  ____ ___     _   _<  // __ \\");
            Console.WriteLine("  / __  / __ `/ __ \\/ //_/  / ___/ / / / ___/ __/ _ \\/ __ `__ \\   | | / / // / / /");
            Console.WriteLine(" / /_/ / /_/ / / / / ,<    (__  ) /_/ (__  ) /_/  __/ / / / / /   | |/ / // /_/ / ");
            Console.WriteLine("/_____/\\__,_/_/ /_/_/|_|  /____/\\__, /____/\\__/\\___/_/ /_/ /_/    |___/_(_)____/  ");
            Console.WriteLine("                               /____/                                            ");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void PrintBaseMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Welcome in your Bank system please select option");
            Console.WriteLine("1. Create account");
            Console.WriteLine("2. Login in to account ");
            Console.WriteLine("3. Exit ");
        }
        public async void TakeBaseInput()
        {
            var input = WaitForInput();

            switch (input)
            {
                case 1:
                    await CreateNewUser();
                    PrintBaseMenu();
                    TakeBaseInput();
                    break;
                case 2:
                    PrintLoginSection();
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
                default:
                    PrintBaseMenu();
                    TakeBaseInput();
                    break;
            }
        }

        void CheckBalance(BankUser currentUser)
        {
            Console.Clear();
            Console.WriteLine("Cheking your balance.");
            Console.WriteLine($"Your current balance: {currentUser.AccountBalance}$");
            Console.WriteLine("Press Enter to contiune");
            Console.ReadLine();
            PrintLoggedUserMenu();
            TakeLoggedInput();
        }
        async Task AddIncome(BankUser currentUser)
        {
            Console.Clear();
            Console.WriteLine("Please insert amount you want deposit on your account your balance.");
            Console.Write($"Amount: ");
            double amount;
            var goodInput = double.TryParse(Console.ReadLine(), out amount);
            while (!goodInput)
            {
                Console.WriteLine("Inserted amount must be a numeric value. Try again");
                goodInput = double.TryParse(Console.ReadLine(), out amount);
            }

            var response = await mediator.Send(new PutIncomeCommand()
            {
                Income = amount,
                UserId = currentUser.Number
            });
            Console.WriteLine(response.Message);
            Console.WriteLine("Press Enter to contiune");
            Console.ReadLine();
            PrintLoggedUserMenu();
            TakeLoggedInput();
        }
        async Task DoTransfer(BankUser curentUser)
        {
            Console.Clear();
            Console.WriteLine("Please insert amount you want transfer to other account.");
            Console.Write($"Amount: ");
            double amount;
            var goodInput = double.TryParse(Console.ReadLine(), out amount);
            while (!goodInput)
            {
                Console.WriteLine("Inserted amount must be a numeric value. Try again");
                goodInput = double.TryParse(Console.ReadLine(), out amount);
            }
            Console.WriteLine("Please insert acount number where want to transfer cash.");

            var to_userId = Console.ReadLine();
            while (to_userId == null)
            {
                Console.WriteLine("Inserted account number cant be null. Try again");
                to_userId = Console.ReadLine();
            }
            var response = await mediator.Send(new TransferMoneyCommand()
            {
                Amount = amount,
                To_UserId = to_userId,
                From_UserId = currentUser.Number
            });

            Console.WriteLine(response.Message);
            Console.WriteLine("Press Enter to contiune");
            Console.ReadLine();
            PrintLoggedUserMenu();
            TakeLoggedInput();
        }
        async Task CloseAccount(BankUser currentUser)
        {
            Console.ReadLine();
            Console.WriteLine("Are You sure you want close your account?");
            Console.WriteLine("Write 'confirm' to confirm :)");
            var confirm = Console.ReadLine();
            if (confirm == "confirm")
            {
                var result = await mediator.Send(new DeleteUserCommand()
                {
                    UserId = currentUser.Number,
                });

                Console.WriteLine(result.Message);
            }
            PrintLoggedUserMenu();
            TakeLoggedInput();

        }
        void LogOut()
        {
            Console.ReadLine();
            Console.WriteLine("Loging out... Pres enter to continue");
            Console.ReadLine();
            currentUser = null;
            PrintBaseMenu();
            TakeBaseInput();
        }
        public async void TakeLoggedInput()
        {
            if (currentUser == null)
            {
                throw new Exception("UserError, Not logged correctly");
            }

            var input = WaitForInput();
            switch (input)
            {
                case 1:
                    CheckBalance(currentUser);
                    break;
                case 2:
                    await AddIncome(currentUser);
                    break;
                case 3:
                    await DoTransfer(currentUser);
                    break;
                case 4:
                    await CloseAccount(currentUser);
                    break;
                case 5:
                    LogOut();
                    break;
                case 6:
                    Environment.Exit(0);
                    break;
                default:
                    PrintLoggedUserMenu();
                    TakeLoggedInput();
                    break;
            }
        }

        void PrintLoggedUserMenu()
        {
            Console.Clear();
            Console.WriteLine("Welcome in your account. Chosee an option");
            Console.WriteLine("1. Balance");
            Console.WriteLine("2. Add income");
            Console.WriteLine("3. Do transfer");
            Console.WriteLine("4. Close account");
            Console.WriteLine("5. Log out ");
            Console.WriteLine("6. Exit");

        }
        async void PrintLoginSection()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("Please enter your creditentials:");
            Console.Write("Account number: ");
            var accountNumber = Console.ReadLine();
            while (accountNumber == null)
            {
                Console.WriteLine("Account number cant be empty. Try again");
                accountNumber = Console.ReadLine();
            }
            Console.Write("Pin: ");
            var pin = Console.ReadLine();
            while (pin == null)
            {
                Console.WriteLine("Pin cant be empty. Try again");
                pin = Console.ReadLine();
            }

            var response = await mediator.Send(new LogInCommand()
            {
                UserId = accountNumber,
                Pin = pin
            });

            if (response.Success)
            {
                currentUser = response.BankUser;
                PrintLoggedUserMenu();
                TakeLoggedInput();
            }
            else
            {
                Console.WriteLine("Incorrect creditentials. Returning to menu. Press enter.");
                Console.ReadKey();
                PrintBaseMenu();
                TakeBaseInput();
            }

        }

        async Task CreateNewUser()
        {
            Console.Clear();
            Console.WriteLine("Creating Account");
            var response = await mediator.Send(new CreateUserCommand());
            Console.WriteLine(response.Message);
            Console.WriteLine("Press enter to return to menu.");
            Console.ReadLine();
        }

        int WaitForInput()
        {
            int parameter;
            var success = int.TryParse(Console.ReadLine(), out parameter);
            if (!success)
            {
                Console.WriteLine($"Please select only numbers from list. Try again");
                WaitForInput();
            }
            return parameter;
        }
    }
}
