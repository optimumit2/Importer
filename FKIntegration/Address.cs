using MXDokFK;

namespace FKIntegration
{
    public class Address
    {
        public Address(FirmaInfo firmaInfo)
        {
            _streetNumber = firmaInfo.Dom;
            _zipCode = firmaInfo.KodPocztowy;
            _post = firmaInfo.Poczta;
            _localNumber = firmaInfo.Lokal;
            _city = firmaInfo.Miejscowosc;
            _street = firmaInfo.Ulica;
            _province = firmaInfo.Wojewodztwo;
        }

        private string _streetNumber;
        public string StreetNumber
        {
            get { return _streetNumber; }
            set { _streetNumber = value; }
        }

        private string _zipCode;
        public string ZipCode
        {
            get { return _zipCode; }
            set { _zipCode = value; }
        }

        private string _post;
        public string Post
        {
            get { return _post; }
            set { _post = value; }
        }

        private string _localNumber;
        public string LocalNumber
        {
            get { return _localNumber; }
            set { _localNumber = value; }
        }

        private string _city;
        public string City
        {
            get { return _city; }
            set { _city = value; }
        }

        private string _street;
        public string Street
        {
            get { return _street; }
            set { _street = value; }
        }

        private string _province;
        public string Province
        {
            get { return _province; }
            set { _province = value; }
        }

        internal void Fill(FirmaInfo firmaInfo)
        {
            firmaInfo.Dom = _streetNumber;
            firmaInfo.KodPocztowy = _zipCode;
            firmaInfo.Poczta = _post;
            firmaInfo.Lokal = _localNumber;
            firmaInfo.Miejscowosc = _city;
            firmaInfo.Ulica = _street;
            firmaInfo.Wojewodztwo = _province;
        }
    }
}