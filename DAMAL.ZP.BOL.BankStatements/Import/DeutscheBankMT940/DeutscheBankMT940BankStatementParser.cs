using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DAMAL.ZP.BOL.BankStatements.Import.DeutscheBankMT940
{
    public class DeutscheBankMT940BankStatementParser : BankStatementParser
    {

        public DeutscheBankMT940BankStatementParser()
        {
            m_Content = "Import z DeutcheBank MT940";
        }

        protected override void ReadHeader(StreamReader sr, ref string line)
        {

            if (line.Length < 5 || line.Substring(0, 4) != ":20:")
                throw new Exception("Oczekiwano :20:");
            m_BankStatement.Date = DateTimeHelper.ToDateTimeShort(line.Substring(4, 6));

            if ((line = sr.ReadLine()) == null)
                throw new Exception("Napotkano koniec pliku");
            if (line.Length < 5 || line.Substring(0, 4) != ":25:")
                throw new Exception("Oczekiwano :25:");
            m_BankStatement.BankAccount = line.Substring(4);

            if ((line = sr.ReadLine()) == null)
                throw new Exception("Napotkano koniec pliku");
            if (line.Length < 5 || line.Substring(0, 5) != ":28C:")
                throw new Exception("Oczekiwano :28C:");
            m_BankStatement.StatementNr = line.Substring(5);

            if ((line = sr.ReadLine()) == null)
                throw new Exception("Napotkano koniec pliku");
            if (line.Length < 5 || line.Substring(0, 5) != ":60F:")
                throw new Exception("Oczekiwano :60F:");

           

         }

        protected override void ReadBlocks(StreamReader sr, ref string line)
        {
            if ((line = sr.ReadLine()) == null)
                return;
            if (line.Length < 5 || line.Substring(0, 4) != ":61:")
                return;

            while (line != null && line.Length >= 5 && line.Substring(0, 4) == ":61:")
            {
                string s61a = string.Empty;
                string s86 = string.Empty;
                string s61b = string.Empty;
                s61a = line.Substring(4);
                if ((line = sr.ReadLine()) == null)
                    throw new Exception("Napotkano koniec pliku");
                s61b = line;
                while (line.Length < 5 || line.Substring(0, 4) != ":86:")
                   line = sr.ReadLine();

                if (line.Length < 5 || line.Substring(0, 4) != ":86:")
                    throw new Exception("Oczekiwano :86:");
                s86 = line.Substring(4);

                while ((line = sr.ReadLine()) != null)
                {
                    if (line.Length < 2 || line.Substring(0, 1) != "<")
                        break;
                    s86 += line;
                }

                DeutscheBankBankStatementPosition bsPosition = new DeutscheBankBankStatementPosition();
                bsPosition.DecodeData(s61a,s61b, s86);
                m_BankStatement.Positions.Add(bsPosition.BankStstementPosition);
            }
        }
    }
}

