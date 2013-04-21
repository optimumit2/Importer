using System;
using System.Collections.Generic;
using FKIntegration.CardInexes;

namespace FKIntegration.Repositories
{
    public interface IDepartmentRepository
    {
        List<Department> GetAll();
        Department GetByPosition(Department department);
        Department Find(Predicate<Department> condition);
        void Save(Department department);
    }
}