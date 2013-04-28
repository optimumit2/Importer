using FKIntegration.CardInexes;
using MXDokFK;

namespace FKIntegration.Repositories.FKP
{
    internal class BankingAccountRepository : CardIndexBaseRepository<BankingAccount>, IBankingAccountRepository
    {
        public BankingAccountRepository(FKPDatabase fkDatabase)
            : base(fkDatabase, SubjectType.SUB_RACHUNKI)
        { }

        internal override void FillSyncroBody(IItgCommon itgCommon, BankingAccount entity)
        {
            entity.FillSyncroBody(itgCommon);
        }

        internal override BankingAccount Get(SyncroData syncroData)
        {
            return new BankingAccount(syncroData);
        }

        public BankingAccount GetById(BankingAccount bankingAccounts)
        {
            return GetById(bankingAccounts.Id);
        }
    }
}