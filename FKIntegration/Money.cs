namespace FKIntegration.Documents
{
    public class Money
    {
        private readonly decimal _amount;
        private readonly CurrencyEnum _currency;

        public Money(decimal amount, CurrencyEnum currency)
        {
            _amount = amount;
            _currency = currency;
        }

        public Money(decimal amount)
            : this(amount, CurrencyEnum.PLN)
        { }

        public bool Equals(Money other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other._amount == _amount && Equals(other._currency, _currency);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Money)) return false;
            return Equals((Money) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_amount.GetHashCode()*397) ^ _currency.GetHashCode();
            }
        }

        public decimal Amount
        {
            get { return _amount; }
        }

        public CurrencyEnum Currency1
        {
            get { return _currency; }
        }
    }
}