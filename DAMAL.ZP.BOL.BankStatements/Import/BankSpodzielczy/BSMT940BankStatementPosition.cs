using System;

namespace DAMAL.ZP.BOL.BankStatements.Import.BankSpodzielczy
{
    public class BSMT940BankStatementPosition
    {
        public MT940BankStatementPosition BankStstementPosition;

        public BSMT940BankStatementPosition()
        {
            BankStstementPosition = new MT940BankStatementPosition();
        }

        public void DecodeData(string s61a, string s86)
        {
            BankStstementPosition.Date = DateTimeHelper.ToDateTimeShort(s61a.Substring(0, 6));
            string s = s61a.Substring(10);
            if (s[0] == 'R')
            {
                BankStstementPosition.DebitCredit = s.Substring(0, 2);
                s = s.Substring(2);
            }
            else
            {
                BankStstementPosition.DebitCredit = s.Substring(0, 1);
                s = s.Substring(1);
            }
            BankStstementPosition.Currency3 = s.Substring(0, 1);
            s = s.Substring(1);
            BankStstementPosition.Value = Convert.ToDecimal(s.Substring(0, s.IndexOf(',') + 3));
            s = s.Substring(s.IndexOf(',') + 3);

            if (s.IndexOf("//") >= 0)
            {
                BankStstementPosition.CustRef = s.Substring(0, s.IndexOf("//"));
                s = s.Substring(s.IndexOf("//") + 2);
                BankStstementPosition.BankRef = s;
            }
            else
            {
                BankStstementPosition.CustRef = "";
                BankStstementPosition.BankRef = "";
            }

            string[] paramArr = s86.Split(">".ToCharArray());
            for (int i = 1; i < paramArr.Length; i++)
            {
                string code = paramArr[i].Substring(0, 2);
                string value = paramArr[i].Substring(2);
                switch (code)
                {
                    case "00":
                        BankStstementPosition.GVCCode = value;
                        break;
                    case "10":
                        BankStstementPosition.RefNr = value;
                        break;
                    case "20":
                        BankStstementPosition.Descr1 = value;
                        break;
                    case "21":
                        BankStstementPosition.Descr2 = value;
                        break;
                    case "22":
                        BankStstementPosition.Descr3 = value;
                        break;
                    case "23":
                        BankStstementPosition.Descr4 = value;
                        break;
                    case "24":
                        BankStstementPosition.Descr5 = value;
                        break;
                    case "25":
                        BankStstementPosition.Descr6 = value;
                        break;
                    case "26":
                        BankStstementPosition.Descr7 = value;
                        break;
                    case "27":
                        BankStstementPosition.CustName1 = value;
                        break;
                    case "28":
                        BankStstementPosition.CustName1 += value;
                        break;
                    case "29":
                        BankStstementPosition.CustAddress1 = value;
                        break;
                    case "33":
                        BankStstementPosition.CustAddress1 = value;
                        break;
                    case "60":
                        BankStstementPosition.CustAddress2 = value;
                        break;
                    case "30":
                        BankStstementPosition.CustBank = value;
                        break;
                    case "38":
                        BankStstementPosition.CustAccount = value;
                        break;
                }
            }
        }
    }
}