using System;
using System.Collections.Generic;

namespace _5._2P
{
    class Bank
    {
        private List<Account> accounts;
        private List<Transaction> transactions;

        public Bank()
        {
            accounts = new List<Account>();
            transactions = new List<Transaction>();
        }
        public bool CanRollback(int index)
        {
            if (index >= transactions.Count - 1)
            {
                return true; 
            }
            for (int i = index + 1; i < transactions.Count; i++)
            {
                if (transactions[i].Reversed)
                {
                    return false;
                }
            }
            return true;
        }
        public void AddAccount(Account account)
        {
            accounts.Add(account);
        }
        public Account GetAccount(string name)
        {
            return accounts.Find(account => account.Name == name);
        }
        public void ExecuteTransaction(Transaction transaction)
        {
            transactions.Add(transaction);
            transaction.Execute();
        }
        public void RollbackTransaction(int index)
        {
            if (index >= 0 && index < transactions.Count)
            {
                Transaction transaction = transactions[index];
                if (transaction.Reversed)
                {
                    throw new InvalidOperationException("This transaction has already been rolled back.");
                }

                transaction.Rollback();
            }
            else
            {
                throw new InvalidOperationException("Invalid transaction number.");
            }
        }
        public void PrintTransactionHistory()
        {
            if (transactions.Count == 0)
            {
                Console.WriteLine("No transactions to display.");
                return;
            }
            for (int i = 0; i < transactions.Count; i++)
            {
                Console.WriteLine($"Transaction {i + 1}:");
                transactions[i].Print();
                Console.WriteLine($"Timestamp: {transactions[i].DateStamp}");
                Console.WriteLine("-----------------------------");
            }
        }
        public int TransactionsCount => transactions.Count;
        public Transaction GetTransaction(int index)
        {
            if (index >= 0 && index < transactions.Count)
            {
                return transactions[index];
            }
            return null;
        }
    }
}
