using System.Collections.Generic;
using FKIntegration.Documents;
using MXDokFK;

namespace FKIntegration.Repositories.FKP
{
    internal class DocumentDefinitionRepository : IDocumentDefinitionRepository
    {
        private readonly FKPDatabase _fkDatabase;

        public DocumentDefinitionRepository(FKPDatabase fkDatabase)
        {
            _fkDatabase = fkDatabase;
        }

        //these methods are year info aware
        //TODO: - zaimplementuj zeby po zalogowaniu do obiektu integracji ustawiany byl ostatni otwarty rok
        //TODO: i zeby wszystkie repozytoria ktorych dotyczy rok korzytaly z tego
        public List<DocumentDefinition> GetDocumentDefinitionsByDocumentType<T>() where T : Document
        {
            var documentDefinitions = new List<DocumentDefinition>();
            using (_fkDatabase.Open())
            {
                var defDokumClass = new DefDokumClass();
                defDokumClass.Open(FKIntegrationManager.SelectedYearInfo.YearId, _fkDatabase.BtDatabase);
                defDokumClass.MoveFirst();

                do
                {
                    if ((System.Int16)defDokumClass["dWzor"] == GetDocumentDefinitionIdByPatternId<T>())
                        documentDefinitions.Add(new DocumentDefinition(defDokumClass));
                }
                while (defDokumClass.MoveNext() != 0);
            }
            return documentDefinitions;
        }



        private static System.Int16 GetDocumentDefinitionIdByPatternId<T>() where T : Document
        {
            if (typeof(T) == typeof(BankStatement))
                return (int)DocumentDefinitionType.WB;
            
            if(typeof(T) == typeof(SimpleDocument))
                return (int) DocumentDefinitionType.DP;

            return 0;
        }

        public DocumentDefinition GetDocumentDefinitionByDocumentShortCut(string shortCut)
        {
            return null;
        }

        public List<DocumentDefinition> GetDocumentDefinitions()
        {
            return null;
        }

    }
}