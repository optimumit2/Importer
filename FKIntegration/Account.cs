using System.Collections.Generic;
using FKIntegration.CardInexes;
using MXDokFK;

namespace FKIntegration
{
    public enum BalanceAccountType
    {
        Balance,
        OutBalance,
        Result
    }

    public enum ClearanceAccountType
    {
        Clearance,
        Special,
        Custom,
        None
    }

    public enum ClearanceType
    {
        Warn,
        Require,
        Ignore
    }

    public abstract class Account
    {
        public int Id { get; private set; }
        public int Level { get; private set; }
        public string Number { get; private set; }
        public string Name { get; set; }
        public string Shortcut { get; set; }
        //public bool LongTerm { get; set; }
        //public bool ZeroBalanceControl { get; set; }
        //public bool IsCurrency { get; set; }
        //public bool IsVatSettlementAccount { get; set; }
        //public ClearanceType ClearanceType { get; private set; }
        public BalanceAccountType BalanceAccountType { get; private set; }
        public ClearanceAccountType ClearanceAccountType { get; private set; }

        public List<Account> SubAccounts { get; private set; }

        private readonly string _fullNumber;

        public string FullNumber
        {            
            get { return !IsCardIndex ? _fullNumber : _fullNumberWithoutLastLevel + "-" + GetLastLevelNumber(); }
        }
        
        private readonly string _fullNumberWithoutLastLevel;
        internal Account(IPlanKont planKont, int level)
        {
            Id = (int)planKont["id"];
            Level = level;
            _fullNumber = GetFullNumber(planKont, Level);
            _fullNumberWithoutLastLevel = GetFullNumberWithoutLastLevel(planKont, level);            
            Number = GetNumber(planKont, Level);
            Name = planKont["nazwa"].ToString();
            Shortcut = planKont["skrot"].ToString();
            BalanceAccountType = GetBalanceAccountType(planKont);
            ClearanceAccountType = GetClearanceAccountType(planKont);

            SubAccounts = new List<Account>();


            //string flags = planKont["flags"].ToString();
            //string skrot = planKont["skrot"].ToString();
            //string typ = planKont["typ"].ToString();
            //string podtyp = planKont["podtyp"].ToString();
            //string SubjectType = planKont["SubjectType"].ToString();
            //string SubjectCode = planKont["SubjectCode"].ToString();
        }

        private ClearanceAccountType GetClearanceAccountType(IPlanKont planKont)
        {
            var subType = (short)planKont["podtyp"];
            switch (BalanceAccountType)
            {
                case BalanceAccountType.OutBalance:
                    return ClearanceAccountType.None;
                case BalanceAccountType.Balance:
                    {
                        if (subType == 0)
                            return ClearanceAccountType.Custom;
                        if (subType == 1)
                            return ClearanceAccountType.Clearance;
                        if (subType == 2)
                            return ClearanceAccountType.Special;
                        throw new FKIntegrationException("Ten podtyp konta nie jest obsługiwany: " + subType);
                    }
                case BalanceAccountType.Result:
                    {
                        if (subType == 0)
                            return ClearanceAccountType.Custom;
                        if (subType == 2)
                            return ClearanceAccountType.Special;
                        throw new FKIntegrationException("Ten podtyp konta nie jest obsługiwany: " + subType);
                    }
                default: throw new FKIntegrationException("Ten typ konta nie jest obsługiwany przy tworzeniu podtypu konta: " + BalanceAccountType);
            }
        }

        private static BalanceAccountType GetBalanceAccountType(IPlanKont planKont)
        {
            var type = (short)planKont["typ"];
            if (type == 0)
                return BalanceAccountType.OutBalance;
            if (type == 1)
                return BalanceAccountType.Balance;
            if (type == 2)
                return BalanceAccountType.Result;
            throw new FKIntegrationException("Ten typ konta nie jest obsługiwany: " + type);
        }

        private string GetFullNumber(IPlanKont planKont, int level)
        {
            string fullNumber = GetSyntheticNumber(planKont);
            if (level > 0)
            {
                for (int i = 1; i < level; i++)
                {
                    fullNumber += "-" + planKont["wart" + i];
                }
                fullNumber += "-" + GetLastLevelNumber(planKont);
            }
            return fullNumber;
        }

        private static string GetFullNumberWithoutLastLevel(IPlanKont planKont, int level)
        {
            string fullNumber = GetSyntheticNumber(planKont);
            if (level > 0)
            {
                for (int i = 1; i < level; i++)
                {
                    fullNumber += "-" + planKont["wart" + i];
                }                
            }
            return fullNumber;
        }

        protected string GetLastLevelNumber(IPlanKont planKont)
        {
            if (Level > 0)
                return planKont["wart" + Level].ToString();
            return string.Empty;
        }

        protected abstract string GetLastLevelNumber();

        private static string GetNumber(IPlanKont planKont, int level)
        {
            if (level == 0)
                return GetSyntheticNumber(planKont);
            return planKont["wart" + level].ToString();
        }

        private static string GetSyntheticNumber(IPlanKont planKont)
        {
            var syntet = (short)planKont["syntet"];
            return string.Format("{0:000}", syntet);
        }

        internal void AddSubAccount(Account subAccount)
        {
            Account parentAccount = FindParrentAccountFor(subAccount);
            parentAccount.SubAccounts.Add(subAccount);
        }

        private Account FindParrentAccountFor(Account account)
        {
            if (!account._fullNumber.Contains(_fullNumber))
                throw new FKIntegrationException(string.Format("Konto o numerze {0} nie należy do konta o numerze {1}", account._fullNumber, _fullNumber));

            Account accountWithSubaccounts = this;

            string[] numbers = account._fullNumber.Split('-');
            for (int i = 1; i < numbers.Length - 1; i++)
            {
                int index0 = i;
                Account foundAccount = accountWithSubaccounts.SubAccounts.Find(accountInLoop => accountInLoop.Number == numbers[index0]);
                if (foundAccount != null)
                    accountWithSubaccounts = foundAccount;
            }

            return accountWithSubaccounts;
        }

        public virtual bool IsSynthetic
        {
            get { return false; }
        }

        public abstract string Type { get; }

        public virtual bool IsCardIndex
        {
            get { return false; }
        }

        public virtual bool IsEmployeeAccount
        {
            get { return false; }
        }

        public virtual bool IsContractorsAccount
        {
            get { return false; }
        }

        public virtual bool IsBankingAccountAccount
        {
            get { return false; }
        }

        public virtual bool IsDepartmentsAccount
        {
            get { return false; }
        }

        public virtual bool IsDictionaryAccount
        {
            get { return false; }
        }

        public virtual void SetEmployee(Employee employee)
        {
            throw new FKIntegrationException("Ta metoda musi być override'owana w klasie dziedziczącej");
        }

        public virtual void SetContractor(Contractor contractor)
        {
            throw new FKIntegrationException("Ta metoda musi być override'owana w klasie dziedziczącej");
        }

        public virtual void SetDepartment(Department department)
        {
            throw new FKIntegrationException("Ta metoda musi być override'owana w klasie dziedziczącej");
        }

        public virtual void SetBankingAccount(BankingAccount bankAccounts)
        {
            throw new FKIntegrationException("Ta metoda musi być override'owana w klasie dziedziczącej");
        }
    }
}