using System;
using System.Collections.Generic;
using FKIntegration.CardInexes;

namespace FKIntegration.Repositories
{
    interface IDictionaryRepository
    {
        List<Dictionary> GetAll();
        Dictionary GetById(Dictionary dictionary);
        Dictionary  Find(Predicate<Dictionary> dictionary);
        void Save(Dictionary dictionary);
    }
}