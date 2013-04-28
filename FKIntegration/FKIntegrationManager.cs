using System;
using FKIntegration.Repositories;
using MXDokFK;

namespace FKIntegration
{
    public class FKIntegrationManager
    {
        private readonly static ItgInfoClass ItgInfoClass;

        static FKIntegrationManager()
        {
            ItgInfoClass = new ItgInfoClass();
        }

        public static string Description
        {
            get { return ItgInfoClass.get_Description(0); }
        }

        public static string Version
        {
            get { return ItgInfoClass.get_Version(0); }
        }

        private static FKPConfiguration _fkpConfiguration;

        public static FKPConfiguration ConfigureFKP()
        {
            _fkpConfiguration = new FKPConfiguration();
            return _fkpConfiguration;
        }


        private static FKFConfiguration _fkfConfiguration;

        public static FKFConfiguration ConfigureFKF()
        {
            _fkfConfiguration = new FKFConfiguration();
            return _fkfConfiguration;
        }

        public static IBankingAccountRepository BankingAccountRepository
        {
            get
            {
                if (_fkpConfiguration != null)
                    return _fkpConfiguration.BankingAccountRepository;
                if (_fkfConfiguration != null)
                    return _fkfConfiguration.BankingAccountRepository;

                throw new FKIntegrationException("Pobranie repozytorium rachunków bankowych poprzedzone musi być skonfigurowaniem biblioteki");
            }
        }

        public static IContractorRepository ContractorsRepository
        {
            get
            {
                if (_fkpConfiguration != null)
                    return _fkpConfiguration.ContractorRepository;
                if (_fkfConfiguration != null)
                    return _fkfConfiguration.ContractorRepository;

                throw new FKIntegrationException("Pobranie repozytorium kontrahentów poprzedzone musi być skonfigurowaniem biblioteki");
            }
        }

        public static ICountryRepository CountryRepository
        {
            get
            {
                if (_fkpConfiguration != null)
                    return _fkpConfiguration.CountryRepository;
                if (_fkfConfiguration != null)
                    return _fkfConfiguration.CountryRepository;

                throw new FKIntegrationException("Pobranie repozytorium krajów poprzedzone musi być skonfigurowaniem biblioteki");
            }
        }

        public static IDepartmentRepository DepartmentRepository
        {
            get
            {
                if (_fkpConfiguration != null)
                    return _fkpConfiguration.DepartmentRepository;
                if (_fkfConfiguration != null)
                    return _fkfConfiguration.DepartmentRepository;

                throw new FKIntegrationException("Pobranie repozytorium urzędów poprzedzone musi być skonfigurowaniem biblioteki");
            }
        }

        public static IEmployeeRepository EmployeeRepository
        {
            get
            {
                if (_fkpConfiguration != null)
                    return _fkpConfiguration.EmployeeRepository;
                if (_fkfConfiguration != null)
                    return _fkfConfiguration.EmployeeRepository;

                throw new FKIntegrationException("Pobranie repozytorium pracowników poprzedzone musi być skonfigurowaniem biblioteki");
            }
        }

        public static IAccountRepository AccountRepository
        {
            get
            {
                if (_fkpConfiguration != null)
                    return _fkpConfiguration.AccountRepository;
                if (_fkfConfiguration != null)
                    return _fkfConfiguration.AccountRepository;

                throw new FKIntegrationException("Pobranie repozytorium kont poprzedzone musi byc skonfigurowaniem biblioteki");
            }
        }

        public static IDocumentDefinitionRepository DocumentDefinitionRepository
        {
            get
            {
                if (_fkpConfiguration != null)
                    return _fkpConfiguration.DocumentDefinitionRepository;
                if (_fkfConfiguration != null)
                    return _fkfConfiguration.DocumentDefinitionRepository;

                throw new FKIntegrationException("Pobranie repozytorium definicji dokumentow poprzedzone musi byc skonfigurowaniem biblioteki");
            }
        }

        public static CompanyInfo GetCompanyInfo()
        {
            if (_fkpConfiguration != null)
                return _fkpConfiguration.GetCompanyInfo();
            if (_fkfConfiguration != null)
                return _fkfConfiguration.GetCompanyInfo();

            throw new FKIntegrationException("Pobranie repozytorium firmy poprzedzone musi być skonfigurowaniem biblioteki");
        }

        public static bool LogUser()
        {
            if (_fkpConfiguration != null)
                return _fkpConfiguration.LogUser();
            if (_fkfConfiguration != null)
                return _fkfConfiguration.LogUser();

            throw new FKIntegrationException("Zalogowanie użytkownika poprzedzone musi być skonfigurowaniem biblioteki");
        }

        public delegate void InTransactionDelegate();
        public static void InTransaction(InTransactionDelegate inTransactionDelegate)
        {
            FKPDatabase database = _fkpConfiguration.GetDatabase();

            try
            {
                SetConfigurationAsInTransaction(true);
                database.Open();
                database.BeginTransaction();
                inTransactionDelegate();
                database.CommitTransaction();
                SetConfigurationAsInTransaction(false);
            }
            catch (Exception)
            {
                database.RollbackTransaction();
                throw;
            }
            finally
            {
                database.Dispose();
            }
        }

        private static void SetConfigurationAsInTransaction(bool isInTransaction)
        {
            if (_fkpConfiguration != null)
            {
                _fkpConfiguration.IsInTrasaction(isInTransaction);
                return;
            }
            if (_fkfConfiguration != null)
            {
                _fkfConfiguration.IsInTransaction(isInTransaction);
                return;
            }

            throw new FKIntegrationException("Rozpoczęcie transakcji poprzedzone musi być skonfigurowaniem biblioteki");
        }

        internal static FKPDatabase GetFKPDatabase()
        {
            if (_fkpConfiguration != null)
                return _fkpConfiguration.GetDatabase();
            throw new FKIntegrationException("Biblioteka nie została skonfigurowana dla FKP, nie można pobrać konfiguracji");
        }

        public static void SelectBookingYear(YearInfo selectedYearInfo)
        {
            _selectedYearInfo = selectedYearInfo;
        }

        private static YearInfo _selectedYearInfo;
        internal static YearInfo SelectedYearInfo
        {
            get { return _selectedYearInfo; }
        }
    }
}
