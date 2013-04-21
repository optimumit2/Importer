using MXDokFK;

namespace FKIntegration.Repositories.FKP
{
    internal interface IItgCommon
    {
        object this[string index] { get; set; }
        void GetByPosition(int position);
    }

    internal class SyncroData:IItgCommon
    {
        readonly FKPDatabase _fkpDatabase;
        private readonly SyncroSubjectClass _syncroSubject = new SyncroSubjectClass();

        public SyncroData(FKPDatabase fkpDatabase)
        {
            _fkpDatabase = fkpDatabase;
        }

        //internal void Open(__MIDL___MIDL_itf_MxDokFK_0277_0001 type)
        internal void Open(SubjectType type)
        {            
            Open((int)type,0);
        }

        internal void Open(int pType, object bDictElem)
        {
            _syncroSubject.Open(_fkpDatabase.BtDatabase, pType, bDictElem);
        }        

        public object this[string index]
        {
            get { return _syncroSubject.get_Value(index); }
            set { _syncroSubject.set_Value(index, value); }
        }

        public void GetByPosition(int position)
        {
            _syncroSubject.GetRecByPoz(position);
        }

        public void GetById(int id)
        {
            _syncroSubject.GetRecById(id);
        }

        internal void MoveFirst()
        {
            _syncroSubject.GetFirstRec();
            for (short i = 0; i < _syncroSubject.FieldsCount - 1; i++)
            {
                System.Diagnostics.Debug.Print(_syncroSubject.get_FieldName(i));
            }
        }

        internal bool MoveNext()
        {
            return _syncroSubject.GetNext() == 0;
        }

        internal void Save()
        {
            if ((int)_syncroSubject.get_Value("Id") == 0)
            {
                short ret = _syncroSubject.Insert(0);
            }
            else
                _syncroSubject.Update(0);
        }
    }
}