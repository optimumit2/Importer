using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
namespace DAMAL.ZP.BOL.BankStatements.Import.PKOXML
{
    public class PKOxmlBankStatementPosition

    {
        string sDescription;
        public PKOxmlBankStatementPosition(string POKxmlDesctiption)
        {
            sDescription = POKxmlDesctiption;
        }
        public string PKOAccount
        {
            get { return AccountNo(sDescription); }
        }
        public string PKOTextDesc
        {
            get { return TextDescriptionPKO(sDescription); }
        }
        public string PKOxmlContractor
        {
            get { return ContractorName(sDescription); }
        }
        
        
        private string AccountNo(string sSTR)  
        {
            string MyAccount="";
            
            if (sSTR.IndexOf("Nr rach. przeciwst.:") != -1)
            {
                sSTR=sSTR.Replace("Nr rach. przeciwst.:", "");
                MyAccount = sSTR.Substring(0, sSTR.IndexOf("\n"));
                MyAccount = MyAccount.Replace(" ", "");     
            }
            return MyAccount;
        }
        private string TextDescriptionPKO(string sSTR)
        {
            string Dessription = sSTR;
            if (sSTR.IndexOf("Tytuł:") != -1)
            {

                Dessription = sSTR.Substring( sSTR.IndexOf("Tytuł:"));
                Dessription = Dessription.Replace("Tytuł:", "");
            }
            if (sSTR.IndexOf("Symbol formularza:") != -1)
            {

                Dessription = sSTR.Substring(sSTR.IndexOf("Symbol formularza:"));
                Dessription = Dessription.Replace("Symbol formularza:", "");
            }
            if (sSTR.IndexOf("Symbol formularza:") != -1)
            {

                Dessription = sSTR.Substring(sSTR.IndexOf("Symbol formularza:"));
                Dessription = Dessription.Replace("Symbol formularza:", "");
            }
            if (sSTR.IndexOf("Deklaracja:") != -1)
            {

                Dessription = sSTR.Substring(sSTR.IndexOf("Deklaracja:"));
                Dessription = Dessription.Replace("Deklaracja:", "");
            }
            return Dessription;
        }
        
        private string ContractorName(string sSTR)
        {
            string MyContractor = "";

            if (sSTR.IndexOf("Dane adr. rach. przeciwst.:") != -1)
            {
                //sSTR = sSTR.Replace("Nr rach. przeciwst.:", "");
                MyContractor = sSTR.Substring(sSTR.IndexOf("Dane adr. rach. przeciwst.:"), sSTR.IndexOf("\n"));
                MyContractor = MyContractor.Replace("Dane adr. rach. przeciwst.:", "");
                MyContractor = MyContractor.Replace("Tytuł:", "");
            }
            return MyContractor;
        }
    }
}
