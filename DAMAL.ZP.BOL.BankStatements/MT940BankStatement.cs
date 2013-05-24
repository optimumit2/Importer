using System;
using System.Collections.Generic;
using FKIntegration.Documents;

namespace DAMAL.ZP.BOL.BankStatements
{
    /// <summary>
    /// Klasa reprezentująca wyciąg bankowy
    /// </summary>
    public class MT940BankStatement
    {
        private string m_BankAccount;
        private string m_Currency;
        private DateTime m_Date;
        private string m_FileName;

        private string m_Ref;
        private string m_SaldoType;
        private string m_StatementNr;
        private decimal m_Value;

        public string FileName
        {
            get { return m_FileName; }
            set { m_FileName = value; }
        }

        public string Ref
        {
            get { return m_Ref; }
            set { m_Ref = value; }
        }

        public string BankAccount
        {
            get { return m_BankAccount; }
            set { m_BankAccount = value; }
        }

        public string StatementNr
        {
            get { return m_StatementNr; }
            set { m_StatementNr = value; }
        }

        public string SaldoType
        {
            get { return m_SaldoType; }
            set { m_SaldoType = value; }
        }

        public DateTime Date
        {
            get { return m_Date; }
            set { m_Date = value; }
        }

        public string Currency
        {
            get { return m_Currency; }
            set { m_Currency = value; }
        }

        public decimal Value
        {
            get { return m_Value; }
            set { m_Value = value; }
        }

        readonly List<MT940BankStatementPosition> m_Positions = new List<MT940BankStatementPosition>();
        public List<MT940BankStatementPosition> Positions
        {
            get { return m_Positions; }
        }

        public BankStatement GetBankStatement()
        {
            BankStatement bs = new BankStatement("BS");
            bs.FileName = FileName;
            bs.DocumentDate = Date;            
            bs.BankAccount = BankAccount;
            bs.Number = StatementNr;
            //bs.NumberBS = StatementNr;

            foreach (MT940BankStatementPosition posMT940 in Positions)
            {                
                
                bs.Positions.Add(posMT940.GetBankStatementPosition());
            }
            return bs;
        }

        public bool IsNotEmpty()
        {
            return m_Positions.Count > 0;
        }
    }
}