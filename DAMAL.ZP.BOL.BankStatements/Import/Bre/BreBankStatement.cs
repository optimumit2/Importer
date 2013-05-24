using System;
using System.Collections.Generic;
using System.IO;
using FKIntegration.Documents;

namespace DAMAL.ZP.BOL.BankStatements.Import.Bre
{
    public class BreBankStatement : BankStatementParser
    {
        private string m_BankAccount; // numer rachunku 
        private string m_Currency; // waluta
        private DateTime m_Date; // data 
        private string m_FileName; // nazwa pliku
        private List<BreBankStatementPosition> m_Positions;
        private string m_Ref; // opis - Bre bank
        private string m_SaldoType; // typ salda (C or D)
        private string m_StatementNr; // numer wyciągu  
        private decimal m_Value;

        public BreBankStatement()
        {
            m_Positions = new List<BreBankStatementPosition>();
        }

        public string FileName
        {
            get { return m_FileName; }
            set { m_FileName = value; }
        }

        protected override void ReadHeader(StreamReader sr, ref string line)
        {
            if (line.Length < 5 || line.Substring(0, 4) != ":20:")
                throw new Exception("Oczekiwano :20:");
            m_Ref = line.Substring(4, 2);

            if ((line = sr.ReadLine()) == null)
                throw new Exception("Napotkano koniec pliku");
            if (line.Length < 5 || line.Substring(0, 4) != ":25:")
                throw new Exception("Oczekiwano :25:");
            m_BankAccount = line.Substring(4);

            if ((line = sr.ReadLine()) == null)
                throw new Exception("Napotkano koniec pliku");
            if (line.Length < 6 || line.Substring(0, 5) != ":28C:")
                throw new Exception("Oczekiwano :28C:");
            m_StatementNr = line.Substring(5);

            if ((line = sr.ReadLine()) == null)
                throw new Exception("Napotkano koniec pliku");
            if (line.Length < 10 || line.Substring(0, 5) != ":60F:")
                throw new Exception("Oczekiwano :60F:");
            m_SaldoType = line.Substring(5, 1);
            m_Date = DateTimeHelper.ToDateTimeShort(line.Substring(6, 6));
            m_Currency = line.Substring(12, 3);
            //m_Value = Convert.ToDecimal(line.Substring(15));
            //DL dla Societe Generale
            m_Value = Convert.ToDecimal(line.Substring(17));
        }

        protected override void ReadBlocks(StreamReader sr, ref string line)
        {
            if ((line = sr.ReadLine()) == null)
                return;
            //if (line.Length < 5 || line.Substring(0, 4) != ":61:")
            if (line.Substring(0, 4) != ":61:")
                return;
            while (line != null && line.Length >= 5 && line.Substring(0, 4) == ":61:")
            {
                string s61a = "";
                string s61b = "";
                string s86 = "";

                s61a = line.Substring(4);
                //if ((line = sr.ReadLine()) == null)
                 //   throw new Exception("Napotkano koniec pliku");
                //s61b = line;
                //DL dla Societe Generale

                if ((line = sr.ReadLine()) == null)
                    throw new Exception("Napotkano koniec pliku");

                if (line.Length < 5 || line.Substring(0, 4) != ":86:")
                    throw new Exception("Oczekiwano :86:");
                s86 = line.Substring(4);

                while ((line = sr.ReadLine()) != null)
                {
                    //if (line.Length < 2 || line.Substring(0, 2) == ":6")// || line.Substring(0, 5) == ":62F:")
                    if (line.Substring(0, 1) == "-") // || line.Substring(0, 5) == ":62F:")
                        break;
                    if (line.Length > 1) 
                        if (line.Substring(0, 2) == ":6")
                            break;
                       
                    s86 += line;
                }
                BreBankStatementPosition bsPosition = new BreBankStatementPosition();
                bsPosition.DecodeData(s61a, s61b, s86);
                m_Positions.Add(bsPosition);
            }
        }

        public override BankStatement GetBankStatement()
        {
            BankStatement bs = new BankStatement("WB");
            bs.FileName = FileName;
            bs.Content = "Import z systemu Bre BreBank";
            bs.DocumentDate = m_Date;
            bs.BankAccount = m_BankAccount;

            foreach (BreBankStatementPosition posBre in m_Positions)
            {
                BankStatementPosition pos = new BankStatementPosition();
                pos.DebitCredit = new DebitCredit(posBre.DebitCredit);
                pos.SideWN.Value = posBre.Value;
                pos.SideMA.Value = posBre.Value;
                pos.Descr = posBre.Descr; //sprawdzic to 
                pos.OperationType.Number = posBre.TransCode;
                pos.OperationType.Type = posBre.CustRef;
                pos.OrginalDesc = posBre.BankRef;
                bs.Positions.Add(pos);
            }
            return bs;
        }

        override protected bool IsNotEmpty()
        {
            return m_Positions.Count > 0;
        }

        //public override void SetImportFileName(ImportFile importFile)
        //{
        //    m_BankStatement.FileName = importFile.GetFileName();
        //}

    }
}