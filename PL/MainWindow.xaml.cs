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
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public DateTime MyTime
        {
            get { return (DateTime)GetValue(MyTimeProperty); }
            set { SetValue(MyTimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyTimeProperty =
            DependencyProperty.Register("MyTime", typeof(DateTime), typeof(MainWindow), new PropertyMetadata(null));

        public MainWindow()
        {
            InitializeComponent();
            MyTime = s_bl.CurrentClock;
            DataContext = this;
        }

        private void Btn_Manager(object sender, RoutedEventArgs e)
        {
            new ManagerWindow().Show();
        }

        private void Btn_Engineer(object sender, RoutedEventArgs e)
        {
            new GetIdWindow().Show();
        }       

        private void Button_Click_AddDay(object sender, RoutedEventArgs e)
        {
            s_bl.PromoteDay();
            MyTime = s_bl.CurrentClock;
        }

        private void Button_Click_AddHour(object sender, RoutedEventArgs e)
        {
            s_bl.PromoteHour();
            MyTime = s_bl.CurrentClock;
        }

        private void Button_Click_ResetTime(object sender, RoutedEventArgs e)
        {
            s_bl.ResetTime();
            MyTime = s_bl.CurrentClock;
        }
    }
}
