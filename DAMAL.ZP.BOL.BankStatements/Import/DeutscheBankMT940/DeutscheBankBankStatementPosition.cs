using System;
using System.Text.RegularExpressions;

namespace DAMAL.ZP.BOL.BankStatements.Import.DeutscheBankMT940
{
    public class DeutscheBankBankStatementPosition
    {
        public MT940BankStatementPosition BankStstementPosition;

        public DeutscheBankBankStatementPosition()
        {
            BankStstementPosition = new MT940BankStatementPosition();
        }

        public void DecodeData(string s61a, string s61b, string s86)
        {
            BankStstementPosition.Date = DateTimeHelper.ToDateTimeShort(s61a.Substring(0, 6));
            string s = s61a.Substring(6);
            BankStstementPosition.DebitCredit = s.Substring(0, 1);
            s = s.Substring(3);

            BankStstementPosition.Value = Convert.ToDecimal(s.Substring(0, s.IndexOf('F')));
            BankStstementPosition.BookingText = s61b;

            //BankStstementPosition.TransCode = s.Substring(0, 3);
            //s = s.Substring(3);
            //if (s != "NONREF")
            //{
            //    BankStstementPosition.CustRef = s.Substring(0, s.IndexOf("//"));
            //    s = s.Substring(s.IndexOf("//") + 2);
            //    BankStstementPosition.BankRef = s;
            //}
            //else
            //{
            //    BankStstementPosition.CustRef = "";
            //    BankStstementPosition.BankRef = "";
            //}

            BankStstementPosition.Descr1 = s86;
            BankStstementPosition.CustAccount = FindCustAccount(s86.Replace(" ",""));

        }

        private static string FindCustAccount(string sLine)
        {

            Regex regex = new Regex(@"\d{26}");
            return regex.Match(sLine).Success ? regex.Match(sLine).ToString() : string.Empty;
        }
    }
}