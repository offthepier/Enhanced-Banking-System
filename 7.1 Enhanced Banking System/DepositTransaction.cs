using _5._2P;
using System.Net.NetworkInformation;

public class DepositTransaction : Transaction
{
    public Account ToAccount { get; private set; }
    public decimal Amount { get; private set; }

    public DepositTransaction(Account toAccount, decimal amount) : base(amount)
    {
        ToAccount = toAccount ?? throw new ArgumentNullException(nameof(toAccount));
        Amount = amount;
    }
    public override void Execute()
    {
        if (Reversed)
        {
            throw new InvalidOperationException("Cannot execute a reversed transaction.");
        }

        ToAccount.Deposit(Amount);
        base.Execute();
    }
    public override void Rollback()
    {
        if (!Reversed)
        {
            ToAccount.Withdraw(Amount);
            base.Rollback();
        }
    }
    public override void Print()
    {
        Console.WriteLine($"Deposit of ${Amount} to {ToAccount.Name} - Status: {Status}");
    }
}
