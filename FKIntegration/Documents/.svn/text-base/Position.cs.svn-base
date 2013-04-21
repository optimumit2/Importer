using System;
using System.Collections.Generic;
using MXDokFK;

namespace FKIntegration.Documents
{
    public abstract class Position
    {
        internal Position(Document document, int number)
        {
            _document = document;
            _number = number;
            _currency = CurrencyEnum.PLN;
        }

        private int _id;
        protected Document _document;
        protected int _number;
        protected CurrencyEnum _currency;
        private decimal _debitAmount;
        private decimal _creditAmount;
        private string _debitDescription;
        private string _debitAccount;
        private string _creditAccount;
        private string _cretidDescription;

        internal int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int Number
        {
            get { return _number; }
        }

        public virtual CurrencyEnum Currency
        {
            get { return _currency; }
            set { _currency = value; }
        }

        public virtual decimal DebitAmount
        {
            get { return _debitAmount; }
            set { _debitAmount = value; }
        }

        public virtual decimal CreditAmount
        {
            get { return _creditAmount; }
            set { _creditAmount = value; }
        }

        public virtual string DebitDescription
        {
            get { return _debitDescription; }
            set { _debitDescription = value; }
        }

        public virtual string DebitAccount
        {
            get { return _debitAccount; }
            set { _debitAccount = value; }
        }

        public virtual string CreditAccount
        {
            get { return _creditAccount; }
            set { _creditAccount = value; }
        }

        public virtual string CreditDescription
        {
            get { return _cretidDescription; }
            set { _cretidDescription = value; }
        }

        protected virtual BreakDown AddBreakDown(AccountSide accountSide, string description, decimal amount, string account)
        {
            var breakDown = new BreakDown(this, accountSide)
                                {
                                    Description = description,
                                    Amount = amount,
                                    Account = account
                                };
            _breakDownList.Add(breakDown);
            return breakDown;
        }

        private readonly List<BreakDown> _breakDownList = new List<BreakDown>();
        public virtual List<BreakDown> BreadkDownList
        {
            get { return _breakDownList; }
        }

        public Document Document
        {
            get { return _document; }
        }

        public virtual Position AddDebitPosition(string debitdescription, CurrencyEnum currencyEnum, decimal debitAmount, string debitAccount, string creditdescription, decimal creditAmount, string creditAccount)
        {
            throw new NotImplementedException();
        }

        public virtual Position AddDebitPosition(string description, CurrencyEnum currencyEnum, decimal amount, string debitAccount, string creditAccount)
        {
            throw new NotImplementedException();
        }

        public virtual Position AddDebitPosition(string description, CurrencyEnum currency, decimal debitAmount, string debitAccount, decimal creditAmount, string creditAccount)
        {
            throw new NotImplementedException();
        }

        public virtual Position AddCreditPosition(string debitdescription, CurrencyEnum currencyEnum, decimal debitAmount, string debitAccount, string creditdescription, decimal creditAmount, string creditAccount)
        {
            throw new NotImplementedException();
        }

        public virtual Position AddCreditPosition(string description, CurrencyEnum currencyEnum, decimal amount, string debitAccount, string creditAccount)
        {
            throw new NotImplementedException();
        }

        public virtual Position AddCreditPosition(string description, CurrencyEnum currency, decimal debitAmount, string debitAccount, decimal creditAmount, string creditAccount)
        {
            throw new NotImplementedException();
        }

        public virtual BreakDown AddBreakDown(string description, decimal amount, string account)
        {
            throw new NotImplementedException();
        }

        internal void AddPosition(PDokumentClass pDokumentClass)
        {
            pDokumentClass.Grupa.Insert();
            pDokumentClass.Grupa.Zapis["kwota"] = (double)_debitAmount;
            pDokumentClass.Grupa.Zapis["opis"] = _debitDescription;
            string[] splitedDebitAccount = _debitAccount.Split('-');

            pDokumentClass.Grupa.Zapis["Synt"] = splitedDebitAccount[0];
            int debitAccountPosition = 1;
            if (splitedDebitAccount.Length > 1)
                while (!string.IsNullOrEmpty(splitedDebitAccount[debitAccountPosition]))
                {
                    pDokumentClass.Grupa.Zapis["Poz" + debitAccountPosition] = splitedDebitAccount[debitAccountPosition];
                    debitAccountPosition++;
                }

            pDokumentClass.Grupa.Zapis.Insert();

            pDokumentClass.Grupa.Zapis["kwota"] = (double)_creditAmount;
            pDokumentClass.Grupa.Zapis["opis"] = _cretidDescription;
            string[] splitedCreditAccount = _creditAccount.Split('-');

            pDokumentClass.Grupa.Zapis["Synt"] = splitedCreditAccount[0];
            int creditAccountPosition = 1;
            if (splitedCreditAccount.Length > 1)
                while (!string.IsNullOrEmpty(splitedCreditAccount[creditAccountPosition]))
                {
                    pDokumentClass.Grupa.Zapis["Poz" + creditAccountPosition] = splitedCreditAccount[creditAccountPosition];
                    creditAccountPosition++;
                }

            foreach (var eachBreakDown in BreadkDownList)
            {
                eachBreakDown.AddBreakDown(pDokumentClass);
            }

        }

        public void Save()
        {
            _document.Save();
        }

        public virtual BreakDown AddBreakDown()
        {
            throw new NotImplementedException();
        }
    }
}