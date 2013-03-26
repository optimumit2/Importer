using System.Configuration;

namespace WpfModern.Configuration
{
    public static class ConfigurationProvider
    {
        private const string ApplicationConfiguration = "ApplicationConfiguration";

        public static ApplicationConfiguration GetConfiguration()
        {
            var configuration =
                ConfigurationManager
                    .GetSection(ApplicationConfiguration)
                as ApplicationConfigurationSection;

            if (configuration != null)
                return configuration.GetApplicationConfiguration();

            return new ApplicationConfigurationSection().GetApplicationConfiguration();
        }

        public static void Save(ApplicationConfiguration applicationConfiguration)
        {
            var config =
                ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var section = (ApplicationConfigurationSection)config.Sections[ApplicationConfiguration];
            section.ApplyChangesFrom(applicationConfiguration);

            config.Save(ConfigurationSaveMode.Modified);

            ConfigurationManager.RefreshSection(ApplicationConfiguration);
        }
    }
}