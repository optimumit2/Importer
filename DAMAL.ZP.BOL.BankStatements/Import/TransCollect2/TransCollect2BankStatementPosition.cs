using System;

namespace DAMAL.ZP.BOL.BankStatements.Import.TransCollect2
{
    public class  TransCollect2BankStatementPosition
    {
        public MT940BankStatementPosition BankStstementPosition;

        public TransCollect2BankStatementPosition()
        {
            BankStstementPosition = new MT940BankStatementPosition();
            BankStstementPosition.HasVirtualId = true;
        }


        public void DecodeData(string s61a, string s86)
        {
            s61a = s61a.Remove(0, s61a.IndexOf(",")+ 1);
            s61a = s61a.Remove(0, s61a.IndexOf(",") + 1);
            s61a = s61a.Remove(0, s61a.IndexOf(",") + 1);
            //s61a = s61a.Remove(0, s61a.IndexOf(",") + 1);
            BankStstementPosition.VirtualId = Convert.ToInt32(s61a.Substring(0, s61a.IndexOf(",")));
            s61a = s61a.Remove(0, s61a.IndexOf(",") + 1);
            //nr rachunku
            BankStstementPosition.CustAccount = s61a.Substring(0, s61a.IndexOf(","));
            //s61a = s61a.Remove(0, s61a.IndexOf(",") + 1);

            //virtual id (4 ostatnie cyfry z virtualnego rachunku)
            //s61a = s61a.Remove(0, s61a.IndexOf(",") - 4);
            //0BankStstementPosition.VirtualId = Convert.ToInt16(s61a.Substring(0, 4));
            s61a = s61a.Remove(0, s61a.IndexOf(",") + 1);

            //kwota
            BankStstementPosition.Value = Convert.ToDecimal(s61a.Substring(0, s61a.IndexOf(",") - 2));
            s61a = s61a.Remove(0, s61a.IndexOf(",") - 2);
            BankStstementPosition.Value += (0.01M * Convert.ToDecimal(s61a.Substring(0, s61a.IndexOf(","))));
            s61a = s61a.Remove(0, s61a.IndexOf(",") + 1);

            
            
            BankStstementPosition.DebitCredit = "C";
            s61a = s61a.Remove(0, s61a.IndexOf("\"")+1);
            BankStstementPosition.Descr1 = s61a.Substring(0, s61a.IndexOf("\""));
            s61a = s61a.Remove(0, s61a.IndexOf("\"")+3);
            BankStstementPosition.CustName1 = s61a.Substring(0,s61a.IndexOf("\"")); 
        }
    }
}