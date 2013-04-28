using FKIntegration.Repositories.FKP;

namespace FKIntegration.CardInexes
{
    public class BankingAccount
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Bank { get; set; }
        public string Account { get; set; }
        public string City { get; set; }

        internal BankingAccount(IItgCommon itgCompanyAccount)
        {
            FillCompanyAccountBody(itgCompanyAccount);
        }

        internal BankingAccount()
        {
        }

        private void FillCompanyAccountBody(IItgCommon itgCompanyAccount)
        {
            Id = (int)itgCompanyAccount["id"];
            Name = (string)itgCompanyAccount["nazwa"];
            Bank = (string)itgCompanyAccount["bank"];
            Account = (string)itgCompanyAccount["nrRachunku"];
            City = (string)itgCompanyAccount["miejscowosc"];
        }

        //internal void Save(SyncroData itgCompanyAccount)
        //{
        //    FillSyncroBody(itgCompanyAccount);
        //    itgCompanyAccount.Save();
        //}

        internal void FillSyncroBody(IItgCommon itgCompanyAccount)
        {
            itgCompanyAccount["id"] = Id;
            itgCompanyAccount["nazwa"] = Name;
            itgCompanyAccount["bank"] = Bank;
            itgCompanyAccount["nrRachunku"] = Account;
            itgCompanyAccount["miejscowosc"] = City;
        }
    }
}