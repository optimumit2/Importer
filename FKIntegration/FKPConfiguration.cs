using System;
using System.Collections.Generic;
using FKIntegration.Repositories;
using FKIntegration.Repositories.FKP;

namespace FKIntegration
{
    public class FKPConfiguration
    {
        private string _userName;
        public FKPConfiguration User(string userName)
        {
            _userName = userName;
            return this;
        }

        private string _password;
        public FKPConfiguration Password(string password)
        {
            _password = password;
            return this;
        }

        private string _databasePath;
        public FKPConfiguration Database(string databasePath)
        {
            _databasePath = databasePath;
            return this;
        }

        private bool _useSilentMode;
        public FKPConfiguration UseSilentMode()
        {
            _useSilentMode = true;
            return this;
        }

        public FKPConfiguration UseShowErrorsMode()
        {
            _useSilentMode = false;
            return this;
        }

        internal bool LogUser()
        {
            try
            {
                var database = GetDatabase();
                //{
                //    ErrorMode = _useSilentMode ? FKErrorMode.Silent : FKErrorMode.ShowErrors
                //};
                database.Open();
                if (!database.IsOpen())
                    return false;

                //select last booking year
                List<YearInfo> yearInfoList = GetCompanyInfo().GetYearInfoList();
                if (yearInfoList != null && yearInfoList.Count > 0)
                    FKIntegrationManager.SelectBookingYear(yearInfoList[yearInfoList.Count - 1]);

                return true;
            }
            catch (Exception exception)
            {
                throw new FKIntegrationException(exception.Message, exception);
            }
        }

        private ContractorRepository _contractorRepository;
        internal IContractorRepository ContractorRepository
        {
            get { return _contractorRepository = new ContractorRepository(GetDatabase()); }
        }

        private EmployeeRepository _employeeRepository;
        internal IEmployeeRepository EmployeeRepository
        {
            get { return _employeeRepository = new EmployeeRepository(GetDatabase()); }
        }

        private IBankingAccountRepository _bankingAccountRepository;
        public IBankingAccountRepository BankingAccountRepository
        {
            get { return _bankingAccountRepository = new BankingAccountRepository(GetDatabase()); }
        }

        private ICountryRepository _countryRepository;
        public ICountryRepository CountryRepository
        {
            get { return _countryRepository = new CountryRepository(GetDatabase()); }
        }

        private IDepartmentRepository _departmentRepository;
        public IDepartmentRepository DepartmentRepository
        {
            get { return _departmentRepository = new DepartmentRepository(GetDatabase()); }
        }

        private IAccountRepository _accountRepository;
        public IAccountRepository AccountRepository
        {
            get { return _accountRepository = new AccountRepository(GetDatabase()); }
        }

        private IDocumentDefinitionRepository _documentDefinitionRepository;
        public IDocumentDefinitionRepository DocumentDefinitionRepository
        {
            get { return _documentDefinitionRepository = new DocumentDefinitionRepository(GetDatabase()); }
        }

        public CompanyInfo GetCompanyInfo()
        {
            //using (var database = GetDatabase())
            //{
            var database = GetDatabase();
            database.Open();
            return new CompanyInfo(database);
            //}
        }

        private static FKPDatabase _fkpDatebase;
        

        internal FKPDatabase GetDatabase()
        {
            return _fkpDatebase ?? (_fkpDatebase = new FKPDatabase(new FKPUser(_userName, _password), _databasePath));
        }

        internal void IsInTrasaction(bool isInTransaction)
        {
            if (_contractorRepository != null)
                _contractorRepository.IsInTransaction(isInTransaction);
            if (_employeeRepository != null)
                _employeeRepository.IsInTransaction(isInTransaction);
        }
    }
}