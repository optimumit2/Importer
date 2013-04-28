using System;
using FKIntegration.Repositories.FKP;

namespace FKIntegration.CardInexes
{
    public class BankAccount
    {
        internal string Bank { get; set; }
        internal string AccountNo { get; set; }

        internal BankAccount(IItgCommon itgCommon)
        {
            Bank = (string)itgCommon["bank"];
            AccountNo = (string)itgCommon["nrRachunku"];
        }

        public BankAccount()
        {
            
        }
    }

    public class DepartmentAddress
    {
        internal string Street { get; set; }
        internal string HouseNo { get; set; }
        internal string City { get; set; }
        internal string PostalCode { get; set; }

        internal DepartmentAddress(IItgCommon itgCommon)
        {
            Street = (string)itgCommon["Ulica"];
            HouseNo = (string)itgCommon["nrDomu"];
            City = (string)itgCommon["miejscowosc"];
            PostalCode = (string)itgCommon["kodPocztowy"];
        }

        public DepartmentAddress()
        {
            
        }
    }

    public class Department
    {
        internal int Id { get; set; }
        public string Shortcut { get; set; }
        public string Name { get; set; }
        public DepartmentAddress DepartmentAddress { get; set; }
        public BankAccount BankAccount { get; set; }
        public int Position { get; set; }
        public bool Active { get; set; }
        public string Mark { get; set; }

        internal Department(IItgCommon itgDepartment)
        {
            FillDepartmentBody(itgDepartment);
        }

        internal Department(int position)
        {
            Position = position;
        }

        internal Department()
        {
            DepartmentAddress = new DepartmentAddress();
            BankAccount =new BankAccount();
        }

        private void FillDepartmentBody(IItgCommon itgDepartment)
        {
            Id  = (int)itgDepartment["Id"];
            Shortcut  = (string)itgDepartment["Skrot"];
            Name  = (string)itgDepartment["Nazwa"];

            DepartmentAddress = new DepartmentAddress(itgDepartment);
            BankAccount = new BankAccount(itgDepartment);
            
            Position  = (int)itgDepartment["Pozycja"];
            Active  = Convert.ToBoolean(itgDepartment["aktywny"]);
            Mark  = (string)itgDepartment["znacznik"];
        }

        //internal void Save(SyncroData itgDepartment)
        //{
        //    FillSyncroBody(itgDepartment);
        //    itgDepartment.Save();
        //}

        internal void FillSyncroBody(IItgCommon itgDepartment)
        {
            itgDepartment["Id"] = Id;
            itgDepartment["Skrot"] = Shortcut;
            itgDepartment["Nazwa"] = Name;

            itgDepartment["Ulica"] = DepartmentAddress.Street;
            itgDepartment["nrDomu"] = DepartmentAddress.HouseNo;
            itgDepartment["miejscowosc"] = DepartmentAddress.City;
            itgDepartment["kodPocztowy"] = DepartmentAddress.PostalCode;

            itgDepartment["bank"] = BankAccount.Bank;
            itgDepartment["nrRachunku"] = BankAccount.AccountNo;

            itgDepartment["Pozycja"] = Position;
            itgDepartment["aktywny"] = Active;
            itgDepartment["znacznik"] = Mark;
        }
    }
}