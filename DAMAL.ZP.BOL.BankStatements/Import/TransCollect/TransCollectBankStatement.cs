using System;
using System.IO;

namespace DAMAL.ZP.BOL.BankStatements.Import.TransCollect
{
    public class TransCollectBankStatement : BankStatementParser
    {
        public TransCollectBankStatement()
        {
            m_Content = "Import z systemu  TransCollect";
        }

        protected override void ReadHeader(StreamReader sr, ref string line)
        {
            if (line.Substring(0,2) != "01")
                throw new Exception("Błąd pliku");
            m_BankStatement.Date = DateTimeHelper.ToDateTimeLong(line.Substring(3, 8));
            line = line.Remove(0,line.IndexOf("\"")+1);
            m_BankStatement.BankAccount = line.Substring(0,line.IndexOf("\"")).Replace(" ","");

        }

        protected override void ReadBlocks(StreamReader sr, ref string line)
        {
            line = sr.ReadLine();
            while (line != null)
            {
                string s61a = line;
                string s86 = "";

                TransCollectBankStatementPosition bsPosition = new TransCollectBankStatementPosition();
                bsPosition.DecodeData(s61a, s86);
                m_BankStatement.Positions.Add(bsPosition.BankStstementPosition);

                line = sr.ReadLine();
            }
        }
    }
}
