﻿using System;
using System.IO;

namespace DAMAL.ZP.BOL.BankStatements.Import.BPH
{
    public class BPHBankStatement : BankStatementParser
    {
        public BPHBankStatement()
        {
            m_Content = "Import Wyciągów Bankowych";
        }

        protected override void ReadHeader(StreamReader sr, ref string line)
        {
            if (line.Length < 5 || line.Substring(0, 4) != ":20:")
                throw new Exception("Oczekiwano :20:");

            if ((line = sr.ReadLine()) == null)
                throw new Exception("Napotkano koniec pliku");
            if (line.Length < 5 || line.Substring(0, 4) != ":25:")
                throw new Exception("Oczekiwano :25:");
            m_BankStatement.BankAccount = line.Substring(4);

            if ((line = sr.ReadLine()) == null)
                throw new Exception("Napotkano koniec pliku");
            if (line.Length < 6 || line.Substring(0, 5) != ":28C:")
                throw new Exception("Oczekiwano :28C:");
            m_BankStatement.StatementNr = line.Substring(5);


            if ((line = sr.ReadLine()) == null)
                throw new Exception("Napotkano koniec pliku");
            if (line.Length < 5 || line.Substring(0, 4) != ":NS:")
                throw new Exception("Oczekiwano :NS:");


            if ((line = sr.ReadLine()) == null)
                throw new Exception("Napotkano koniec pliku");
            if (line.Length < 10 || line.Substring(0, 5) != ":60F:")
                throw new Exception("Oczekiwano :60F:");
            m_BankStatement.SaldoType = line.Substring(5, 1);
            m_BankStatement.Date = DateTimeHelper.ToDateTimeShort(line.Substring(6, 6));
            m_BankStatement.Currency = line.Substring(12, 3);
            m_BankStatement.Value = Convert.ToDecimal(line.Substring(15));
        }

        protected override void ReadBlocks(StreamReader sr, ref string line)
        {
            if ((line = sr.ReadLine()) == null)
                return;
            if (line.Length < 5 || line.Substring(0, 4) != ":61:")
                return;

            while (line != null && line.Length >= 5 && line.Substring(0, 4) == ":61:")
            {
                string s61a = "";
                string s86 = "";

                s61a = line.Substring(4);
                if ((line = sr.ReadLine()) == null)
                    throw new Exception("Napotkano koniec pliku");

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

                BPHBankStatementPosition bsPosition = new BPHBankStatementPosition();
                bsPosition.DecodeData(s61a, s86);
                m_BankStatement.Positions.Add(bsPosition.BankStstementPosition);
            }
        }
    }
}