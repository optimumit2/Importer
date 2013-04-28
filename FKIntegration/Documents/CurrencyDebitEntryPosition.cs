namespace FKIntegration.Documents
{
    internal class CurrencyDebitEntryPosition : DebitEntryPosition
    {
        internal CurrencyDebitEntryPosition(Document document, int position, CurrencyEnum currency)
            : base(document, position)
        {
            _currency = currency;
        }

        public override CurrencyEnum Currency
        {
            get { return base.Currency; }
            set { throw new FKIntegrationException(); }
        }
    }
}