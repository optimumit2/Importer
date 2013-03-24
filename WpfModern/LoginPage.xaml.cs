using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfModern
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(btnOk).DialogResult = true;

            //var window = GetWindow();
            //if (window == null)
            //    return;
            //window.DialogResult = true;
        }

        //private Window GetWindow()
        //{
        //    DependencyObject controlParent = this.Parent;
        //    while (controlParent != null)
        //    {
        //        Window window = this.Parent as Window;
        //        if (window != null)
        //            return window;
        //        controlParent = controlParent.
        //    }

        //    return null;
        //}
    }
}
