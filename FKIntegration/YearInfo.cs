using System;
using MXDokFK;

namespace FKIntegration
{
    public class YearInfo
    {
        private readonly RokInfo _info;

        public YearInfo(RokInfo info)
        {
            _info = info;
        }

        //public int Archivized
        //{
        //    get { return _info.WArchiwum; }
        //}

        public int YearLength
        {
            get { return _info.DlugoscRoku; }
        }

        public string Folder
        {
            get { return _info.Katalog; }
        }

        public DateTime StartDate
        {
            get { return _info.PoczatekRoku; }
        }

        public DateTime EndDate
        {
            get { return _info.KoniecRoku; }
        }

        public int YearId
        {
            get { return _info.RokId; }
        }

        public bool Closed
        {
            get { return _info.Zamkniety == 1; }
        }

        public string ClosedDescription
        {
            get { return Closed ? "nieaktywny" : "aktywny"; }
        }

        public void Attach()
        {
            _info.Attach(null, 0);
            throw new NotImplementedException();
        }
    }
}