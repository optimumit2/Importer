using System;
using MXDokFK;

namespace FKIntegration.Documents
{
    public class BreakDown
    {
        private readonly Position _position;
        private readonly AccountSide _accountSide;

        public AccountSide AccountSide
        {
            get { return _accountSide; }
        }

        internal BreakDown(Position position, AccountSide accountSide)
        {
            _position = position;
            _accountSide = accountSide;
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        private decimal _amount;
        public decimal Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }

        private string _account;
        public string Account
        {
            get { return _account; }
            set { _account = value; }
        }

        private DateTime? _paymentDate;
        public void WithTransaction(DateTime paymentDate)
        {
            _paymentDate = paymentDate;
        }

        public Position AddCreditPosition(string description)
        {
            return _position.AddCreditPosition(description, CurrencyEnum.PLN, 10, null, null);
        }

        protected virtual Position AddDebitPosition()
        {
            return AddDebitPosition(_position.Document.Content);
        }

        protected virtual Position AddDebitPosition(string description)
        {
            return AddDebitPosition(description, CurrencyEnum.PLN, 0, null, description, 0, null);
        }

        protected virtual Position AddDebitPosition(string description, decimal amount, string debitAccount, string creditAccount)
        {
            return AddDebitPosition(description, CurrencyEnum.PLN, amount, debitAccount, creditAccount);
        }

        protected virtual Position AddDebitPosition(string description, CurrencyEnum currency, decimal amount, string debitAccount, string creditAccount)
        {
            return AddDebitPosition(description, currency, amount, debitAccount, amount, creditAccount);
        }

        protected virtual Position AddDebitPosition(string description, decimal debitAmount, string debitAccount, decimal creditAmount, string creditAccount)
        {
            return AddDebitPosition(description, CurrencyEnum.PLN, debitAmount, debitAccount, creditAmount, creditAccount);
        }

        protected virtual Position AddDebitPosition(string description, CurrencyEnum currency, decimal debitAmount, string debitAccount, decimal creditAmount, string creditAccount)
        {
            return AddDebitPosition(description, currency, debitAmount, debitAccount, description, creditAmount,
                                    creditAccount);
        }

        protected virtual Position AddDebitPosition(string debitDescription, CurrencyEnum currency, decimal debitAmount, string debitAccount, string creditDescription, decimal creditAmount, string creditAccount)
        {
            return _position.AddDebitPosition(debitDescription, currency, debitAmount, debitAccount, creditDescription,
                                              creditAmount, creditAccount);
        }

        protected virtual Position AddDebitPosition(decimal amount, string debitaccount, string creditaccount)
        {
            throw new NotImplementedException();
        }

        internal void AddBreakDown(PDokumentClass pDokumentClass)
        {
            pDokumentClass.Grupa.Rozbicie = (short)_accountSide;
            pDokumentClass.Grupa.Zapis.Insert();

            pDokumentClass.Grupa.Zapis["kwota"] = (double)_amount;
            pDokumentClass.Grupa.Zapis["opis"] = _description;
            string[] splitedCreditAccount = _account.Split('-');

            pDokumentClass.Grupa.Zapis["Synt"] = splitedCreditAccount[0];
            int creditAccountPosition = 1;
            if (splitedCreditAccount.Length > 1)
                while (!string.IsNullOrEmpty(splitedCreditAccount[creditAccountPosition]))
                {
                    pDokumentClass.Grupa.Zapis["Poz" + creditAccountPosition] = splitedCreditAccount[creditAccountPosition];
                    creditAccountPosition++;
                }
        }

        public void Save()
        {
            _position.Save();
        }
    }
}