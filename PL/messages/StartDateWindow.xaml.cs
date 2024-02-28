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
using System.Windows.Shapes;

namespace PL.messages
{
    /// <summary>
    /// Interaction logic for StartDateWindow.xaml
    /// </summary>
    public partial class StartDateWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public DateTime StartDate
        {
            get { return (DateTime)GetValue(startdateProperty); }
            set { SetValue(startdateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for startdate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty startdateProperty =
            DependencyProperty.Register("startdate", typeof(DateTime), typeof(StartDateWindow), new PropertyMetadata(null));


        public StartDateWindow()
        {
            StartDate = DateTime.Now;
            InitializeComponent();
            
        }

        private void Btn_set(object sender, RoutedEventArgs e)
        {
            s_bl.Clock.SetStartDate(StartDate);
            this.Close();
            foreach (var item in s_bl.Task.ReadAll())
            {
                s_bl.Task.SetScheduele(item);
            }
            MessageBox.Show("Dates assaigned succusfully", "message", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
