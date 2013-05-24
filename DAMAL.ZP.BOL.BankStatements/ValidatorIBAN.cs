using System;

namespace DAMAL.ZP.BOL.BankStatements
{
    public class ValidatorIBAN
    {
        private string m_Account;

        public ValidatorIBAN(string account)
        {
            m_Account = account;
        }

        public string Account
        {
            get { return m_Account; }
            set { m_Account = value; }
        }

        public bool CheckIBAN()
        {
            if (m_Account.Length != 26)
                return false;

            int[] mod97_10 = {
                                 1, 10, 3, 30, 9, 90, 27, 76, 81, 34, 49, 5, 50, 15, 53, 45,
                                 62, 38, 89, 17, 73, 51, 25, 56, 75, 71, 31, 19, 93, 57, 85, 74, 61, 28
                             };
            string readyAccount;
            double sum = 0;
            int cc;

            readyAccount = m_Account.Substring(2, 24) + "2521" + m_Account.Substring(0, 2);


            for (int k = 0; k < readyAccount.Length; k++)
            {
                int n = Convert.ToInt32(readyAccount[readyAccount.Length - k - 1]) - 48;
                sum += mod97_10[k] * n;
            }

            cc = Convert.ToInt32(sum % 97);

            if (cc == 1)
                return true;
            else
                return false;
        }
    }
}