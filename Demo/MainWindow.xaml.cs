using RDF.PersianNumericConverter;
using System.ComponentModel;
using System.Windows;

namespace Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new ViewModel();
        }
    }

    public class ViewModel : INotifyPropertyChanged
    {
        NumericParser parser = new NumericParser();

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private decimal? _Number;
        private string _NumberText;

        private string _Text;
        private decimal? _TextNumber;

        public decimal? Number
        {
            get { return _Number; }
            set
            {
                this._Number = value;
                OnPropertyChanged("Number");

                if (Number.HasValue)
                    NumberText = parser.Convert(Number.Value);
                else
                    NumberText = null; 
            }
        }

        public string NumberText
        {
            get { return _NumberText; }

            set
            {
                _NumberText = value;
                OnPropertyChanged("NumberText");
            }
        }

        public string Text
        {
            get { return _Text; }

            set
            {
                _Text = value;
                OnPropertyChanged("Text");


                if (!string.IsNullOrEmpty(Text))
                    TextNumber = parser.Parse(Text);
                else
                    TextNumber = null;
            }
        }

        public decimal? TextNumber
        {
            get { return _TextNumber; }

            set
            {
                _TextNumber = value;
                OnPropertyChanged("TextNumber");
            }
        }
    }
}
