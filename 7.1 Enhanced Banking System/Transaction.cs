namespace _5._2P
{
    public abstract class Transaction
    {
        protected decimal _amount;
        public string Status { get; protected set; } = "Pending";
        protected bool _executed;
        protected bool _reversed;
        protected DateTime _dateStamp;

        public bool Executed => _executed;
        public bool Reversed => _reversed;
        public DateTime DateStamp => _dateStamp;

        public Transaction(decimal amount)
        {
            _amount = amount;
        }

        public virtual void Print()
        {

        }

        public virtual void Execute()
        {
            _executed = true;
            _dateStamp = DateTime.Now;
            Status = "Executed";
        }

        public virtual void Rollback()
        {
            if (_reversed)
            {
                throw new InvalidOperationException("This transaction has already been rolled back.");
            }

            _reversed = true;
            _dateStamp = DateTime.Now;
            Status = "Rolled Back";
        }
    }
}