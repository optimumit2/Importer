namespace FKIntegration.Documents
{
    internal class DebitEntryPosition : Position
    {
        internal DebitEntryPosition(Document document, int position)
            : base(document, position)
        { }

        public override BreakDown AddBreakDown(string description, decimal amount, string account)
        {
            return AddBreakDown(AccountSide.Debit, description, amount, account);
        }

        public override BreakDown AddBreakDown()
        {
            return AddBreakDown(null, 0, null);
        }

        public override Position AddCreditPosition(string debitdescription, CurrencyEnum currencyEnum, decimal debitAmount, string debitAccount, string creditdescription, decimal creditAmount, string creditAccount)
        {
            throw new FKIntegrationException();
        }

        public override Position AddCreditPosition(string description, CurrencyEnum currency, decimal debitAmount, string debitAccount, decimal creditAmount, string creditAccount)
        {
            throw new FKIntegrationException();
        }

        public override Position AddCreditPosition(string description, CurrencyEnum currencyEnum, decimal amount, string debitAccount, string creditAccount)
        {
            throw new FKIntegrationException();
        }
    }
}