//using System;
//using System.Threading;
//using System.Windows.Forms;
//using DAMAL.BOL.IWB;
//using DAMAL.BOL.IWB.FKDocument;
//using DAMAL.ZP.BOL.BankStatements.Dialog;
//using FKIntegration.Documents;

//namespace DAMAL.ZP.BOL.BankStatements
//{
//    public delegate void Tick(int percent);

//    public class FKSender : IFKSender
//    {
//        /// <summary>
//        /// Zapis do systemu FK
//        /// </summary>
//        /// <param name="bs">wyciąg bankowy </param>        
//        /// <param name="companyBankAccount">Paramerty zapisu</param>        
//        /// <returns> Zwraca informację o poprawności zapisu</returns>
//        public bool SendToFK(BankStatement bs, CompanyBankAccounts companyBankAccount)
//        {
//            return SendOneBankStatement(bs);
           
//        }

//        private static bool SendOneBankStatement(BankStatement bs)
//        {
//            FKDocument doc = new FKDocument(bs);
//            try
//            {

   

//            if (!doc.IsValid(CurrentCompanySetting.FkCompany))
//            {
//                DlgAddDocumentWithErrors dialog = new DlgAddDocumentWithErrors(doc);
//                DialogResult dialogResult = dialog.ShowDialog();

//                if (dialogResult == DialogResult.Cancel)
//                {
//                    return false;
//                }
//            }

//            bs.FKDocumentName = doc.Insert(CurrentCompanySetting.FkCompany.Year);
//            return true;

//        }
//        catch (Exception ex)
//        {

//            throw new Exception(ex.Message);
//        }
//        }
//    }
//}