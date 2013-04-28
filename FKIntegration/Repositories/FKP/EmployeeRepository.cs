using FKIntegration.CardInexes;
using MXDokFK;

namespace FKIntegration.Repositories.FKP
{
    internal class EmployeeRepository : CardIndexBaseRepository<Employee>, IEmployeeRepository
    {        
        public EmployeeRepository(FKPDatabase fkDatabase)
            : base(fkDatabase, SubjectType.SUB_PRACOWNICY)
        { }

        internal override void FillSyncroBody(IItgCommon itgCommon, Employee entity)
        {
            entity.FillSyncroBody(itgCommon);
        }

        internal override Employee Get(SyncroData syncroData)
        {
            return new Employee(syncroData);
        }
        
        public Employee GetByPosition(Employee employee)
        {
            return GetByPosition(employee.Position);
        }
    }
}