using System;

namespace DAMAL.ZP.BOL.BankStatements
{
    /// <summary>
    /// Klasa reprezentująca pojedyńczą pozycje wyciągu bankowego.
    /// </summary>
    public class MT940BankStatementPosition
    {
        private string m_BankRef;
        private string m_BookingText;
        private string m_Currency3;
        private string m_CustAccount;
        private string m_CustAddress1;
        private string m_CustAddress2;
        private string m_CustBank;
        private string m_CustName1;
        private string m_CustName2;
        private string m_CustRef;
        private DateTime m_Date;

        private string m_DebitCredit;
        private string m_Descr1;
        private string m_Descr2;
        private string m_Descr3;
        private string m_Descr4;
        private string m_Descr5;
        private string m_Descr6;
        private string m_Descr7;
        private string m_GVCCode;
        private string m_GVCDescr;
        private string m_Ref;
        private string m_RefNr;
        private string m_TransCode;
        private decimal m_Value;

        public DateTime Date
        {
            get { return m_Date; }
            set { m_Date = value; }
        }

        public string DebitCredit
        {
            get { return m_DebitCredit; }
            set { m_DebitCredit = value; }
        }

        public string Currency3
        {
            get { return m_Currency3; }
            set { m_Currency3 = value; }
        }

        public decimal Value
        {
            get { return m_Value; }
            set { m_Value = value; }
        }

        public string TransCode
        {
            get { return m_TransCode; }
            set { m_TransCode = value; }
        }

        public string CustRef
        {
            get { return m_CustRef; }
            set { m_CustRef = value; }
        }

        public string BankRef
        {
            get { return m_BankRef; }
            set { m_BankRef = value; }
        }

        public string GVCCode
        {
            get { return m_GVCCode; }
            set { m_GVCCode = value; }
        }

        public string GVCDescr
        {
            get { return m_GVCDescr; }
            set { m_GVCDescr = value; }
        }

        public string BookingText
        {
            get { return m_BookingText; }
            set { m_BookingText = value; }
        }

        public string RefNr
        {
            get { return m_RefNr; }
            set { m_RefNr = value; }
        }

        public string Descr1
        {
            get { return m_Descr1; }
            set { m_Descr1 = value; }
        }

        public string Descr2
        {
            get { return m_Descr2; }
            set { m_Descr2 = value; }
        }

        public string Descr3
        {
            get { return m_Descr3; }
            set { m_Descr3 = value; }
        }

        public string Descr4
        {
            get { return m_Descr4; }
            set { m_Descr4 = value; }
        }

        public string Descr5
        {
            get { return m_Descr5; }
            set { m_Descr5 = value; }
        }

        public string Descr6
        {
            get { return m_Descr6; }
            set { m_Descr6 = value; }
        }

        public string Descr7
        {
            get { return m_Descr7; }
            set { m_Descr7 = value; }
        }

        public string CustName1
        {
            get { return m_CustName1; }
            set { m_CustName1 = value; }
        }

        public string CustName2
        {
            get { return m_CustName2; }
            set { m_CustName2 = value; }
        }

        public string CustAddress1
        {
            get { return m_CustAddress1; }
            set { m_CustAddress1 = value; }
        }

        public string CustAddress2
        {
            get { return m_CustAddress2; }
            set { m_CustAddress2 = value; }
        }

        public string CustBank
        {
            get { return m_CustBank; }
            set { m_CustBank = value; }
        }

        public string CustAccount
        {
            get { return m_CustAccount; }
            set { m_CustAccount = value; }
        }

        public string Ref
        {
            get { return m_Ref; }
            set { m_Ref = value; }
        }

        public bool HasVirtualId;
        private int m_VirtualId;

        public int VirtualId
        {
            set { m_VirtualId = value; }
            get { return m_VirtualId; }
        }


        public BankStatementPosition GetBankStatementPosition()
        {
            BankStatementPosition position = new BankStatementPosition();
            position.Descr = Descr1 + " ";
            position.Descr += Descr2 + " ";
            position.Descr += Descr3 + " ";
            position.Descr += Descr4 + " ";
            position.Descr += Descr5 + " ";
            position.Descr += Descr6 + " ";
            position.Descr += Descr7 + " ";
            position.Descr = position.Descr.Trim();

            position.OrginalDesc = position.Descr;

            position.Customer.Name = CustName1;
            position.Customer.ShortName = CustName1;
            position.Customer.City = CustAddress2;
            position.Customer.Address = CustAddress1;
            position.Customer.BankAccount = CustAccount;

            position.SideWN.Value = Value;
            position.SideMA.Value = Value;
            position.DebitCredit = new DebitCredit(DebitCredit);

            if (!string.IsNullOrEmpty(BookingText))
                position.OperationType.Type = BookingText;

            if (!string.IsNullOrEmpty(GVCCode))
                position.OperationType.Number = GVCCode;


            position.VirtualId = VirtualId;
            
            return position;
        }
    }
}