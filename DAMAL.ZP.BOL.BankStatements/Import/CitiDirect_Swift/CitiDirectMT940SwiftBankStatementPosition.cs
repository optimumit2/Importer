using System;

namespace DAMAL.ZP.BOL.BankStatements.Import.CitiDirect_Swift
{
    public class CitiDirectMT940SwiftBankStatementPosition
    {
        public MT940BankStatementPosition BankStstementPosition;

        public CitiDirectMT940SwiftBankStatementPosition()
        {
            BankStstementPosition = new MT940BankStatementPosition();
        }

        public void DecodeData(string s61a, string s61b, string s86)
        {
            try
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

                BankStstementPosition.BookingText = s61b;
                BankStstementPosition.GVCCode = FindTransCode(s61b);// Substring(0, 3);
                BankStstementPosition.Descr1 = FindBankStatementPositionDescryption(s86);
                BankStstementPosition.CustName1 = FindCustomerName(s86);
                BankStstementPosition.CustAccount = FindCustomerAccount(s86);


                //string[] paramArr = s86.Substring(4).Split("/".ToCharArray());
                //for (int i = 0; i < paramArr.Length; i++)
                //{
                //    string code = paramArr[i].Substring(0, 2);
                //    string value = paramArr[i].Substring(2);
                //    switch (code)
                //    {
                //        case "00":
                //            BankStstementPosition.BookingText = value;
                //            break;
                //        case "20":
                //           BankStstementPosition.Descr1 = value;
                //            break;
                //        case "21":
                //            BankStstementPosition.Descr2 = value;
                //            break;
                //        case "22":
                //            BankStstementPosition.Descr3 = value;
                //            break;
                //        case "23":
                //            BankStstementPosition.Descr4 = value;
                //            break;
                //        case "24":
                //            BankStstementPosition.Descr5 = value;
                //            break;
                //        case "25":
                //            BankStstementPosition.Descr6 = value;
                //            break;
                //        case "26":
                //            BankStstementPosition.Descr7 = value;
                //            break;
                //        case "27":
                //            BankStstementPosition.Descr7 = BankStstementPosition.Descr7 + value;
                //            break;
                //        case "32":
                //            BankStstementPosition.CustName1 = value;
                //            break;
                //        case "33":
                //            BankStstementPosition.CustAddress1 = value;
                //            break;
                //        case "30":
                //            BankStstementPosition.CustBank = value;
                //            break;
                //        case "38":
                //            BankStstementPosition.CustAccount = value;
                //            break;
                //    }
                //}
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private string FindTransCode(string s61a)
        {
            if ((s61a.IndexOf("/CTC/") != -1))
            {
                s61a = s61a.Substring(s61a.IndexOf("/CTC/") + 5);
                return s61a.Substring(0, (s61a.IndexOf("/")));
            }
            if ((s61a.IndexOf("/BAI/") != -1))
            {
                s61a = s61a.Substring(s61a.IndexOf("/BAI/") + 5);
                return s61a.Substring(0, (s61a.IndexOf("/")));
            }
            return string.Empty;
        }

        private string FindCustomerAccount(string s86)
        {
            if ((s86.IndexOf("/BI/") != -1))
            {
                s86 = s86.Substring(s86.IndexOf("/BI/") + 4);
                if ((s86.IndexOf("/") != -1))
                    return s86.Substring(0, (s86.IndexOf("/")));
                return s86;
            }

            //string[] paramArr = s86.Substring(4).Split(" ".ToCharArray());
            //for (int i = 0; i < paramArr.Length; i++)
            //{
            if (s86.Length < 26)
                return string.Empty;

            string paramArr = s86.Substring(s86.Length - 26);
            // if (paramArr[.Length == 26)
            // {
            if (CheckThisIsBankAccount(paramArr))
                return paramArr;
            // }
            //}
            return string.Empty;
        }

        private bool CheckThisIsBankAccount(string bankAccount)
        {
            for (int i = 0; i < bankAccount.Length; i++)
            {
                if ((bankAccount[i] < 48) | (bankAccount[i] > 57))
                    return false;
            }
            ValidatorIBAN validIBAN = new ValidatorIBAN(bankAccount);
            return validIBAN.CheckIBAN();

        }

        private string FindCustomerName(string s86)
        {
            //if (s86.IndexOf("/OB/") != -1)
            //    return s86.Substring(s86.IndexOf("/OB/") + 5, (s86.IndexOf("/OB3/") - (s86.IndexOf("/OB/") + 5)));

            if ((s86.IndexOf("/BN/") != -1))
            {
                s86 = s86.Substring(s86.IndexOf("/BN/") + 4);
                return s86.Substring(0, (s86.IndexOf("/")));
            }
            if ((s86.IndexOf("/OB/") != -1))
            {
                s86 = s86.Substring(s86.IndexOf("/OB/") + 4);
                return s86.Substring(0, (s86.IndexOf("/")));
            }
            return string.Empty;
        }

        private string FindBankStatementPositionDescryption(string s86)
        {
            if (s86.IndexOf("/OB/") != -1)
                return s86.Substring(s86.IndexOf("/PY/") + 4, (s86.IndexOf("/OB/") - (s86.IndexOf("/PY/") + 4)));
            if (s86.IndexOf("/BI/") != -1)
                return s86.Substring(s86.IndexOf("/PY/") + 4, (s86.IndexOf("/BI/") - (s86.IndexOf("/PY/") + 4)));
            return s86;
        }
    }
}