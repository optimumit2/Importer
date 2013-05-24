using System;

namespace DAMAL.ZP.BOL.BankStatements.Import.MultiCash
{
    public class MultiCashMT940BankStatementPosition
    {
        public MT940BankStatementPosition BankStstementPosition;

        public MultiCashMT940BankStatementPosition()
        {
            BankStstementPosition = new MT940BankStatementPosition();
        }

        public void DecodeData(string s61a, string s61b, string s86)
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
            BankStstementPosition.Value = Convert.ToDecimal(s.Substring(0, s.IndexOf('N')));
            s = s.Substring(s.IndexOf('N') + 1);
            BankStstementPosition.TransCode = s.Substring(0, 3);
            s = s.Substring(3);
            BankStstementPosition.CustRef = s.Substring(0, s.IndexOf("//"));
            s = s.Substring(s.IndexOf("//") + 2);
            BankStstementPosition.BankRef = s;

            BankStstementPosition.GVCDescr = s61b;
            BankStstementPosition.GVCCode = s86.Substring(0, 3);
            string[] paramArr = s86.Substring(4).Split("<".ToCharArray());
            for (int i = 0; i < paramArr.Length; i++)
            {
                string code = paramArr[i].Substring(0, 2);
                string value = paramArr[i].Substring(2);
                switch (code)
                {
                    case "00":
                        BankStstementPosition.BookingText = value;
                        break;
                    case "10":
                        BankStstementPosition.RefNr = value;
                        break;
                    case "63":
                        BankStstementPosition.Ref = value;
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
                        BankStstementPosition.CustName2 = value;
                        break;
                    case "29":
                        BankStstementPosition.CustAddress1 = value;
                        break;
                    case "60":
                        BankStstementPosition.CustAddress2 = value;
                        break;
                    case "30":
                        BankStstementPosition.CustBank = value;
                        break;
                    case "31":
                        BankStstementPosition.CustAccount = value;
                        break;
                    case "32":
                        BankStstementPosition.CustBank = value;
                        break;
                    case "33":
                        BankStstementPosition.CustBank = BankStstementPosition.CustBank + value;
                        break;
                    case "38":
                        BankStstementPosition.CustAccount = value;
                        break;
                    case "62":
                        BankStstementPosition.CustAddress1 = value;
                        break;
                }
            }
        }
    }
}