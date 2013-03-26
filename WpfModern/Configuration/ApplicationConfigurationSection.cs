using System.Configuration;

namespace WpfModern.Configuration
{
    public class ApplicationConfigurationSection : ConfigurationSection
    {
        private const string FKDatabasePathPropertyName = "FKDatabasePath";
        private const string UserNamePropertyName = "UserName";
        private const string SupplierAccountPropertyName = "SupplierAccount";
        private const string RecipientAccountPropertyName = "RecipientAccount";

        [ConfigurationProperty(FKDatabasePathPropertyName)]
        public string FKDatabasePath
        {
            get { return (string)this[FKDatabasePathPropertyName]; }
            set { this[FKDatabasePathPropertyName] = value; }
        }

        [ConfigurationProperty(UserNamePropertyName)]
        public string UserName
        {
            get { return (string)this[UserNamePropertyName]; }
            set { this[UserNamePropertyName] = value; }
        }

        [ConfigurationProperty(SupplierAccountPropertyName)]
        public string SupplierAccount
        {
            get { return (string)this[SupplierAccountPropertyName]; }
            set { this[SupplierAccountPropertyName] = value; }
        }

        [ConfigurationProperty(RecipientAccountPropertyName)]
        public string RecipientAccount
        {
            get { return (string)this[RecipientAccountPropertyName]; }
            set { this[RecipientAccountPropertyName] = value; }
        }

        public ApplicationConfiguration GetApplicationConfiguration()
        {
            return new ApplicationConfiguration
                {
                    FKDatabasePath = FKDatabasePath,
                    RecipientAccount = RecipientAccount,
                    UserName = UserName,
                    SupplierAccount = SupplierAccount
                };
        }

        public void ApplyChangesFrom(ApplicationConfiguration applicationConfiguration)
        {
            FKDatabasePath = applicationConfiguration.FKDatabasePath;
            RecipientAccount = applicationConfiguration.RecipientAccount;
            UserName = applicationConfiguration.UserName;
            SupplierAccount = applicationConfiguration.SupplierAccount;
        }
    }
}