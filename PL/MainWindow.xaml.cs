using PL.Task;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Static instance of BlApi.IBl.
        /// </summary>
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        /// <summary>
        /// Gets or sets the current time.
        /// </summary>
        public DateTime MyTime
        {
            get { return (DateTime)GetValue(MyTimeProperty); }
            set { SetValue(MyTimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        /// <summary>
        /// Dependency property for MyTime.
        /// </summary>
        public static readonly DependencyProperty MyTimeProperty =
            DependencyProperty.Register("MyTime", typeof(DateTime), typeof(MainWindow), new PropertyMetadata(null));

        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            MyTime = s_bl.CurrentClock;
            DataContext = this;
        }

        /// <summary>
        /// Event handler for the Manager button click.
        /// </summary>
        private void Btn_Manager(object sender, RoutedEventArgs e)
        {
            new ManagerWindow().Show();
        }

        /// <summary>
        /// Event handler for the Engineer button click.
        /// </summary>
        private void Btn_Engineer(object sender, RoutedEventArgs e)
        {
            new GetIdWindow().Show();
        }

        /// <summary>
        /// Event handler for the Add Day button click.
        /// </summary>
        private void Button_Click_AddDay(object sender, RoutedEventArgs e)
        {
            s_bl.PromoteDay();
            MyTime = s_bl.CurrentClock;
        }

        /// <summary>
        /// Event handler for the Add Hour button click.
        /// </summary>
        private void Button_Click_AddHour(object sender, RoutedEventArgs e)
        {
            s_bl.PromoteHour();
            MyTime = s_bl.CurrentClock;
        }

        /// <summary>
        /// Event handler for the Reset Time button click.
        /// </summary>
        private void Button_Click_ResetTime(object sender, RoutedEventArgs e)
        {
            s_bl.ResetTime();
            MyTime = s_bl.CurrentClock;
        }
    }
}
