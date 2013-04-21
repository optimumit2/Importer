using FKIntegration.Repositories.FKP;

namespace FKIntegration
{
    public class Currency
    {
        public int Id { get; set; }
        public string Shortcut { get; set; }

        internal Currency()
        {
        }

        internal Currency(IItgCommon syncroData)
        {
            FillCurrencyBody(syncroData);
        }

        private void FillCurrencyBody(IItgCommon syncroData)
        {
            Id = (int)syncroData["id"];
            Shortcut = (string) syncroData["skrot"];
        }

        internal void FillSyncroBody(IItgCommon itgCommon)
        {
            itgCommon["id"] = Id;
            itgCommon["skrot"] = Shortcut;
        }
    }
}
