using System;

namespace DAMAL.ZP.BOL.BankStatements.Import.Societe
{
    public class SocieteBankStatementPosition
    {
        private string m_BankRef;
        private string m_Currency3;
        private string m_CustRef;
        private DateTime m_Date;
        private string m_DebitCredit;
        private string m_Descr;
        private string m_TransCode;
        private decimal m_Value;
        private string m_CustName;
        private string m_CustAcc;

        public DateTime Date
        {
            get { return m_Date; }
            set { m_Date = value; }
        }

        public string DebitCredit
        {
            get { return m_DebitCredit; }
            set { m_DebitCredit = value; }
        }


        public string Currency3
        {
            get { return m_Currency3; }
            set { m_Currency3 = value; }
        }

        public decimal Value
        {
            get { return m_Value; }
            set { m_Value = value; }
        }

        public string TransCode
        {
            get { return m_TransCode; }
            set { m_TransCode = value; }
        }

        public string CustRef
        {
            get { return m_CustRef; }
            set { m_CustRef = value; }
        }

        public string BankRef
        {
            get { return m_BankRef; }
            set { m_BankRef = value; }
        }

        public string Descr
        {
            get { return m_Descr; }
            set { m_Descr = value; }
        }

        public string CustName
        {
            get { return m_CustName; }
            set { m_CustName = value; }
        }

        public string CustAcc
        {
            get { return m_CustAcc; }
            set { m_CustAcc = value; }
        }

        public void DecodeData(string s61a, string s61b, string s86)
        {
            m_Date = DateTimeHelper.ToDateTimeShort(s61a.Substring(0, 6));
            m_DebitCredit = s61a.Substring(10, 1);
            
            //m_Currency3 = s61a.Substring(11, 1);
            //DL dla Societe Generale
            //m_Currency3 = s61a.Substring(s61a.IndexOf(",")+3, 1);

            //string s = s61a.Substring(12);
            //DL dla Societe Generale
            string s = s61a.Substring(11);
            //

            m_Value = Convert.ToDecimal(s.Substring(0, s.IndexOf("N")));
            //s = s.Substring(s.IndexOf('N') + 1);
            //DL dla Societe Generale
            s = s.Substring(s.IndexOf('N'));
            //

            //m_TransCode = s.Substring(0, 3);
            //s = s.Substring(3);
            //DL dla Societe Generale
            m_TransCode = s.Substring(0, 4);
            //s = s.Substring(0);
            //
            if (s.IndexOf("//") > 0)
            {
                m_CustRef = s.Substring(0, s.IndexOf("//"));
                m_BankRef = s.Substring(s.IndexOf("//") + 2);
            }
            
            //m_Descr = s61b.Substring(s61b.IndexOf("-") + 1);
            //DL dla Societe Generale
            m_Descr = s86;

            //
            //SocieteBankStatementPosition.GVCCode = s86.Substring(0, 3);
            string[] paramArr = s86.Substring(0).Split("|".ToCharArray());
            for (int i = 0; i < paramArr.Length; i++)
            {
                string code = "";
                string value = "";

                int iPos = paramArr[i].IndexOf(":");
                if (iPos <= 0)
                    iPos = 0;
                else
                {
                    code = paramArr[i].Substring(0, iPos);
                    value = paramArr[i].Substring(iPos + 1);
                }
                switch (code)
                {
                    case "FAV":
                        m_CustName = value;
                        break;
                    case "TO":
                        m_CustAcc = value;
                        break;
                    case "FROM":
                        m_CustAcc = value;
                        break;
                    case "RE":
                        m_Descr = value;
                        break;
                    case "CODE":
                        m_TransCode = value;
                        break;
                    default:
                        m_Descr += value;
                        break;

                           
                }
            }

        }



    }
}