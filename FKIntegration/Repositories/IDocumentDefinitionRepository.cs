using System.Collections.Generic;
using FKIntegration.Documents;

namespace FKIntegration.Repositories
{
    public interface IDocumentDefinitionRepository
    {
        List<DocumentDefinition> GetDocumentDefinitionsByDocumentType<T>() where T : Document;
        DocumentDefinition GetDocumentDefinitionByDocumentShortCut(string shortCut);        
        List<DocumentDefinition> GetDocumentDefinitions();          
    }
}