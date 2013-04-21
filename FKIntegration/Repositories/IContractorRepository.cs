using System;
using System.Collections.Generic;
using FKIntegration.CardInexes;

namespace FKIntegration.Repositories
{
    public interface IContractorRepository
    {
        List<Contractor> GetAll();
        Contractor GetByPosition(Contractor contractor);
        Contractor Find(Predicate<Contractor> condition);
        void Save(Contractor contractor);
    }
}