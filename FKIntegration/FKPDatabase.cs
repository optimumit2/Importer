using System;
using MXDokFK;

namespace FKIntegration
{
    internal interface IFKDatabase : IDisposable
    {
        string Path { get; }
        //void Open();
        FKPDatabase Open();
        bool IsOpen();
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
        DateTime CurrentDate { get; set; }
        string Version { get; }
        FKErrorMode ErrorMode { get; set; }
        //CompanyInfo FKCompanyInfo { get; }
    }

    public class Nip
    {
        private readonly string _value;

        public Nip(string value)
        {
            _value = value;
        }

        public string Value
        {
            get { return _value; }
        }
    }

    public class Regon
    {
        private readonly string _value;

        public Regon(string value)
        {
            _value = value;
        }

        public string Value
        {
            get { return _value; }
        }
    }

    internal class FKPDatabase : IFKDatabase
    {
        //private BtDatabase _btDatabase = new BtDatabase();
        private BtDatabase _btDatabase;

        public BtDatabase BtDatabase
        {
            get { return _btDatabase; }
        }

        private readonly FKPUser _fkpUser;

        private readonly string _path;
        public string Path { get { return _path; } }

        public FKPDatabase(FKPUser fkpUser, string path)
        {
            if (fkpUser == null)
                throw new ArgumentNullException("fkpUser");
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException("path");
            if (!fkpUser.IsValid())
                throw new FKIntegrationException(fkpUser.GetValidationMessage());

            _path = path;
            _fkpUser = fkpUser;            
        }

        public FKPDatabase Open()
        {
            _btDatabase = new BtDatabase();
            _btDatabase.Open(_path, _fkpUser.UserName, _fkpUser.Password);
            return this;
        }

        public bool IsOpen()
        {
            return _btDatabase.IsOpen() == 1;
        }

        public void BeginTransaction()
        {
            if (!IsOpen())
                throw new FKIntegrationException("");
            _btDatabase.BeginTransaction();
        }

        public void CommitTransaction()
        {
            if (!IsOpen())
                throw new FKIntegrationException("");
            _btDatabase.EndTransaction();
        }

        public void RollbackTransaction()
        {
            if (!IsOpen())
                throw new FKIntegrationException("");
            _btDatabase.AbortTransaction();
        }

        public DateTime CurrentDate
        {
            get { return _btDatabase.CurrentDate; }
            set { _btDatabase.CurrentDate = value; }
        }

        public string Version
        {
            get { return _btDatabase.Version; }
        }

        public FKErrorMode ErrorMode
        {
            get
            {
                switch (_btDatabase.ErrorMode)
                {
                    case ErrorModeType.showErrorrsMode:
                        return FKErrorMode.ShowErrors;
                    case ErrorModeType.silentMode:
                        return FKErrorMode.Silent;
                    default:
                        throw new FKIntegrationException(string.Format("Ten tryb pracy z polaczeniem do bazy danych nie jest obslugiwany, tryb = {0}", ErrorModeType.useParentSettings));
                }
            }
            set
            {
                switch (value)
                {
                    case FKErrorMode.ShowErrors:
                        _btDatabase.ErrorMode = ErrorModeType.showErrorrsMode;
                        break;
                    case FKErrorMode.Silent:
                        _btDatabase.ErrorMode = ErrorModeType.silentMode;
                        break;
                    default:
                        throw new FKIntegrationException(string.Format("Ten tryb pracy z polaczeniem do bazy danych nie jest obslugiwany, tryb = {0}", value));
                }
            }
        }

        //public CompanyInfo FKCompanyInfo
        //{
        //    get { return new CompanyInfo(_btDatabase.FirmaInfo); }
        //}

        public void Dispose()
        {
            Dispose(true);
            //GC.SuppressFinalize(this);
            GC.SuppressFinalize(_btDatabase);
        }

        ~FKPDatabase()
        {
            Dispose(false);
        }

        bool _isDisposed;
        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (_btDatabase != null)
                {
                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(_btDatabase);
                }
                _isDisposed = true;
            }
        }
    }

    public enum FKErrorMode
    {
        Silent, ShowErrors
    }
}
