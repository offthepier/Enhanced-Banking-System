using _5._2P;

public class TransferTransaction : Transaction
{
    public Account FromAccount { get; private set; }
    public Account ToAccount { get; private set; }
    public decimal Amount { get; private set; }

    private WithdrawTransaction _associatedWithdraw;
    private DepositTransaction _associatedDeposit;

    public TransferTransaction(Account fromAccount, Account toAccount, decimal amount) : base(amount)
    {
        FromAccount = fromAccount ?? throw new ArgumentNullException(nameof(fromAccount));
        ToAccount = toAccount ?? throw new ArgumentNullException(nameof(toAccount));
        Amount = amount;
    }
    public override void Execute()
    {
        if (Reversed)
        {
            throw new InvalidOperationException("Cannot execute a reversed transaction.");
        }

        _associatedWithdraw = new WithdrawTransaction(FromAccount, Amount);
        _associatedDeposit = new DepositTransaction(ToAccount, Amount);

        _associatedWithdraw.Execute();
        _associatedDeposit.Execute();

        base.Execute();
    }
    public override void Rollback()
    {
        if (!Reversed)
        {
            if (ToAccount.Balance < Amount)
            {
                throw new InvalidOperationException($"Cannot rollback transfer. Insufficient funds in {ToAccount.Name}'s account.");
            }

            FromAccount.Deposit(Amount);
            ToAccount.Withdraw(Amount);
            base.Rollback();
        }
    }
    public override void Print()
    {
        Console.WriteLine($"Transferred ${Amount} from {FromAccount.Name}'s account to {ToAccount.Name}'s account");
    }
}
