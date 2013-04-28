using FKIntegration.CardInexes;
using MXDokFK;

namespace FKIntegration.Repositories.FKP
{
    internal class CountryRepository : CardIndexBaseRepository<Country>, ICountryRepository
    {        
        public CountryRepository(FKPDatabase fkDatabase)
            : base(fkDatabase, SubjectType.SUB_KRAJ)
        { }

        internal override void FillSyncroBody(IItgCommon itgCommon, Country entity)
        {
            entity.FillSyncroBody(itgCommon);
        }

        internal override Country Get(SyncroData syncroData)
        {
            return new Country(syncroData);
        }
        
        public Country GetById(Country country)
        {
            return GetById(country.Id);
        }
    }
}