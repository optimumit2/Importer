using System.Windows;

namespace WpfModern
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //Without the next line your app would've ended upon closing Login window:
            ShutdownMode = ShutdownMode.OnExplicitShutdown;
            //Authenticate user (if canceled returns 'false')
            LoginWindow wndLogin = new LoginWindow();
            if (wndLogin.ShowDialog() == false)
            {
                Shutdown();
            }
            else
            {
                //if you have some cache to load, then show some progress dialog,
                //or welcome screen, or whatever...
                //after this, the MainWindow executes, so restore the ShutdownMode,
                //so the app ends with closing of main window (otherwise, you have to call
                //Applicaiton.Current.Shutdown(); explicitly in Closed event of MainWindow)
                ShutdownMode = ShutdownMode.OnMainWindowClose;
            }
        }
    }    
}
