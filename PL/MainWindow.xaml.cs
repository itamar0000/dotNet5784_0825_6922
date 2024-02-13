using PL.Engineer;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void btnEngineers_Click(object sender, RoutedEventArgs e)
        {
            new EngineerListWindow().Show();
        }

        private void btnInit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mbResult  =  MessageBox.Show("Are you sure you want to iniitalize the data?", "Initalize", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
            if (mbResult == MessageBoxResult.Yes)
            {
                DalTest.Initialization.Do();
            }
        }

    }
}