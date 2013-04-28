using System;
using System.Collections.Generic;
using MXDokFK;

namespace FKIntegration.Documents
{
    public abstract class Document
    {
        protected Document(string shortCut)
        {
            _shortCut = shortCut;            
        }

        private int _id;
        private readonly DateTime _entryDate = DateTime.Now;
        protected string _number;
        protected string _shortCut;
        protected string _content;
        protected DocumentMark _documentMark;
        protected DateTime _documentDate;
        protected int _positionNumber = 1;
        protected readonly List<Position> _positionList = new List<Position>();

        internal int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        //public static int DefinitionTypeId { get; set; }

        public DateTime EntryDate
        {
            get { return _entryDate; }
        }

        public string Number
        {
            get { return _number; }
            set { _number = value; }
        }

        public string ShortCut
        {
            get { return _shortCut; }
            //set { _shortCut = value; }
        }

        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }

        public DocumentMark DocumentMark
        {
            get { return _documentMark; }
            set { _documentMark = value; }
        }

        public DateTime DocumentDate
        {
            get { return _documentDate; }
            set { _documentDate = value; }
        }

        public virtual List<Position> PositionList
        {
            get { return _positionList; }
        }

        protected virtual void AddPosition(Position position)
        {
            _positionList.Add(position);
            _positionNumber++;
        }

        protected virtual Position AddDebitPositionBase()
        {
            return AddDebitPositionBase(_content);
        }

        protected virtual Position AddDebitPositionBase(string description)
        {
            return AddDebitPositionBase(description, CurrencyEnum.PLN, 0, null, description, 0, null);
        }

        protected virtual Position AddDebitPositionBase(decimal amount, string debitaccount, string creditaccount)
        {
            return AddDebitPositionBase(Content, amount, debitaccount, creditaccount);
        }

        protected Position AddCreditEntryPositionBase(string description, CurrencyEnum currency, decimal debitAmount, string debitAccount, decimal creditAmount, string creditAccount)
        {
            throw new NotImplementedException();
        }

        protected virtual Position AddDebitPositionBase(string description, decimal amount, string debitAccount, string creditAccount)
        {
            return AddDebitPositionBase(description, CurrencyEnum.PLN, amount, debitAccount, creditAccount);
        }

        protected virtual Position AddDebitPositionBase(string description, CurrencyEnum currency, decimal amount, string debitAccount, string creditAccount)
        {
            return AddDebitPositionBase(description, currency, amount, debitAccount, amount, creditAccount);
        }

        protected virtual Position AddDebitPositionBase(string description, decimal debitAmount, string debitAccount, decimal creditAmount, string creditAccount)
        {
            return AddDebitPositionBase(description, CurrencyEnum.PLN, debitAmount, debitAccount, creditAmount, creditAccount);
        }

        protected virtual Position AddDebitPositionBase(string description, CurrencyEnum currency, decimal debitAmount, string debitAccount, decimal creditAmount, string creditAccount)
        {
            return AddDebitPositionBase(description, currency, debitAmount, debitAccount, description, creditAmount,
                                    creditAccount);
        }

        protected virtual Position AddDebitPositionBase(string debitDescription, CurrencyEnum currency, decimal debitAmount, string debitAccount, string creditDescription, decimal creditAmount, string creditAccount)
        {
            var position = new DebitEntryPosition(this, _positionNumber)
                               {
                                   DebitDescription = debitDescription,
                                   Currency = currency,
                                   DebitAmount = debitAmount,
                                   DebitAccount = debitAccount,
                                   CreditDescription = creditDescription,
                                   CreditAccount = creditAccount,
                                   CreditAmount = creditAmount
                               };
            AddPosition(position);
            return position;
        }

        public virtual void Save()
        {
            var pDokumentClass = new PDokumentClass();
            FKPDatabase fkpDatabase = FKIntegrationManager.GetFKPDatabase();
            fkpDatabase.Open();
            BtDatabase database = fkpDatabase.BtDatabase;
            //pDokumentClass.Open(string.Format("{0};BUFOR", database.FirmaInfo.OstatniRok), database);
            pDokumentClass.Open("", database);

            FillDocumentBody(pDokumentClass);

            FillOtherShitStuffOfDocument(pDokumentClass);

            FillPositions(pDokumentClass);

            pDokumentClass.Insert();
            _id = (int)pDokumentClass["id"];
        }

        protected virtual void FillPositions(PDokumentClass pDokumentClass)
        {
            foreach (var eachPosition in PositionList)
            {
                eachPosition.AddPosition(pDokumentClass);
            }
        }

        protected virtual void FillDocumentBody(PDokumentClass pDokumentClass)
        {
            pDokumentClass["datadok"] = DocumentDate;
            pDokumentClass["skrot"] = ShortCut;
            pDokumentClass["tresc"] = Content;
            //pDokumentClass[""] = this.DocumentMark;
            pDokumentClass["nazwa"] = Number;
            pDokumentClass["datawpr"] = EntryDate;
        }

        protected virtual void FillOtherShitStuffOfDocument(PDokumentClass pDokumentClass)
        {

        }
    }
}