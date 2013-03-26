using System;

namespace WpfModern.Configuration
{
    public class ApplicationConfiguration : NotifyPropertyChanged
    {
        private string _fkDatabasePath;
        public string FKDatabasePath
        {
            get { return _fkDatabasePath; }
            set
            {
                if (!String.Equals(_fkDatabasePath, value, StringComparison.CurrentCulture))
                {
                    _fkDatabasePath = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set
            {
                if (!String.Equals(_userName, value, StringComparison.CurrentCulture))
                {
                    _userName = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _supplierAccount;
        public string SupplierAccount
        {
            get { return _supplierAccount; }
            set
            {
                if (!String.Equals(_supplierAccount, value, StringComparison.CurrentCulture))
                {
                    _supplierAccount = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _recipientAccount;
        public string RecipientAccount
        {
            get { return _recipientAccount; }
            set
            {
                if (!String.Equals(_recipientAccount, value, StringComparison.CurrentCulture))
                {
                    _recipientAccount = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}