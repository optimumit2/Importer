using System.Collections.Generic;
using FKIntegration.CardInexes;
using MXDokFK;

namespace FKIntegration.Repositories.FKP
{
    internal class DictionaryRepository : CardIndexBaseRepository<Dictionary>, IDictionaryRepository
    {
        public DictionaryRepository(FKPDatabase fkpDatabase)
            : base(fkpDatabase, SubjectType.SUB_SLOWNIK)
        { }        
        internal override void FillSyncroBody(IItgCommon itgCommon, Dictionary entity)
        {
            entity.FillSyncroBody(itgCommon);
            //MXDokFK.SubjectType.SUB_SLOWNIK
        }

        internal override Dictionary Get(SyncroData syncroData)
        {
            return new Dictionary(syncroData);
        }

        public Dictionary GetById(Dictionary dictionary)
        {
            return GetById(dictionary.Id);
        }

        public List<string> GetAllDictionaryNames()
        {
            var dictionaries = base.GetAll();
            return dictionaries.ConvertAll(dictionary => dictionary.Name);
        }

        public override List<Dictionary> GetAll()
        {
            var dictionaries = base.GetAll();
            foreach (var eachDictionary in dictionaries)
            {
                eachDictionary.Items = GetItems(eachDictionary);
            }
            return dictionaries;
        }

        private List<DictionaryItem> GetItems(Dictionary dictionary)
        {
            using (_fkDatabase.Open())
            {
                var list = new List<DictionaryItem>();
                var syncroData = new SyncroData(_fkDatabase);
                syncroData.Open(dictionary.Id,1);

                syncroData.MoveFirst();
                do
                {
                    list.Add(new DictionaryItem(syncroData));
                } while (syncroData.MoveNext());
                return list;
            }
        }
    }
}