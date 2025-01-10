using System;
using System.Security.Principal;

namespace _5._2P
{
    class BankSystem
    {
        static Bank bank = new Bank();

        static MenuOption ReadUserOption()
        {
            MenuOption option;
            do
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1: Withdraw");
                Console.WriteLine("2: Deposit");
                Console.WriteLine("3: Print");
                Console.WriteLine("4: Transfer");
                Console.WriteLine("5: Add account");
                Console.WriteLine("6: Print Transaction History");
                Console.WriteLine("7: Rollback Transaction");
                Console.WriteLine("8: Quit");

                if (int.TryParse(Console.ReadLine(), out int input) && Enum.IsDefined(typeof(MenuOption), input))
                {
                    option = (MenuOption)input;
                    break;
                }
                Console.WriteLine("Invalid option, please select from the given options.");
            } while (true);
            return option;
        }

        static void PrintTransactionHistory()
        {
            bank.PrintTransactionHistory();

            Console.WriteLine("Enter the transaction number to rollback or 0 to cancel:");
            if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= bank.TransactionsCount)
            {
                DoRollback(choice - 1);
            }
            else
            {
                Console.WriteLine("Invalid transaction number.");
            }
        }

        static Account CreateAccount()
        {
            Console.WriteLine("Enter the account name:");
            string accountName = Console.ReadLine();

            Console.WriteLine("Enter the starting balance:");
            decimal startingBalance;
            if (decimal.TryParse(Console.ReadLine(), out startingBalance) && startingBalance >= 0)
            {
                return new Account(accountName, startingBalance);
            }
            else
            {
                Console.WriteLine("Invalid starting balance. Please enter a non-negative number.");
                return null;
            }
        }

        static void DoDeposit()
        {
            Account account = FindAccount();
            if (account != null)
            {
                Console.WriteLine("Enter the amount you want to deposit:");
                decimal amount;
                if (decimal.TryParse(Console.ReadLine(), out amount) && amount > 0)
                {
                    DepositTransaction deposit = new DepositTransaction(account, amount);
                    bank.ExecuteTransaction(deposit); 
                    deposit.Print();
                }
                else
                {
                    Console.WriteLine("Invalid amount. Please enter a positive number.");
                }
            }
        }

        static void DoWithdraw()
        {
            Account account = FindAccount();

            if (account != null)
            {
                Console.WriteLine("Enter the amount you want to withdraw:");
                decimal amount;
                if (decimal.TryParse(Console.ReadLine(), out amount) && amount > 0)
                {
                    if (account.Balance >= amount)
                    {
                        WithdrawTransaction withdrawal = new WithdrawTransaction(account, amount);
                        bank.ExecuteTransaction(withdrawal); 
                        withdrawal.Print();
                    }
                    else
                    {
                        Console.WriteLine("Withdrawal failed. Insufficient funds in the account.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid amount. Please enter a positive number.");
                }
            }
            else
            {
                Console.WriteLine("Account not found.");
            }
        }

        static void DoPrint()
        {
            Account account = FindAccount();
            if (account != null)
            {
                account.Print();
            }
            else
            {
                Console.WriteLine("Account not found.");
            }
        }

        static void DoRollback(int transactionIndex)
        {
            try
            {
                bank.RollbackTransaction(transactionIndex);
                Console.WriteLine("Transaction rolled back successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error rolling back transaction: {ex.Message}");
            }
        }


        static void DoTransfer()
        {
            Console.WriteLine("Enter the source account name:");
            Account fromAccount = FindAccount();

            if (fromAccount != null)
            {
                Console.WriteLine("Enter the destination account name:");
                Account toAccount = FindAccount();

                if (toAccount != null)
                {
                    Console.WriteLine("Enter the amount you want to transfer:");
                    decimal amount;
                    if (decimal.TryParse(Console.ReadLine(), out amount) && amount > 0)
                    {
                        if (fromAccount.Balance >= amount)
                        {
                            TransferTransaction transfer = new TransferTransaction(fromAccount, toAccount, amount);
                            bank.ExecuteTransaction(transfer);
                            transfer.Print();
                        }
                        else
                        {
                            Console.WriteLine("Transfer failed. Insufficient funds in the source account.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid amount. Please enter a positive number.");
                    }
                }
                else
                {
                    Console.WriteLine("Destination account not found.");
                }
            }
            else
            {
                Console.WriteLine("Source account not found.");
            }
        }

        private static Account FindAccount()
        {
            Console.WriteLine("Enter the account name:");
            string accountName = Console.ReadLine();
            return bank.GetAccount(accountName);
        }

        static void Main(string[] args)
        {
            Account account1 = new Account("Daya", 99990);
            Account account2 = new Account("Kshama", 90000);
            bank.AddAccount(account1);
            bank.AddAccount(account2);

            while (true)
            {
                MenuOption option = ReadUserOption();
                switch (option)
                {
                    case MenuOption.Withdraw:
                        Console.WriteLine("Processing Withdraw.");
                        DoWithdraw();
                        break;
                    case MenuOption.Deposit:
                        Console.WriteLine("Processing Deposit.");
                        DoDeposit();
                        break;
                    case MenuOption.Print:
                        Console.WriteLine("Processing Print.");
                        DoPrint();
                        break;
                    case MenuOption.Transfer:
                        Console.WriteLine("Processing Transfer.");
                        DoTransfer();
                        break;
                    case MenuOption.AddAccount:
                        Console.WriteLine("Processing Add new account.");
                        Account newAccount = CreateAccount();
                        if (newAccount != null)
                        {
                            bank.AddAccount(newAccount);
                        }
                        break;
                    case MenuOption.PrintTransactionHistory:
                        Console.WriteLine("Printing Transaction History.");
                        PrintTransactionHistory();
                        break;
                    case MenuOption.Rollback:
                        Console.WriteLine("Processing Rollback Transaction.");
                        Console.WriteLine("Enter the transaction number to rollback:");
                        if (int.TryParse(Console.ReadLine(), out int choice))
                        {
                            DoRollback(choice - 1);  // Adjusting for 0-based index
                        }
                        else
                        {
                            Console.WriteLine("Invalid transaction number.");
                        }
                        break;


                    case MenuOption.Quit:
                        Console.WriteLine("Processing Quit.");
                        return;
                }
            }
        }
    }
}
