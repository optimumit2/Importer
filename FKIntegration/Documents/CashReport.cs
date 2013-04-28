using System;
using System.Collections.Generic;

namespace FKIntegration.Documents
{
    public class CashReport : Document
    {
        public CashReport(string shortCut)
            : base(shortCut)
        { }

        public CashReport FillBody(DateTime documentDate, string number, string cashAccount, decimal openBalance, string content)
        {
            return FillBody(documentDate, number, cashAccount, openBalance, AccountSide.Debit, content);
        }

        public CashReport FillBody(DateTime documentDate, string number, string cashAccount, decimal openBalance, AccountSide openBalanceSide, string content)
        {
            return FillBody(documentDate, number, cashAccount, openBalance, openBalanceSide, content,
                            DocumentMark.Empty);
        }

        public CashReport FillBody(DateTime documentDate, string number, string cashAccount, decimal openBalance, AccountSide openBalanceSide, string content, DocumentMark documentMark)
        {
            _documentDate = documentDate;
            _number = number;
            _cashAccount = cashAccount;
            _openBalance = openBalance;
            _openBalanceSide = openBalanceSide;
            _content = content;
            _documentMark = documentMark;
            return this;
        }

        private readonly List<CashReportPosition> _cashReportPositionList = new List<CashReportPosition>();
        public List<CashReportPosition> PositionList
        {
            get { return _cashReportPositionList; }
        }

        public CashReportPosition AddPosition()
        {
            var position = new CashReportPosition(this, _positionNumber);
            _cashReportPositionList.Add(position);
            _positionNumber++;
            return position;
        }

        private string _cashAccount;
        public string CashAccount
        {
            get { return _cashAccount; }
            set { _cashAccount = value; }
        }

        private decimal _openBalance;
        public decimal OpenBalance
        {
            get { return _openBalance; }
            set { _openBalance = value; }
        }

        private AccountSide _openBalanceSide;
        public AccountSide OpenBalanceSide
        {
            get { return _openBalanceSide; }
            set { _openBalanceSide = value; }
        }
    }
}
