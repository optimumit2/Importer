using MXDokFK;

namespace FKIntegration.Documents
{
    public enum DocumentType
    {
        DEX, DIM, DP, FKS, FKZ, FVS, FVZ, FWN, PWN, RK, WB, WDT, WNT
    }

    internal enum DocumentDefinitionType
    {
        DEX = 8,
        DIM = 9,
        DP = 1,
        F = 20,
        FKS = 11,
        FKZ = 10,
        FVS = 3,
        FVZ = 2,
        FWN = 0x10,
        PWN = 0x13,
        RK = 6,
        RKS = 13,
        RKZ = 12,
        RUS = 5,
        RUZ = 4,
        RZL = 15,
        UNKNOWN = 0x3e8,
        WB = 14,
        WBStary = 7,
        WDT = 0x12,
        WNT = 0x11
    }

    public enum DocumentDefinitionTypeExt
    {
        SimpleDocument = 1,
        PurchaseInvoice = 2,
        SalesInvoice = 3,

    }

    public enum DocumentFormType
    {

    }

    public class DocumentDefinition
    {
        public DocumentDefinition(DefDokumClass defDokumClass)
        {
            FillDocumentDefinition(defDokumClass);
        }

        private void FillDocumentDefinition(DefDokumClass defDokumClass)
        {
            Id = (int) defDokumClass["Id"];
            ShortCut = (string) defDokumClass["dSkrot"];
            Name = (string) defDokumClass["dNazwa"];
            DefaultVatRegistryId = (System.Int16)defDokumClass["rejestr"];
            FormId = (System.Int16)defDokumClass["dWzor"];
            IsActive = (System.Int16)defDokumClass["dAktywny"] == 1;
            YearInfo = FKIntegrationManager.SelectedYearInfo;
        }

        public string ShortCut { get; private set; }
        public string Name { get; private set; }
        public int DefaultVatRegistryId { get; private set; }
        public int Id { get; private set; }
        public int FormId { get; private set; }
        public bool IsActive { get; private set; }
        public YearInfo YearInfo { get; private set; }
    }
}