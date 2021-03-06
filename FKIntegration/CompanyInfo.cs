using System;
using System.Collections.Generic;
using MXDokFK;

namespace FKIntegration
{
    public class CompanyInfo
    {
        private readonly FKPDatabase _fkpDatabase;

        internal CompanyInfo(FKPDatabase fkpDatabase)
        {
            FirmaInfo firmaInfo = fkpDatabase.BtDatabase.FirmaInfo;
            string someName = firmaInfo.Nazwa;
            string someshortName = firmaInfo.NazwaSkrocona;
            string someNip = firmaInfo.NIP;
            string someRegon = firmaInfo.Regon;
            string someCity = firmaInfo.Miejscowosc;

            _fkpDatabase = fkpDatabase;
            _createDate = _fkpDatabase.BtDatabase.FirmaInfo.CreateDate;
            _name = _fkpDatabase.BtDatabase.FirmaInfo.Nazwa;
            _shortName = _fkpDatabase.BtDatabase.FirmaInfo.NazwaSkrocona;
            _nip = new Nip(_fkpDatabase.BtDatabase.FirmaInfo.NIP);
            _regon = new Regon(_fkpDatabase.BtDatabase.FirmaInfo.Regon);            

            _address = new Address(_fkpDatabase.BtDatabase.FirmaInfo);
            _allowedPropertyStructureItems = new List<PropertyStructureItem>(11)
                                                 {
                                                     new PropertyStructureItem(0, "Spółka z.o.o"),
                                                     new PropertyStructureItem(1, "Spółka cywilna i jednoosobowa"),
                                                     new PropertyStructureItem(2, "Spółka komandytowa"),
                                                     new PropertyStructureItem(3, "Spółka akcyjna"),
                                                     new PropertyStructureItem(4, "Przedsiębiorstwo państwowe"),
                                                     new PropertyStructureItem(5, "Spółdzielnia"),
                                                     new PropertyStructureItem(6, "Fundacja"),
                                                     new PropertyStructureItem(7, "Jednostka budżetowa"),
                                                     new PropertyStructureItem(8, "Spółka jawna"),
                                                     new PropertyStructureItem(9, "Spółka partnerska"),
                                                     new PropertyStructureItem(10, "Spółka komandytowo-akcyjna")
                                                 };
            SetPropertyStructureByStructureId(_fkpDatabase.BtDatabase.FirmaInfo.Wlasnosc);
            _propertyType = new PropertyType(_fkpDatabase.BtDatabase.FirmaInfo);
            _telephoneNo1 = _fkpDatabase.BtDatabase.FirmaInfo.get_Telefon(TelefonType.Telefon1);
            _telephoneNo2 = _fkpDatabase.BtDatabase.FirmaInfo.get_Telefon(TelefonType.Telefon2);
            _faxNo1 = _fkpDatabase.BtDatabase.FirmaInfo.get_Telefon(TelefonType.Fax1);
            _faxNo2 = _fkpDatabase.BtDatabase.FirmaInfo.get_Telefon(TelefonType.Fax2);
        }

        public List<YearInfo> GetYearInfoList()
        {
            if (_fkpDatabase.IsOpen())
                return GetYearInfoList(_fkpDatabase);

            using (_fkpDatabase.Open())
            {
                return GetYearInfoList(_fkpDatabase);
            }
        }

        private static List<YearInfo> GetYearInfoList(FKPDatabase fkpDatabase)
        {
            var yearInfoList = new List<YearInfo>(fkpDatabase.BtDatabase.FirmaInfo.RokCount);
            for (short i = 0; i < fkpDatabase.BtDatabase.FirmaInfo.RokCount; i++)
            {
                var yearInfo = new YearInfo(fkpDatabase.BtDatabase.FirmaInfo.get_Rok(i));
                yearInfoList.Add(yearInfo);
            }
            return yearInfoList;
        }

        public void SetYear(YearInfo yearInfo)
        {
            if (_fkpDatabase.IsOpen())
                _fkpDatabase.BtDatabase.FirmaInfo.SetRok((short)yearInfo.YearId); ;

            using (_fkpDatabase.Open())
            {
                _fkpDatabase.BtDatabase.FirmaInfo.SetRok((short)yearInfo.YearId);
            }
        }

        private readonly DateTime _createDate;
        public DateTime CreateDate
        {
            get { return _createDate; }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private readonly string _shortName;
        public string ShortName
        {
            get { return _shortName; }
        }

        private Nip _nip;
        public Nip Nip
        {
            get { return _nip; }
            set { _nip = value; }
        }

        private Regon _regon;
        public Regon Regon
        {
            get { return _regon; }
            set { _regon = value; }
        }

        private Address _address;
        public Address Address
        {
            get { return _address; }
        }

        private PropertyStructureItem _propertyStructure;
        public PropertyStructureItem PropertyStructure
        {
            get { return _propertyStructure; }
            set { SetPropertyStructureByStructureId(value.Id); }
        }

        private void SetPropertyStructureByStructureId(int id)
        {
            PropertyStructureItem structureItem = _allowedPropertyStructureItems.Find(item => item.Id == id);
            if (structureItem == null)
                throw new FKIntegrationException("Ustawiana struktura własności jest spoza listy dozwolonych struktur własności");
            _propertyStructure = structureItem;
        }

        private readonly List<PropertyStructureItem> _allowedPropertyStructureItems;
        public List<PropertyStructureItem> AllowedPropertyStructureItems
        {
            get { return _allowedPropertyStructureItems; }
        }

        private readonly PropertyType _propertyType;
        public PropertyType PropertyType
        {
            get { return _propertyType; }
        }

        private string _telephoneNo1;
        public string TelephoneNo1
        {
            get { return _telephoneNo1; }
            set { _telephoneNo1 = value; }
        }

        private string _telephoneNo2;
        public string TelephoneNo2
        {
            get { return _telephoneNo2; }
            set { _telephoneNo2 = value; }
        }

        private string _faxNo1;
        public string FaxNo1
        {
            get { return _faxNo1; }
            set { _faxNo1 = value; }
        }

        private string _faxNo2;
        public string FaxNo2
        {
            get { return _faxNo2; }
            set { _faxNo2 = value; }
        }

        //private bool _hasChanged;
        //TODO: Ta metoda powinna byc publiczna... przemysla z database czy upublicznic...
        internal void AttachDatabase(FKPDatabase database)
        {
            _fkpDatabase.BtDatabase.FirmaInfo.Attach(database.BtDatabase);            
        }

        public void Update()
        {
            _fkpDatabase.BtDatabase.FirmaInfo.Nazwa = _name;
            _fkpDatabase.BtDatabase.FirmaInfo.NIP = _nip.Value;
            _fkpDatabase.BtDatabase.FirmaInfo.Regon = _regon.Value;
            _address.Fill(_fkpDatabase.BtDatabase.FirmaInfo);
            _propertyStructure.Fill(_fkpDatabase.BtDatabase.FirmaInfo);
            _propertyType.Fill(_fkpDatabase.BtDatabase.FirmaInfo);
            _fkpDatabase.BtDatabase.FirmaInfo.set_Telefon(TelefonType.Telefon1, _telephoneNo1);
            _fkpDatabase.BtDatabase.FirmaInfo.set_Telefon(TelefonType.Telefon2, _telephoneNo2);
            _fkpDatabase.BtDatabase.FirmaInfo.set_Telefon(TelefonType.Fax1, _faxNo1);
            _fkpDatabase.BtDatabase.FirmaInfo.set_Telefon(TelefonType.Fax2, _faxNo2);

            _fkpDatabase.BtDatabase.FirmaInfo.Update();
        }
    }

    public class PropertyStructureItem
    {
        private readonly short _id;
        private readonly string _name;

        internal PropertyStructureItem(short id, string name)
        {
            _id = id;
            _name = name;
        }

        internal short Id
        {
            get { return _id; }
        }

        public string Name
        {
            get { return _name; }
        }

        public void Fill(FirmaInfo firmaInfo)
        {
            firmaInfo.Wlasnosc = _id;
        }
    }
}