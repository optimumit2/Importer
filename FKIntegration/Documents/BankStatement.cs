using System;
using MXDokFK;

namespace FKIntegration.Documents
{
    public class BankStatement : Document
    {
        public BankStatement(string shortCut)
            : base(shortCut)
        { }

        //TODO: Sprawdz czy to tez nie ma byc przypadkiem ustawiane

        private DateTime _reportingPeriodDate;

        public DateTime ReportingPeriodDate
        {
            get { return _reportingPeriodDate; }
            set { _reportingPeriodDate = value; }
        }

        private Money _amount;

        public Money Amount
        {
            get { return _amount; }
        }

        private Money _parallelAccountAmount;

        public Money ParallelAccountAmount
        {
            get { return _parallelAccountAmount; }
        }

        private Money _totalAmount;

        public Money TotalAmount
        {
            get { return _totalAmount; }
        }

        protected override void FillOtherShitStuffOfDocument(PDokumentClass pDokumentClass)
        {
            pDokumentClass["dataokr"] = ReportingPeriodDate;
        }

        public BankStatement FillBody(DateTime documentDate, string number, DateTime reportingPeriodDate, string content, DocumentMark mark)
        {
            _documentDate = documentDate;
            _number = number;
            _reportingPeriodDate = reportingPeriodDate;
            _content = content;
            _documentMark = mark;
            return this;
        }

        public BankStatement FillBody(DateTime documentDate, string number, DateTime reportingPeriodDate, string content)
        {
            return FillBody(documentDate, number, reportingPeriodDate, content, DocumentMark.Empty);
        }

        public BankStatement FillBody(DateTime documentDate, string number, string content)
        {
            return FillBody(documentDate, number, documentDate, content);
        }

        public BankStatement FillBody(string number, string content)
        {
            return FillBody(DateTime.Now, number, content);
        }

        public Position AddDebitPosition()
        {
            return base.AddDebitPositionBase();
        }

        public Position AddDebitPosition(string description)
        {
            return base.AddDebitPositionBase(description);
        }

        public Position AddDebitPosition(string debitDescription, CurrencyEnum currency, decimal debitAmount, string debitAccount, string creditDescription, decimal creditAmount, string creditAccount)
        {
            return base.AddDebitPositionBase(debitDescription, currency, debitAmount, debitAccount, creditDescription,
                                             creditAmount, creditAccount);
        }

        public Position AddDebitPosition(string description, CurrencyEnum currency, decimal creditAmount, string creditAccount, decimal debitAmount, string debitAccount)
        {
            return base.AddDebitPositionBase(description, currency, creditAmount, creditAccount, debitAmount,
                                             debitAccount);
        }

        public Position AddDebitPosition(string description, decimal debitAmount, string debitAccount, decimal creditAmount, string creditAccount)
        {
            return base.AddDebitPositionBase(description, debitAmount, debitAccount, creditAmount, creditAccount);
        }

        public Position AddDebitPosition(string description, decimal amount, string debitAccount, string creditAccount)
        {
            return base.AddDebitPositionBase(description, amount, debitAccount, creditAccount);
        }

        public Position AddDebitPosition(decimal amount, string debitaccount, string creditaccount)
        {
            return base.AddDebitPositionBase(amount, debitaccount, creditaccount);
        }

        public Position AddDebitPosition(string description, CurrencyEnum currency, decimal amount, string debitAccount, string creditAccount)
        {
            return base.AddDebitPositionBase(description, currency, amount, debitAccount, creditAccount);
        }

        public Position AddCreditEntryPosition(string description, CurrencyEnum currency, decimal debitAmount, string debitAccount, decimal creditAmount, string creditAccount)
        {
            return AddCreditEntryPositionBase(description, currency, debitAmount, debitAccount, creditAmount,
                                               creditAccount);
        }
    }
}
