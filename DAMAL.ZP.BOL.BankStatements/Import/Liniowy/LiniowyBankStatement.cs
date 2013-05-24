using System;
using System.IO;

namespace DAMAL.ZP.BOL.BankStatements.Import.Liniowy
{
    public class LiniowyBankStatement : BankStatementParser
    {
        public LiniowyBankStatement()
        {
            m_Content = "Import z systemu IWB";
        }

        protected override void ReadHeader(StreamReader sr, ref string line)
        {
            if (line.Length < 20)
                throw new Exception("Błąd pliku");
            m_BankStatement.Date = DateTimeHelper.ToDateTime(line.Substring(10, 10), "-");
        }

        protected override void ReadBlocks(StreamReader sr, ref string line)
        {
            while (line != null)
            {
                string s61a = line;
                string s86 = "";

                LiniowyBankStatementPosition bsPosition = new LiniowyBankStatementPosition();
                bsPosition.DecodeData(s61a, s86);
                m_BankStatement.Positions.Add(bsPosition.BankStstementPosition);

                line = sr.ReadLine();
            }
        }
    }
}
