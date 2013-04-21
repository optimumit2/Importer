using System;
using System.Collections.Generic;

namespace FKIntegration.Repositories
{
    public interface ICurrencyRepository
    {
        List<Currency> GetAll();
        Currency GetById(Currency currency);
        Currency Find(Predicate<Currency> currency);
        void Save(Currency currency);
    }
}