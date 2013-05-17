using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using FKIntegration.CardInexes;
using FKIntegration.Documents;

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

            ContractorsFK =new ObservableCollection<Contractor>(FKIntegration.FKIntegrationManager.ContractorsRepository.GetAll());

            Document = new BankStatement("WB");
            Document.AddDebitPosition("opis pozycji", 100, "100", 20, "200");

            Document.Save();
        }

        public DateTime CurrentDate { get; set; }
        public string DocumentNumber { get; set; }

        public ObservableCollection<Contractor> ContractorsFK { get; set; }

        public BankStatement Document { get; set; }

    }
}
