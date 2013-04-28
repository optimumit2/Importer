using MXDokFK;

namespace FKIntegration.Repositories.FKP
{
    internal class CurrencyRepository : CardIndexBaseRepository<Currency>, ICurrencyRepository
    {                
        public CurrencyRepository(FKPDatabase database)
            : base(database, SubjectType.SUB_WALUTY)
        {
        }

        internal override void FillSyncroBody(IItgCommon itgCommon, Currency entity)
        {
            entity.FillSyncroBody(itgCommon);
        }

        internal override Currency Get(SyncroData syncroData)
        {
            return new Currency(syncroData);
        }        

        public Currency GetById(Currency currency)
        {
            return GetById(currency.Id);
        }            
    }
}