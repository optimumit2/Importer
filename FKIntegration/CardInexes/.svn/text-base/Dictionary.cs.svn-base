using System.Collections.Generic;
using FKIntegration.Repositories.FKP;

namespace FKIntegration.CardInexes
{
    public class Dictionary
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<DictionaryItem> Items { get; set; }

        internal Dictionary(IItgCommon syncroData)
        {
            FillDictionaryBody(syncroData);
        }

        private void FillDictionaryBody(IItgCommon syncroData)
        {
            Id = (int)syncroData["id"];
            Name = (string) syncroData["nazwa"];
            Description = (string) syncroData["opis"];
        }

        internal void FillSyncroBody(IItgCommon itgCommon)
        {
            itgCommon["id"] = Id;
            itgCommon["nazwa"] = Name;
            itgCommon["opis"] = Description;
        }
    }

    public class DictionaryItem
    {
        internal DictionaryItem(IItgCommon syncroData)
        {
            FillDictionaryBody(syncroData);
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        private void FillDictionaryBody(IItgCommon syncroData)
        {
            Id = (int)syncroData["id"];
            Name = (string)syncroData["nazwa"];
            Description = (string)syncroData["opis"];
        }

        internal void FillSyncroBody(IItgCommon itgCommon)
        {
            itgCommon["id"] = Id;
            itgCommon["nazwa"] = Name;
            itgCommon["opis"] = Description;
        }
    }
}