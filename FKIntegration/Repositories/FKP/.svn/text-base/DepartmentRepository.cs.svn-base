using FKIntegration.CardInexes;
using MXDokFK;

namespace FKIntegration.Repositories.FKP
{
    internal class DepartmentRepository : CardIndexBaseRepository<Department>, IDepartmentRepository
    {        
        public DepartmentRepository(FKPDatabase fkDatabase)
            : base(fkDatabase, SubjectType.SUB_URZEDY)
        { }

        internal override void FillSyncroBody(IItgCommon itgCommon, Department entity)
        {
            entity.FillSyncroBody(itgCommon);
        }

        internal override Department Get(SyncroData syncroData)
        {
            return new Department(syncroData);
        }

        public Department GetByPosition(Department department)
        {
            return GetByPosition(department.Position);
        }
    }
}