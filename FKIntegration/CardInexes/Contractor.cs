using System;
using FKIntegration.Repositories.FKP;

namespace FKIntegration.CardInexes
{
    public class Contact
    {
        internal Contact(IItgCommon common)
        {
            FirstName = (string)common["imie"];
            LastName = (string)common["nazwisko"];
            Phone1 = (string)common["telefon1"];
            Phone2 = (string)common["telefon2"];
            Telex = (string)common["telex"];
            Fax = (string)common["fax"];
            Email = (string)common["email"];
            Www = (string)common["www"];
        }

        public Contact()
        {
                
        }

        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Telex { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Www { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class ContractorAddress
    {
        internal ContractorAddress(IItgCommon common)
        {
            Country = (string)common["kraj"];
            CountryCode = (string)common["kodKraju"];
            City = (string)common["miejscowosc"];
            Street = (string)common["Ulica"];
            HouseNo = (string)common["nrDomu"];
            FlatNo = (string)common["nrMieszkania"];
            PostalCode = (string)common["kodPocztowy"];
            Region = (string)common["rejon"];
        }

        internal ContractorAddress()
        {
                
        }

        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNo { get; set; }
        public string FlatNo { get; set; }
        public string PostalCode { get; set; }
        public string Region { get; set; }
    }

    //TODO: zrefaktorować to na listę List<BankAccount> - niepotrzebne dublowanie funkcjonalnosci z BankAccount
    public class BankAccounts
    {
        internal BankAccounts(IItgCommon common)
        {
            Bank = (string)common["bank"];
            AccountNo = (string)common["nrRachunku"];
            Bank2 = (string)common["bank2"];
            AccountNo2 = (string)common["nrRachunku2"];
        }

        public BankAccounts()
        {
            
        }

        public string Bank { get; set; }
        public string AccountNo { get; set; }
        public string Bank2 { get; set; }
        public string AccountNo2 { get; set; }
    }

    public class Contractor
    {
        public int Id { get; set; }        
        public string Shortcut { get; set; }
        public string Name { get; set; }
        public Nip Nip { get; set; }
        public string Pesel { get; set; }
        public string Regon { get; set; }
        public bool StatusUE { get; set; }

        public Contact Contact { get; set; }
        public ContractorAddress Address { get; set; }
        public BankAccounts BankAccounts { get; set; }

        public string CreateUserName { get; set; }
        public string UpdateUserName { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string Kind { get; set; }
        public string Catalog { get; set; }
        public double CreditLimit { get; set; }
        public bool ActiveCredit { get; set; }
        public string CreditCurrency { get; set; }
        public string ClientCode { get; set; }
        public int Position { get; private set; }
        public bool Active { get; set; }
        public string Marker { get; set; }
        public bool Approved { get; set; }
        public string Guid { get; set; }


        internal Contractor(IItgCommon syncroData)
        {
            FillContractorBody(syncroData);
        }

        internal Contractor(int position)
        {
            Position = position;
        }

        public Contractor()
        {
            Address = new ContractorAddress();
            Contact = new Contact();
            BankAccounts = new BankAccounts();
            Nip = new Nip(null);
            Active = true;
        }

        private void FillContractorBody(IItgCommon syncroData)
        {
            Shortcut = (string)syncroData["Skrot"];
            Name = (string)syncroData["Nazwa"];
            Nip = new Nip((string)syncroData["nip"]);
            Pesel = (string)syncroData["pesel"];
            Regon = (string)syncroData["regon"];
            StatusUE = Convert.ToBoolean(syncroData["statusUE"]);

            Contact = new Contact(syncroData);
            Address = new ContractorAddress(syncroData);
            BankAccounts = new BankAccounts(syncroData);

            CreateUserName = (string)syncroData["wprowadził"];
            UpdateUserName = (string)syncroData["zmodyfikował"];
            CreateDate = (DateTime)syncroData["datawpr"];
            UpdateDate = (DateTime)syncroData["datamod"];
            Kind = (string)syncroData["rodzaj"];
            Catalog = (string)syncroData["katalog"];
            CreditLimit = (double)syncroData["limitKredytu"];
            ActiveCredit = Convert.ToBoolean(syncroData["aktywnyKredyt"]);
            CreditCurrency = (string)syncroData["walutaKredytu"];
            ClientCode = (string)syncroData["kodKlienta"];
            Position = (int)syncroData["pozycja"];
            Active = Convert.ToBoolean(syncroData["aktywny"]);
            Marker = (string)syncroData["znacznik"];
            Approved = Convert.ToBoolean(syncroData["zatwierdzony"]);
        }

        internal void FillSyncroBody(IItgCommon syncroData)
        {
            syncroData["Skrot"] = Shortcut;
            syncroData["Nazwa"] = Name;
            syncroData["nip"]=Nip;
            syncroData["pesel"] = Pesel;
            syncroData["regon"] = Regon;
            syncroData["statusUE"] = StatusUE;

            syncroData["imie"] = Contact.FirstName;
            syncroData["nazwisko"] = Contact.LastName;
            syncroData["telefon1"] = Contact.Phone1;
            syncroData["telefon2"] = Contact.Phone2;
            syncroData["telex"] = Contact.Telex;
            syncroData["fax"] = Contact.Fax;
            syncroData["email"] = Contact.Email;
            syncroData["www"] = Contact.Www;

            syncroData["kraj"] = Address.Country;
            syncroData["kodKraju"] = Address.CountryCode;
            syncroData["miejscowosc"] = Address.City;
            syncroData["Ulica"] = Address.Street;
            syncroData["nrDomu"] = Address.HouseNo;
            syncroData["nrMieszkania"] = Address.FlatNo;
            syncroData["kodPocztowy"] = Address.PostalCode;
            syncroData["rejon"] = Address.Region;

            syncroData["bank"]= BankAccounts.Bank;
            syncroData["nrRachunku"] = BankAccounts.AccountNo;
            syncroData["bank2"] = BankAccounts.Bank2;
            syncroData["nrRachunku2"] = BankAccounts.AccountNo2;

            syncroData["wprowadził"] = CreateUserName;
            syncroData["zmodyfikował"] = UpdateUserName;
            syncroData["datawpr"] = CreateDate;
            syncroData["datamod"] = UpdateDate;
            syncroData["rodzaj"] = Kind;
            syncroData["katalog"] = Catalog;
            syncroData["limitKredytu"] = CreditLimit;
            syncroData["aktywnyKredyt"] = ActiveCredit;
            syncroData["walutaKredytu"] = CreditCurrency;
            syncroData["kodKlienta"] = ClientCode;
            syncroData["pozycja"] = Position;
            syncroData["aktywny"] = Active;
            syncroData["znacznik"] = Marker;
            syncroData["zatwierdzony"] = Approved;
        }
    }
}