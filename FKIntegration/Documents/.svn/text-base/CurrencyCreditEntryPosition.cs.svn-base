namespace FKIntegration.Documents
{
    internal class CurrencyCreditEntryPosition : CreditEntryPosition
    {
        internal CurrencyCreditEntryPosition(Document document, int number, CurrencyEnum currency)
            : base(document, number)
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