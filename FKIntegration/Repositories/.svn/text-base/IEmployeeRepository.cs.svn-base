using System;
using System.Collections.Generic;
using FKIntegration.CardInexes;

namespace FKIntegration.Repositories
{
    public interface IEmployeeRepository
    {
        List<Employee> GetAll();
        Employee GetByPosition(Employee employee);
        Employee Find(Predicate<Employee> condition);
        void Save(Employee employee);
    }
}