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
            ThisDataContext dataContext = new ThisDataContext();
            DataContext = dataContext;
            //GridContractorsFK.ItemsSource = ((ThisDataContext)DataContext).contractorsFK;

            //GridContractorsFK.DataContext = dataContext.contractorsFK;
        }
    }

    public class ThisDataContext
    {
        public ThisDataContext()
        {
            CurrentDate = DateTime.Now;
            DocumentNumber = "Doc124";

            contractorsFK =new ObservableCollection<Contractor>(FKIntegration.FKIntegrationManager.ContractorsRepository.GetAll());

            //contractorsFK.Add(new Contractor() { Name = "test", Nip = new FKIntegration.Nip("1231231212") });
        }

        public DateTime CurrentDate { get; set; }
        public string DocumentNumber { get; set; }

        private ObservableCollection<Contractor> contractorsFK;

        public ObservableCollection<Contractor> ContractorsFK
        {
            get { return contractorsFK; }
            set { contractorsFK = value; }
        }


        //public ObservableCollection<Contractor> contractorsFK = new ObservableCollection<Contractor>();

       
    }
}
