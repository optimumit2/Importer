using System;
using System.IO;

namespace DAMAL.ZP.BOL.BankStatements.Import.Citi
{
    public class CitiBankStatement : BankStatementParser
    {
        public CitiBankStatement()
        {
            m_Content = "Import z systemu IWB";
        }
        protected override void ReadHeader(StreamReader sr, ref string line)
        {
            if (line.Length < 20)
                throw new Exception("Błąd pliku");
            m_BankStatement.Date = DateTimeHelper.ToDateTime(line.Substring(15, 8), string.Empty);
        }

        protected static void ReadBlocks(StreamReader sr, ref string[] line)
        {
            for (int i = 0; i < line.Length - 1; i++)
            {
                string s61a = line[i];
                string s86 = "";

                CitiBankStatementPosition bsPosition = new CitiBankStatementPosition();
                bsPosition.DecodeData(s61a, s86);
            }
        }

        protected override void ReadBlocks(StreamReader sr, ref string line)
        {
            throw new Exception("Ta metoda nie powinna być wywoływana w wyciągu citi bank");
        }

        public override bool ReadStream(StreamReader sr, ref string line)
        {
            string[] lineTab = line.Split('@');
            ReadHeader(sr, ref lineTab[0]);
            ReadBlocks(sr, ref lineTab);
            return m_BankStatement.IsNotEmpty();
        }
    }
}