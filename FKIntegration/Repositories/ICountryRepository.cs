using System;
using System.Collections.Generic;
using FKIntegration.CardInexes;

namespace FKIntegration.Repositories
{
    public interface ICountryRepository
    {
        List<Country> GetAll();
        Country GetById(Country country);
        Country Find(Predicate<Country> condition);
        void Save(Country country);
    }
}