using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
namespace DAMAL.ZP.BOL.BankStatements.Import.PKOXML
{
    public class PKOXMLBankStatement: BankStatementParser
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
            while (sr.Read())
            {
                if (sr.Name == "account")
                {
                    m_BankStatement.BankAccount = sr.ReadString();
                    m_BankStatement.BankAccount = m_BankStatement.BankAccount.Replace(" ", "");
                }

                if (sr.Name == "date")
                {
                    sr.MoveToAttribute(0);    
                    m_BankStatement.Date = DateTimeHelper.ToDateTime(sr.Value, "-");

                    break;
                }
                            }
            
        }



        public void ReadBlocks(XmlTextReader sr)
        {

                MT940BankStatementPosition bankStatementPosition = new MT940BankStatementPosition();
                while (sr.Read())
                {


                    if ( sr.Name == "operation" )
                    {
                        m_BankStatement.Positions.Add(bankStatementPosition);
                        break;
                    }
           

                    switch (sr.Name)
                    {
                        case "type":
                            bankStatementPosition.BookingText = sr.ReadString();
                            break;
                        case "description":
                            
                            bankStatementPosition.Descr1 = sr.ReadString();
                            PKOxmlBankStatementPosition pkoConvert = new PKOxmlBankStatementPosition(bankStatementPosition.Descr1);
                            bankStatementPosition.CustAccount = pkoConvert.PKOAccount;
                            bankStatementPosition.Descr1 = pkoConvert.PKOTextDesc;
                            bankStatementPosition.CustName1 = pkoConvert.PKOxmlContractor;
                            break;
                        
                        case "amount":
                                
                                bankStatementPosition.Value = Convert.ToDecimal(sr.ReadElementString().Replace(".",","));
                            
                                bankStatementPosition.DebitCredit = "C";
                                if (bankStatementPosition.Value < 0)
                                {
                                    bankStatementPosition.DebitCredit = "D";
                                    bankStatementPosition.Value *=-1;
                                }
                               
                                break;
                                            }
 
                }
                
            }
      


 
    }
    }

