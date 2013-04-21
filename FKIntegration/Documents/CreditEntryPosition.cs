namespace FKIntegration.Documents
{
    internal class CreditEntryPosition : Position
    {
        internal CreditEntryPosition(Document document, int position)
            : base(document, position)
        { }

        public override BreakDown AddBreakDown(string description, decimal amount, string account)
        {
            return AddBreakDown(AccountSide.Credit, description, amount, account);
        }

        public override Position AddDebitPosition(string debitdescription, CurrencyEnum currencyEnum, decimal debitAmount, string debitAccount, string creditdescription, decimal creditAmount, string creditAccount)
        {
            throw new FKIntegrationException();
        }

        public override Position AddDebitPosition(string description, CurrencyEnum currency, decimal debitAmount, string debitAccount, decimal creditAmount, string creditAccount)
        {
            throw new FKIntegrationException();
        }

        public override Position AddDebitPosition(string description, CurrencyEnum currencyEnum, decimal amount, string debitAccount, string creditAccount)
        {
            throw new FKIntegrationException();
        }
    }
}