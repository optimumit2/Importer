//using System;
//using System.Collections.Generic;
//using DAMAL.BOL.IWB;
//using Equin.ApplicationFramework;

//namespace DAMAL.ZP.BOL.BankStatements
//{
//    /// <summary>
//    /// Klasa wyszukiwająca kontrahentów w bazie FK i mapująca go na odpowiedniego kontrahenta z wyciągu bankowego.
//    /// Sprawdzenie odbywa się po numerze rachunku.
//    /// Jeżeli nie zostanie znaleziony kontrahent jest możliwośc dodanie go do bazy. 
//    /// </summary>
//    public class CustomerMapping
//    {
//        public static bool isNew;
//        private static string m_BankAccount;
//        //private static List<FKContractor> m_custList;
//       private static int m_VirtualId;
//        private static string m_NIP;
//        private static string m_Skrot;
//        private static int m_ID;

//        //static CustomerMapping()
//        //{
//        //    m_CustomerListHasChange = true;
//        //    CheckFKCustomerHasChange();
//        //}

//        //public static bool CustomerListHasChange
//        //{
//        //    get { return m_CustomerListHasChange; }
//        //    set { m_CustomerListHasChange = value; }
//        //}

//        private static List<FKContractor> m_FkContractors;
//        public static List<FKContractor> FKContractors
//        {
//            get { return m_FkContractors; }
//            set { m_FkContractors = value; }
//        }


//        private static string NormalizeNIP(string NIP)
//        {
//            if (NIP == null)
//                return "";
//            string s = NIP.Trim().Replace("-", "");
//            return s;
//        }

//        private static bool EqualNIP(FKContractor cust)
//        {
//            return (NormalizeNIP(cust.NIP) == m_NIP);
//        }

//        private static bool EqualSkrot(FKContractor cust)
//        {
//            return (cust.Skrot == m_Skrot);
//        }

//        private static string NormalizeBankAccount(string BankAccount)
//        {
//            if (BankAccount == null)
//                return "";
//            string s = BankAccount.Trim().Replace(" ", "");
//            return s;
//        }

//        private static bool EqualBankAccount(FKContractor cust)
//        {
//            return
//                (NormalizeBankAccount(cust.Rachunek1) == m_BankAccount ||
//                 NormalizeBankAccount(cust.Rachunek2) == m_BankAccount);
//        }

//        private static bool EqualVirtualId(FKContractor cust)
//        {
//            return
//                 cust.Pozycja == m_VirtualId;
//        }

//        private static bool EqualID(FKContractor cust)
//        {
//            return
//                cust.ID == m_ID;
//        }

//        public static FKContractor MapCustomer(BankStatementPosition bsPos)
//        {
//            //CheckFKCustomerHasChange();

//       //   if (!string.IsNullOrEmpty(bsPos.ContractorKhFK.ToString()) &&(bsPos.ContractorKhFK>0))
//         // {
//          //    bsPos.FKContractor = FKContractor.FindFkCustomerByPosition(bsPos.ContractorKhFK);
//        //  }
            
//            if(m_FkContractors == null)
//                throw new ArgumentException("Bład programu.");
//            if (m_FkContractors.Count == 0)
//                return null;


//            isNew = false;
//            FKContractor cust = null;

//            if (bsPos.Customer == null)
//                return null;

//            if (bsPos.FKContractor != null) 
//            {
//                m_ID = bsPos.FKContractor.ID;
//                cust = m_FkContractors.Find(EqualID);
//            }

//            if ((!string.IsNullOrEmpty(bsPos.Customer.BankAccount)) && (cust == null))
//            {
//                m_BankAccount = NormalizeBankAccount(bsPos.Customer.BankAccount);
//                cust = m_FkContractors.Find(EqualBankAccount);
//            }

//            //m_Skrot = bsPos.Customer.Name;

//            //if (cust == null)
//            //{
//            //    cust = m_custList.Find(EqualSkrot);
//            //    int i = 0;
//            //    while (cust != null)
//            //    {
//            //        //i++;
//            //        //bsPos.Customer.Name += i;
//            //        m_Skrot = bsPos.Customer.Name;
//            //        cust = m_custList.Find(EqualSkrot);
//            //    }

//            //   // cust = bsPos.GetFKCustomer(m_NIP);
//            //    isNew = true;
//            //}

//            if(cust == null )
//                if (!string.IsNullOrEmpty(bsPos.Customer.BankAccount))
//                {
//                    m_VirtualId = bsPos.VirtualId;
//                    cust = m_FkContractors.Find(EqualVirtualId);
//                }

//            if (cust == null)
//                cust = m_FkContractors.Find(EqualNIP);
//            if (cust == null)
//                cust = m_FkContractors.Find(EqualBankAccount);
//            return cust;
//        }

//        //private static void CheckFKCustomerHasChange()
//        //{
//        //    if (m_CustomerListHasChange)
//        //    {
//        //        m_custList = FKContractor.GetCustomers();
//        //        m_CustomerListHasChange = false;
//        //    }
//        //}
//    }
//}