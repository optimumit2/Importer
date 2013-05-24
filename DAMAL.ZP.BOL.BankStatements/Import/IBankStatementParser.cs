using System.IO;

using FKIntegration.Documents;

namespace DAMAL.ZP.BOL.BankStatements.Import
{
    public interface IBankStatementParser
    {
        BankStatement GetBankStatement();
        void SetImportFileName(ImportFile fileName);
        bool ReadStream(StreamReader sr, ref string line);
    }

    public abstract class BankStatementParser : IBankStatementParser
    {
        protected readonly MT940BankStatement m_BankStatement;
        protected string m_Content;

        public BankStatementParser()
        {
            m_BankStatement = new MT940BankStatement();
        }

        protected abstract void ReadHeader(StreamReader sr, ref string line);

        protected abstract void ReadBlocks(StreamReader sr, ref string line);

        protected virtual bool IsNotEmpty()
        {
            return m_BankStatement.IsNotEmpty();
        }
        
        public virtual void SetImportFileName(ImportFile importFile)
        {
            m_BankStatement.FileName = importFile.GetFileName();
        }

        public virtual bool ReadStream(StreamReader sr, ref string line)
        {
            ReadHeader(sr, ref line);
            ReadBlocks(sr, ref line);
            return IsNotEmpty();
        }

        public virtual BankStatement GetBankStatement()
        {
            BankStatement bankStatement = m_BankStatement.GetBankStatement();
            bankStatement.Content = m_Content;
            return bankStatement;
        }
    }
}