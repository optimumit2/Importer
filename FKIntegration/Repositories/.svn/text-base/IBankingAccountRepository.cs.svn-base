using System;
using System.Collections.Generic;
using FKIntegration.CardInexes;

namespace FKIntegration.Repositories
{
    public interface IBankingAccountRepository
    {
        List<BankingAccount> GetAll();
        BankingAccount GetById(BankingAccount bankingAccounts);
        BankingAccount Find(Predicate<BankingAccount> condition);
        void Save(BankingAccount bankingAccounts);
    }
}