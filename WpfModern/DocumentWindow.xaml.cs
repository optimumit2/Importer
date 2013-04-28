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
using FirstFloor.ModernUI.Windows.Controls;

namespace WpfModern
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
            DataContext = new ThisDataContext();                        
        }
    }

    public class ThisDataContext
    {
        public ThisDataContext()
        {
            CurrentDate = DateTime.Now;
            DocumentNumber = "Doc124";
        }

        public DateTime CurrentDate { get; set; }
        public string DocumentNumber { get; set; }
    }
}
