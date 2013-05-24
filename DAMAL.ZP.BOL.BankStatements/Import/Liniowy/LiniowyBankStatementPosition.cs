using System;

namespace DAMAL.ZP.BOL.BankStatements.Import.Liniowy
{
    public class LiniowyBankStatementPosition
    {
        public MT940BankStatementPosition BankStstementPosition;

        public LiniowyBankStatementPosition()
        {
            BankStstementPosition = new MT940BankStatementPosition();
        }


        public void DecodeData(string s61a, string s86)
        {
            s61a = s61a.Remove(0, 10);
            BankStstementPosition.Date = DateTimeHelper.ToDateTime(s61a.Substring(0, 10), "-");
            s61a = s61a.Remove(0, 10);
            BankStstementPosition.BookingText = s61a.Substring(0, 20).Trim();
            s61a = s61a.Remove(0, 28);
            BankStstementPosition.CustAccount = s61a.Substring(0, 34).Trim().Replace(" ", "");
            s61a = s61a.Remove(0, 34);
            BankStstementPosition.Value = Convert.ToDecimal(s61a.Substring(0, 22).Trim());
            s61a = s61a.Remove(0, 22);
            BankStstementPosition.DebitCredit = s61a.Substring(0, 1);
            s61a = s61a.Remove(0, 4);
            BankStstementPosition.CustName1 = s61a.Substring(0, 35).Trim();
            s61a = s61a.Remove(0, 35);
            BankStstementPosition.CustAddress1 = s61a.Substring(0, 35).Trim();
            s61a = s61a.Remove(0, 35);
            BankStstementPosition.CustAddress2 = s61a.Substring(0, 35).Trim();
            s61a = s61a.Remove(0, 35);
            BankStstementPosition.CustName2 = s61a.Substring(0, 35).Trim();
            s61a = s61a.Remove(0, 35);
            BankStstementPosition.Descr1 = s61a.Substring(0, 35).Trim();
            s61a = s61a.Remove(0, 35);
            BankStstementPosition.Descr2 = s61a.Substring(0, 35).Trim();
            s61a = s61a.Remove(0, 35);
            BankStstementPosition.Descr3 = s61a.Substring(0, 35).Trim();
            s61a = s61a.Remove(0, 35);
            BankStstementPosition.Descr4 = s61a.Substring(0, 35).Trim();
            //s61a = s61a.Remove(0, 35);
        }
    }
}