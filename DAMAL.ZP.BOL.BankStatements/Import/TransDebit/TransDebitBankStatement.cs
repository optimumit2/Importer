using System;
using System.IO;

namespace DAMAL.ZP.BOL.BankStatements.Import.TransDebit
{
    public class TransDebitBankStatement : BankStatementParser
    {
        public TransDebitBankStatement()
        {
            m_Content = "Import z systemu  TransDebit";
        }

        protected override void ReadHeader(StreamReader sr, ref string line)
        {
            if (line.Substring(0, 2) != "01")
                throw new Exception("Błąd pliku");
            m_BankStatement.Date = DateTimeHelper.ToDateTimeLong(line.Substring(3, 8));
            line = line.Remove(0, line.IndexOf("\"") + 1);
            m_BankStatement.BankAccount = line.Substring(0, line.IndexOf("\"")).Replace(" ", "");

        }

        protected override void ReadBlocks(StreamReader sr, ref string line)
        {
            line = sr.ReadLine();
            while (line != null)
            {
                string s61a = line;
                string s86 = "";
                s61a =  s61a.Remove(0, s61a.IndexOf(",") + 1);
                if (s61a.Substring(1, 2) == "DD")
                {
                    TransDebitBankStatementPosition bsPosition = new TransDebitBankStatementPosition();
                    bsPosition.DecodeData(s61a, s86);
                   
                    m_BankStatement.Positions.Add(bsPosition.BankStstementPosition);
                }
                line = sr.ReadLine();
            }
        }
    }
}