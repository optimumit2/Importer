using System;
using System.IO;
using FKIntegration.Documents;

namespace DAMAL.ZP.BOL.BankStatements.Import.CitiDirect_Swift
{
    public class CitiDirectMT940SwiftBankStatement : BankStatementParser
    {
        protected override void ReadHeader(StreamReader sr, ref string line)
        {
            if (line.Length < 5 || line.Substring(0, 4) != ":20:")
                throw new Exception("Oczekiwano :20:");
            m_BankStatement.Ref = line.Substring(4);

            if ((line = sr.ReadLine()) == null)
                throw new Exception("Napotkano koniec pliku");
            if (line.Length < 5 || line.Substring(0, 4) != ":25:")
                throw new Exception("Oczekiwano :25:");
            m_BankStatement.BankAccount = line.Substring(6);

            if ((line = sr.ReadLine()) == null)
                throw new Exception("Napotkano koniec pliku");
            if (line.Length < 6 || line.Substring(0, 4) != ":28:")
                throw new Exception("Oczekiwano :28:");
            m_BankStatement.StatementNr = line.Substring(4);

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
            try
            {
                if ((line = sr.ReadLine()) == null)
                    return;
                if (line.Length < 5 || line.Substring(0, 4) != ":61:")
                    return;

                while (line != null && line.Length >= 5 && line.Substring(0, 4) == ":61:")
                {
                    string s61a;
                    string s61b;
                    string s86 = string.Empty;

                    s61a = line.Substring(4);
                    if ((line = sr.ReadLine()) == null)
                        throw new Exception("Napotkano koniec pliku");
                    s61b = line;

                    //while((line = sr.ReadLine())!= null)
                    //{
                    //    if(line.Substring(4) == ":86:")
                    //        break;
                    //    s61a += line;
                    //}
                    if ((line = sr.ReadLine()) == null)
                        throw new Exception("Napotkano koniec pliku");

                    if (line.Length < 5 || line.Substring(0, 4) != ":86:")
                    {
                    } //    throw new Exception("Oczekiwano :86:");
                    else
                    {
                        s86 = line.Substring(4);

                        while ((line = sr.ReadLine()) != null)
                        {
                            if (line.Length <= 4)
                                s86 += line;
                            else if (line.Substring(0, 4) == ":61:" || line.Substring(0, 5) == ":62F:" ||
                                     line.Substring(0, 5) == ":62M:")
                                break;
                            s86 += line;

                        }
                    }
                    CitiDirectMT940SwiftBankStatementPosition bsPosition = new CitiDirectMT940SwiftBankStatementPosition();
                    bsPosition.DecodeData(s61a, s61b, s86);
                    m_BankStatement.Positions.Add(bsPosition.BankStstementPosition);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public override BankStatement GetBankStatement()
        {
            BankStatement bankStatement = m_BankStatement.GetBankStatement();
            bankStatement.Content = "Import z systemu CityDirect_Swift";
            //foreach (BankStatementPosition position in bankStatement.Positions)
            //{
            //    descryption = position.Descr;
            //    position.Customer.Name = FindDetailsInDesc(descryption, "/BN/");
            //    position.Customer.ShortName = position.Customer.Name;
            //    position.Customer.BankAccount = FindDetailsInDesc(descryption, "/BI/");
            //}
            return bankStatement;
        }

        private static string descryption;

        private static string FindDetailsInDesc(string descr, string ident)
        {
            string returnValue = string.Empty;

            if (descr.IndexOf(ident) != -1)
            {
                int startValue = descr.IndexOf(ident) + 4;
                returnValue = descr.Substring(startValue);
                int length = returnValue.IndexOf("/");
                if (length != -1)
                    returnValue = returnValue.Substring(0, length);
                returnValue.Trim();
                descryption = descryption.Remove(startValue - 4, length + 4);
            }
            return returnValue;
        }
    }
}