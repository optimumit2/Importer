using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace DAMAL.ZP.BOL.BankStatements.Import.DeutscheBankXML
{
    public class DBXMLBankStatement : BankStatementParser
    {


        protected override void ReadHeader(StreamReader sr, ref string line)
        {
            throw new System.NotImplementedException();
        }

        protected override void ReadBlocks(StreamReader sr, ref string line)
        {
            throw new System.NotImplementedException();
        }


       public void ReadHeader(XmlTextReader sr)
        {

            for (int i = 0; i < sr.AttributeCount; i++)
            {
                sr.MoveToAttribute(i);
                switch (sr.Name)
                {
                    case "data":
                        m_BankStatement.Date = DateTimeHelper.ToDateTime(sr.Value, "-");
                        break;
                    case "rachunek":
                        m_BankStatement.BankAccount = sr.Value;
                        break;
                }

            }
            sr.MoveToElement(); // Moves the reader back to the element node.
        }



        public void ReadBlocks(XmlTextReader sr)
        {

                MT940BankStatementPosition bankStatementPosition = new MT940BankStatementPosition();
                while (sr.Read())
                {


                    if (sr.NodeType == XmlNodeType.EndElement && sr.Name == "OPERACJA" )
                    {
                        m_BankStatement.Positions.Add(bankStatementPosition);
                        break;
                    }
           

                    switch (sr.Name)
                    {
                        case "OPIS":
                            bankStatementPosition.BookingText = sr.ReadString();
                            break;
                        case "RACHUNEK":
                            bankStatementPosition.CustAccount = sr.ReadString();
                            break;
                        case "KWOTA":
                                bankStatementPosition.Value = Convert.ToDecimal(sr.ReadString().Replace(".",","));
                            break;
                        case "STRONA":
                            bankStatementPosition.DebitCredit = sr.ReadString();
                            break;
                        case "NAZWA1":
                            bankStatementPosition.CustName1 = sr.ReadString();
                            break;
                        case "NAZWA2":
                            bankStatementPosition.CustName2 = sr.ReadString();
                            break;
                        case "NAZWA3":
                            bankStatementPosition.CustAddress1 = sr.ReadString();
                            break;
                        case "NAZWA4":
                            bankStatementPosition.CustAddress2 = sr.ReadString();
                            break;
                        case "TRESC1":
                            bankStatementPosition.Descr1 = sr.ReadString();
                            break;
                        case "TRESC2":
                            bankStatementPosition.Descr1 += sr.ReadString();
                            break;
                        case "TRESC3":
                            bankStatementPosition.Descr1 += sr.ReadString();
                            break;
                        case "TRESC4":
                            bankStatementPosition.Descr1 += sr.ReadString();
                            break;
                    }
 
                }
                
            }
      


 
    }
}
