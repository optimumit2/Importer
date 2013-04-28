using System.Collections.Generic;

namespace FKIntegration.Repositories
{
    public interface IAccountRepository
    {
        List<Account> GetAll();
        List<string> GetAllNumbers();
    }
}