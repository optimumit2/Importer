using System;

namespace DAMAL.ZP.BOL.BankStatements.Import.TransDebit
{
    public class  TransDebitBankStatementPosition
    {
        public MT940BankStatementPosition BankStstementPosition;

        public TransDebitBankStatementPosition()
        {
            BankStstementPosition = new MT940BankStatementPosition();
            BankStstementPosition.HasVirtualId = true;
        }


        public void DecodeData(string s61a, string s86)
        {

            s61a = s61a.Remove(0, s61a.IndexOf(",")+ 1);
            BankStstementPosition.Date = DateTimeHelper.ToDateTimeLong(s61a.Substring(0, 8));
            s61a = s61a.Remove(0,s61a.IndexOf(",")+1);
            s61a = s61a.Remove(0, s61a.IndexOf(",") + 1);
            BankStstementPosition.Value = Convert.ToDecimal(s61a.Substring(0, s61a.IndexOf(",") - 2));
            s61a = s61a.Remove(0, s61a.IndexOf(",") - 2);
            BankStstementPosition.Value += (0.01M * Convert.ToDecimal(s61a.Substring(0, s61a.IndexOf(","))));
            s61a = s61a.Remove(0, s61a.IndexOf(",") + 1);
            if (s61a.IndexOf(",") == 26)
            {
                BankStstementPosition.CustAccount = s61a.Substring(0, 26);
                s61a = s61a.Remove(0, 27);
            }
            else
                s61a = s61a.Remove(0, s61a.IndexOf(",") + 1);

            BankStstementPosition.DebitCredit = "C";
            s61a = s61a.Remove(0, s61a.IndexOf(";") + 1);
            s61a = s61a.Remove(0, s61a.IndexOf(";")+1);
           
            BankStstementPosition.Descr1 = s61a.Substring(0, s61a.IndexOf(";"));
            s61a = s61a.Remove(0, s61a.IndexOf(";")+1);
            s61a = s61a.Remove(0, s61a.IndexOf(",") + 1);
            BankStstementPosition.CustName1 = s61a.Substring(0,s61a.IndexOf(";")); 
        }
    }
}