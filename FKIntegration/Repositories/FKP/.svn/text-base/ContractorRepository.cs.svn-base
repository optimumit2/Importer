using FKIntegration.CardInexes;
using MXDokFK;

namespace FKIntegration.Repositories.FKP
{
    internal class ContractorRepository : CardIndexBaseRepository<Contractor>, IContractorRepository
    {        
        public ContractorRepository(FKPDatabase fkDatabase)
            : base(fkDatabase, SubjectType.SUB_KONTRAHENCI)
        { }

        internal override void FillSyncroBody(IItgCommon itgCommon, Contractor entity)
        {
            entity.FillSyncroBody(itgCommon);
        }

        internal override Contractor Get(SyncroData syncroData)
        {
            return new Contractor(syncroData);
        }        

        public Contractor GetByPosition(Contractor contractor)
        {
            return GetByPosition(contractor.Position);
        }        
    }
}