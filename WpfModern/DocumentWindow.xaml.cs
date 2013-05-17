using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

        private void btnUseSelectedContractor_Click(object sender, RoutedEventArgs e)
        {
            var thisDataContext = DataContext as ThisDataContext;
            if (thisDataContext == null)
                return;
            if (thisDataContext.SelectedContractor == null)
                return;

            if (thisDataContext.SelectedPosition == null || string.IsNullOrEmpty(thisDataContext.SelectedPosition.DebitAccount))
                return;

            thisDataContext.SelectedPosition.CreditAccount = thisDataContext.SelectedPosition.CreditAccount.Replace("K", thisDataContext.SelectedContractor.Position.ToString());

            //foreach (var eachPosition in thisDataContext.Document.PositionList)
            //{
            //    //eachPosition.CreditAccount.Replace
            //}
        }
    }

    public class ThisDataContext
    {
        public ThisDataContext()
        {
            CurrentDate = DateTime.Now;
            DocumentNumber = "Doc124";

            ContractorsFK = new ObservableCollection<Contractor>(FKIntegration.FKIntegrationManager.ContractorsRepository.GetAll());

            var document = new BankStatement("WB");
            var position = document.AddDebitPosition("opis pozycji", 100, "100", 20, "200-K");

            Document = new BankStatementViewModel(document);
            //Document.Save();
        }

        public DateTime CurrentDate { get; set; }
        public string DocumentNumber { get; set; }

        public ObservableCollection<Contractor> ContractorsFK { get; set; }

        public Contractor SelectedContractor { get; set; }

        //public BankStatement Document { get; set; }

        //public Position SelectedPosition { get; set; }


        public BankStatementViewModel Document { get; set; }

        public BankStatementPositionViewModel SelectedPosition { get; set; }
    }

    public class BankStatementViewModel : INotifyPropertyChanged
    {
        private BankStatement _bankStatement;
        public BankStatementViewModel(BankStatement bankStatement)
        {
            if (bankStatement == null)
                throw new ArgumentNullException("bankStatement");

            _bankStatement = bankStatement;

            PositionList = GetPositionList();
        }

        public DateTime EntryDate
        {
            get { return _bankStatement.EntryDate; }
        }

        public string Number
        {
            get { return _bankStatement.Number; }
            set 
            {
                if (_bankStatement.Number != value)
                {
                    _bankStatement.Number = value;
                    OnPropertyChanged("Number");
                }                
            }
        }

        public string ShortCut
        {
            get { return _bankStatement.ShortCut; }
            //set { _shortCut = value; }
        }

        public string Content
        {
            get { return _bankStatement.Content; }
            set 
            {
                if (_bankStatement.Content != value)
                {
                    _bankStatement.Content = value;
                    OnPropertyChanged("Content");
                }   
            }
        }

        public DocumentMark DocumentMark
        {
            get { return _bankStatement.DocumentMark; }
            set 
            {
                if (_bankStatement.DocumentMark != value)
                {
                    _bankStatement.DocumentMark = value;
                    OnPropertyChanged("DocumentMark");
                }   
            }
        }

        public DateTime DocumentDate
        {
            get { return _bankStatement.DocumentDate; }
            set 
            {
                if (_bankStatement.DocumentDate != value)
                {
                    _bankStatement.DocumentDate = value;
                    OnPropertyChanged("DocumentDate");
                }   
            }
        }

        private ObservableCollection<BankStatementPositionViewModel> GetPositionList()
        {
            ObservableCollection<BankStatementPositionViewModel> positionList = new ObservableCollection<BankStatementPositionViewModel>();

            foreach (var eachPosition in _bankStatement.PositionList)
            {
                var position = new BankStatementPositionViewModel(eachPosition);
                positionList.Add(position);
            }
            
            return positionList;
        }

        public ObservableCollection<BankStatementPositionViewModel> PositionList { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class BankStatementPositionViewModel : INotifyPropertyChanged
    {
        private Position _position;
        public BankStatementPositionViewModel(Position position)
        {
            if (position == null)
                throw new ArgumentNullException("position");

            _position = position;
        }

        public int Number
        {
            get { return _position.Number; }
        }

        public virtual decimal DebitAmount
        {
            get { return _position.DebitAmount; }
            set
            {
                if (_position.DebitAmount != value)
                {
                    _position.DebitAmount = value;
                    OnPropertyChanged("DebitAmount");
                }
            }
        }

        public virtual decimal CreditAmount
        {
            get { return _position.CreditAmount; }
            set
            {
                if (_position.CreditAmount != value)
                {
                    _position.CreditAmount = value;
                    OnPropertyChanged("CreditAmount");
                }
            }
        }

        public virtual string DebitDescription
        {
            get { return _position.DebitDescription; }
            set
            {
                if (_position.DebitDescription != value)
                {
                    _position.DebitDescription = value;
                    OnPropertyChanged("DebitDescription");
                }
            }
        }

        public virtual string DebitAccount
        {
            get { return _position.DebitAccount; }
            set
            {
                if (_position.DebitAccount != value)
                {
                    _position.DebitAccount = value;
                    OnPropertyChanged("DebitAccount");
                }
            }
        }

        public virtual string CreditAccount
        {
            get { return _position.CreditAccount; }
            set
            {
                if (_position.CreditAccount != value)
                {
                    _position.CreditAccount = value;
                    OnPropertyChanged("CreditAccount");
                }
            }
        }

        public virtual string CreditDescription
        {
            get { return _position.CreditDescription; }
            set
            {
                if (_position.CreditDescription != value)
                {
                    _position.CreditDescription = value;
                    OnPropertyChanged("CreditDescription");
                }
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
