using System;

namespace _5._2P

{
    enum MenuOption
    {
        Withdraw = 1,
        Deposit,
        Print,
        Transfer,
        AddAccount,
        PrintTransactionHistory,
        Rollback,
        Quit
    }

    public class Account
    {
        private decimal _balance;
        private string _name;

        public Account(string name, decimal balance)
        {
            _name = name;
            _balance = balance;
        }
        public decimal Balance
        {
            get { return _balance; }
        }
        public bool Deposit(decimal amount)
        {
            if (amount > 0)
            {
                _balance += amount;
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Withdraw(decimal amount)
        {
            if (amount > 0 && amount <= _balance)
            {
                _balance -= amount;
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Print()
        {
            Console.WriteLine("Name: " + _name + " your balance: " + _balance);
        }
        public string Name
        {
            get { return _name; }
        }
    }
}

