using System;
using FKIntegration.Repositories;

namespace FKIntegration
{
    public class FKFConfiguration
    {
        public IContractorRepository ContractorRepository;
        public IEmployeeRepository EmployeeRepository;
        public IBankingAccountRepository BankingAccountRepository;
        public ICountryRepository CountryRepository;
        public IDepartmentRepository DepartmentRepository;
        public IAccountRepository AccountRepository;
        public IDocumentDefinitionRepository DocumentDefinitionRepository;
        
        public bool LogUser()
        {
            throw new NotImplementedException();
        }

        public CompanyInfo GetCompanyInfo()
        {
            throw new NotImplementedException();
        }

        public void IsInTransaction(bool isInTransaction)
        {
            throw new NotImplementedException();
        }
    }
}