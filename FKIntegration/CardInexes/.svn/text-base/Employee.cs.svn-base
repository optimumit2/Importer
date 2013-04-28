using System;
using FKIntegration.Repositories.FKP;

namespace FKIntegration.CardInexes
{
    public class Employee
    {
        public int Id { get; set; }
        public string Shortcut { get; set; }
        public string FirstName1 { get; set; }
        public string FirstName2 { get; set; }
        public string LastName { get; set; }
        public string Pesel { get; set; }
        public string Nip { get; set; }
        public int OfficeId { get; set; }
        public string Street { get; set; }
        public string HouseNo { get; set; }
        public string FlatNo { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Phone1 { get; set; }
        public string Bank { get; set; }
        public string AccountNo { get; set; }
        public string CreateUserName { get; set; }
        public DateTime CreateDate { get; set; }
        public string UpdateUserName { get; set; }
        public DateTime UpdateDate { get; set; }
        public int Position { get; set; }
        public bool Active { get; set; }

        internal Employee(IItgCommon syncroData)
        {
            FillEmployerBody(syncroData);
        }

        internal Employee()
        {
            Active = true;
        }

        private void FillEmployerBody(IItgCommon syncroData)
        {
            Id = (int)syncroData["Id"];
            Shortcut = (string)syncroData["Skrot"];
            FirstName1 = (string)syncroData["imie1"];
            FirstName2 = (string)syncroData["imie2"];
            LastName = (string)syncroData["nazwisko"];
            Pesel = (string)syncroData["pesel"];
            Nip = (string)syncroData["nip"];
            OfficeId = (int)syncroData["urzadId"];
            Street = (string)syncroData["Ulica"];
            HouseNo = (string)syncroData["nrDomu"];
            FlatNo = (string)syncroData["nrMieszkania"];
            City = (string)syncroData["miejscowosc"];
            PostalCode = (string)syncroData["kodPocztowy"];
            Phone1 = (string)syncroData["telefon1"];
            Bank = (string)syncroData["bank"];
            AccountNo = (string)syncroData["nrRachunku"];
            CreateUserName = (string)syncroData["wprowadził"];
            CreateDate = (DateTime)syncroData["datawpr"];
            UpdateUserName = (string)syncroData["zmodyfikował"];
            UpdateDate = (DateTime)syncroData["datamod"];
            Position = (int)syncroData["pozycja"];
            Active = Convert.ToBoolean(syncroData["aktywny"]);
        }

        //internal void Save(SyncroData itgEmployee)
        //{
        //    FillSyncroBody(itgEmployee);
        //    itgEmployee.Save();
        //}

        internal void FillSyncroBody(IItgCommon itgEmployee)
        {
            itgEmployee["Id"] = Id;
            itgEmployee["Skrot"]=Shortcut;
            itgEmployee["imie1"]=FirstName1;
            itgEmployee["imie2"]=FirstName2;
            itgEmployee["nazwisko"]=LastName;
            itgEmployee["pesel"]=Pesel;
            itgEmployee["nip"]=Nip;
            itgEmployee["urzadId"]=OfficeId;
            itgEmployee["Ulica"]=Street;
            itgEmployee["nrDomu"]=HouseNo;
            itgEmployee["nrMieszkania"]=FlatNo;
            itgEmployee["miejscowosc"]=City;
            itgEmployee["kodPocztowy"]=PostalCode;
            itgEmployee["telefon1"]=Phone1;
            itgEmployee["bank"]=Bank;
            itgEmployee["nrRachunku"]=AccountNo;
            itgEmployee["wprowadził"]=CreateUserName;
            itgEmployee["datawpr"]=CreateDate;
            itgEmployee["zmodyfikował"]=UpdateUserName;
            itgEmployee["datamod"]=UpdateDate;
            itgEmployee["pozycja"]=Position;
            itgEmployee["aktywny"] = Active;
        }
    }
}