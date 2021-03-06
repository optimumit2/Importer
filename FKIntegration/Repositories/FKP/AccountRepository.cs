using System;
using System.Collections.Generic;
using FKIntegration.CardInexes;
using MXDokFK;

namespace FKIntegration.Repositories.FKP
{
    internal class AccountRepository : IAccountRepository
    {
        private readonly FKPDatabase _fkpDatabase;

        public AccountRepository(FKPDatabase fkpDatabase)
        {
            _fkpDatabase = fkpDatabase;
        }

        private int _year;
        internal int Year
        {
            set { _year = value; }
        }

        public List<Account> GetAll()
        {
            var allAccounts = new List<Account>();
            using (_fkpDatabase.Open())
            {
                var planKont = new PlanKont();
                ///WARNING: Hardcoded 0
                //planKont.Open(0, _fkpDatabase.BtDatabase);
                planKont.Open(_year, _fkpDatabase.BtDatabase);
                planKont.MoveFirst();
                do
                {
                    Account account = CreateAccount(planKont);
                    allAccounts.Add(account);

                } while (planKont.MoveNext() != 0);
            }

            List<Account> groupedAccounts = GroupAccounts(allAccounts);

            return groupedAccounts;
        }

        private static List<Account> GroupAccounts(IEnumerable<Account> accounts)
        {
            var groupedAccounts = new List<Account>();
            Account syntheticAccount = null;
            foreach (var eachAccount in accounts)
            {
                if (eachAccount.IsSynthetic)
                {
                    syntheticAccount = eachAccount;
                    groupedAccounts.Add(eachAccount);
                }
                    //else
                    //groupedAccounts[groupedAccounts.Count - 1].AddSubAccount(eachAccount);
                else
                    syntheticAccount.AddSubAccount(eachAccount);
            }
            return groupedAccounts;
        }

        private static Account CreateAccount(IPlanKont planKont)
        {
            AccountPositionType accountPositionType = GetCurrentAccountType(planKont);
            int level = GetLevel(planKont);
            switch (accountPositionType)
            {
                case AccountPositionType.None:
                    return new SyntheticAccount(planKont, level);
                case AccountPositionType.Fixed:
                    return new AnalitycalAccount(planKont, level);
                case AccountPositionType.Employees:
                    return new EmployeesAccount(planKont, level);
                case AccountPositionType.Contractors:
                    return new ContractorsAccount(planKont, level);
                case AccountPositionType.BankAccounts:
                    return new BankingAccountAccount(planKont, level);
                case AccountPositionType.Offices:
                    return new DepartmentsAccount(planKont, level);
                case AccountPositionType.CustomDictionary:
                    return new DictionaryAccount(planKont, level);
                default:
                    throw new FKIntegrationException(string.Format("Ten typ konta = {0} nie jest obsługiwany w planie kont", accountPositionType));
            }
        }

        private static int GetLevel(IPlanKont planKont)
        {
            for (int i = 5; i > 0; i--)
            {
                if (GetTypeByPosition(planKont, i) != AccountPositionType.None)
                    return i;
            }
            return 0;
        }

        private static AccountPositionType GetCurrentAccountType(IPlanKont planKont)
        {
            for (int i = 5; i > 0; i--)
            {
                if (GetTypeByPosition(planKont, i) != AccountPositionType.None)
                    return GetTypeByPosition(planKont, i);
            }
            return AccountPositionType.None;
        }

        private static AccountPositionType GetTypeByPosition(IPlanKont planKont, int position)
        {
            var type = (short)planKont["typ" + position];
            return (AccountPositionType)type;
        }

        public List<string> GetAllNumbers()
        {
            throw new NotImplementedException();
        }
    }

    internal class SyntheticAccount : Account
    {
        internal SyntheticAccount(IPlanKont planKont, int level)
            : base(planKont, level)
        { }

        protected override string GetLastLevelNumber()
        {
            throw new FKIntegrationException("Ta metoda nie powinna nigdy zostać wywołana");
        }

        public override bool IsSynthetic
        {
            get { return true; }
        }

        public override string Type
        {
            get { return "Syntetyczne"; }
        }
    }

    internal class AnalitycalAccount : Account
    {
        internal AnalitycalAccount(IPlanKont planKont, int level)
            : base(planKont, level)
        {
        }

        protected override string GetLastLevelNumber()
        {
            throw new FKIntegrationException("Ta metoda nie powinna być nigdy wywołana");
        }

        public override string Type
        {
            get { return "Analityczne " + TranslateLevel(Level); }
        }

        public string TranslateLevel(int level)
        {
            switch (level)
            {
                case 1:
                    return "I";
                case 2:
                    return "II";
                case 3:
                    return "III";
                case 4:
                    return "IV";
                case 5:
                    return "V";
                default:
                    throw new FKIntegrationException("");
            }
        }
    }

    internal class EmployeesAccount : Account
    {
        internal EmployeesAccount(IPlanKont planKont, int level)
            : base(planKont, level)
        {
        }

        protected override string GetLastLevelNumber()
        {
            if (_employee != null)
                return _employee.Position.ToString();
            return "x";
        }

        public override string Type
        {
            get { return "Pracownicy"; }
        }

        public override bool IsCardIndex
        {
            get { return true; }
        }

        public override bool IsEmployeeAccount
        {
            get { return true; }
        }

        private Employee _employee;
        public override void SetEmployee(Employee employee)
        {
            _employee = employee;

        }
    }

    internal class ContractorsAccount : Account
    {
        internal ContractorsAccount(IPlanKont planKont, int level)
            : base(planKont, level)
        {
        }

        protected override string GetLastLevelNumber()
        {
            if (_contractor != null)
                return _contractor.Position.ToString();
            return "x";
        }

        public override string Type
        {
            get { return "Kontrahenci"; }
        }

        public override bool IsCardIndex
        {
            get { return true; }
        }

        public override bool IsContractorsAccount
        {
            get { return true; }
        }

        private Contractor _contractor;
        public override void SetContractor(Contractor contractor)
        {
            _contractor = contractor;
        }
    }

    internal class BankingAccountAccount : Account
    {
        internal BankingAccountAccount(IPlanKont planKont, int level)
            : base(planKont, level)
        {
        }

        //TODO: sprawdz czy tu w bankingAccount nie powinien byc zwracany nr pozycji zamiast id !!
        protected override string GetLastLevelNumber()
        {
            if (_bankAccounts != null)
                return _bankAccounts.Id.ToString();
            return "x";
        }

        public override string Type
        {
            get { return "Rachunki"; }
        }

        public override bool IsCardIndex
        {
            get { return true; }
        }

        public override bool IsBankingAccountAccount
        {
            get { return true; }
        }

        private BankingAccount _bankAccounts;
        public override void SetBankingAccount(BankingAccount bankAccounts)
        {
            _bankAccounts = bankAccounts;
        }
    }

    internal class DepartmentsAccount : Account
    {
        internal DepartmentsAccount(IPlanKont planKont, int level)
            : base(planKont, level)
        {
        }

        protected override string GetLastLevelNumber()
        {
            if (_department != null)
                return _department.Position.ToString();          
            return "x";
        }

        public override string Type
        {
            get { return "Urzędy"; }
        }

        public override bool IsCardIndex
        {
            get { return true; }
        }

        public override bool IsDepartmentsAccount
        {
            get { return true; }
        }

        private Department _department;
        public override void SetDepartment(Department department)
        {
            _department = department;
        }
    }

    internal class DictionaryAccount : Account
    {
        internal DictionaryAccount(IPlanKont planKont, int level)
            : base(planKont, level)
        {
        }

        protected override string GetLastLevelNumber()
        {
            return "x";
        }

        public override string Type
        {
            get { return "Słowniki"; }
        }

        public override bool IsCardIndex
        {
            get { return true; }
        }

        public override bool IsDictionaryAccount
        {
            get { return true; }
        }
    }

    internal enum AccountPositionType
    {
        Fixed = -1,
        None = 0,
        Employees = 1,
        Contractors = 2,
        BankAccounts = 3,
        Offices = 4,
        CustomDictionary = 5
    }
}