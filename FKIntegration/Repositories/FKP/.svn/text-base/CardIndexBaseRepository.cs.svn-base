using System;
using System.Collections.Generic;
using MXDokFK;

namespace FKIntegration.Repositories.FKP
{
    internal abstract class CardIndexBaseRepository<T>
    {
        protected readonly FKPDatabase _fkDatabase;
        private readonly SubjectType _syncroType;

        protected CardIndexBaseRepository(FKPDatabase fkDatabase, SubjectType syncroType)
        {
            _fkDatabase = fkDatabase;
            _syncroType = syncroType;
        }

        public virtual List<T> GetAll()
        {
            using (_fkDatabase.Open())
            {
                var list = new List<T>();
                var syncroData = new SyncroData(_fkDatabase);
                syncroData.Open(_syncroType);

                syncroData.MoveFirst();
                do
                {
                    list.Add(Get(syncroData));
                } while (syncroData.MoveNext());
                return list;
            }
        }

        protected virtual T GetByPosition(int position)
        {
            using (_fkDatabase.Open())
            {
                var syncroData = new SyncroData(_fkDatabase);
                syncroData.Open(_syncroType);
                syncroData.GetByPosition(position);
                return Get(syncroData);
            }
        }

        protected virtual T GetById(int id)
        {
            using (_fkDatabase.Open())
            {
                var syncroData = new SyncroData(_fkDatabase);
                syncroData.Open(_syncroType);
                syncroData.GetById(id);
                return Get(syncroData);
            }
        }

        public virtual T Find(Predicate<T> condition)
        {
            using (_fkDatabase.Open())
            {
                var syncroData = new SyncroData(_fkDatabase);
                syncroData.Open(_syncroType);

                syncroData.MoveFirst();
                do
                {
                    T entity = Get(syncroData);
                    if (condition(entity))
                        return entity;
                } while (syncroData.MoveNext());
            }
            return default(T);
        }

        public virtual void Save(T entity)
        {
            if (_isInTransaction)
            {
                Save(_fkDatabase, entity);
                return;
            }

            using (_fkDatabase.Open())
            {
                Save(_fkDatabase, entity);
            }
        }

        private void Save(FKPDatabase fkpDatabase, T entity)
        {
            var syncroData = new SyncroData(fkpDatabase);
            syncroData.Open(_syncroType);
            FillSyncroBody(syncroData, entity);
            syncroData.Save();
        }

        internal abstract void FillSyncroBody(IItgCommon itgCommon, T entity);

        internal abstract T Get(SyncroData syncroData);        

        private bool _isInTransaction;
        internal void IsInTransaction(bool isInTransaction)
        {
            _isInTransaction = isInTransaction;
        }
    }
}