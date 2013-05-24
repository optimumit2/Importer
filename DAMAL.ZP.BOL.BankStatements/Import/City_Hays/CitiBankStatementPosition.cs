using System;

namespace DAMAL.ZP.BOL.BankStatements.Import.Citi
{
    public class CitiBankStatementPosition
    {
        public MT940BankStatementPosition BankStstementPosition;

        public CitiBankStatementPosition()
        {
            BankStstementPosition = new MT940BankStatementPosition();
        }


        public void DecodeData(string s61a, string s86)
        {
            string[] s61aTab = s61a.Split(';');

            s61a = s61a.Remove(0, 10);
            BankStstementPosition.Date = DateTimeHelper.ToDateTime(s61aTab[2], string.Empty);
            BankStstementPosition.BookingText = s61aTab[3];
            BankStstementPosition.CustAccount = s61aTab[13];
            string value = s61aTab[7].Replace("(", "").Replace(")", "");
            BankStstementPosition.Value = Convert.ToDecimal(value);
            BankStstementPosition.DebitCredit = s61aTab[5];
            BankStstementPosition.CustName1 = s61aTab[8];
            BankStstementPosition.CustAddress1 = s61aTab[9];
            BankStstementPosition.CustAddress2 = s61aTab[10];
            BankStstementPosition.Descr1 = s61aTab[14];
        }
    }
}