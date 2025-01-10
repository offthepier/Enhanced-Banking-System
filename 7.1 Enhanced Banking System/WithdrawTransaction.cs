using _5._2P;
using System.Net.NetworkInformation;

public class WithdrawTransaction : Transaction
{
    public Account FromAccount { get; private set; }
    public decimal Amount { get; private set; }

    public WithdrawTransaction(Account fromAccount, decimal amount) : base(amount)
    {
        FromAccount = fromAccount ?? throw new ArgumentNullException(nameof(fromAccount));
        Amount = amount;
    }

    public override void Execute()
    {
        if (Reversed)
        {
            throw new InvalidOperationException("Cannot execute a reversed transaction.");
        }

        if (FromAccount.Withdraw(Amount))
        {
            base.Execute();
        }
        else
        {
            Status = "Failed";
        }
    }

    public override void Rollback()
    {
        if (!Reversed)
        {
            FromAccount.Deposit(Amount);
            base.Rollback();
        }
    }

    public override void Print()
    {
        Console.WriteLine($"Withdrawal of ${Amount} from {FromAccount.Name} - Status: {Status}");
    }
}
