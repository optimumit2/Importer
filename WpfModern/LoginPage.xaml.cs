using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using MS.Internal.Xml.XPath;
using WpfModern.Configuration;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace WpfModern
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private ApplicationConfiguration _applicationConfiguration;

        public LoginPage()
        {
            InitializeComponent();
            Loaded += LoginPage_Loaded;
        }

        void LoginPage_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = _applicationConfiguration = ConfigurationProvider.GetConfiguration();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (TryToLoginToFK())
            {
                var window = Window.GetWindow(btnOk);

                if (window != null)
                    window.DialogResult = true;
            }

            ConfigurationProvider.Save(_applicationConfiguration);
        }

        private bool TryToLoginToFK()
        {
            return true;
        }

        private void BtnSelectDatabasePath_OnClick(object sender, RoutedEventArgs e)
        {
            var folderBrowserDialog = new FolderBrowserDialog();            

            var dialogResult = folderBrowserDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                _applicationConfiguration.FKDatabasePath = folderBrowserDialog.SelectedPath;
            }
        }
    }
}
