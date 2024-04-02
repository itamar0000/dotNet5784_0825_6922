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
        // Static reference to Business Logic layer
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        // Dependency property for StartDate
        public DateTime StartDate
        {
            get { return (DateTime)GetValue(startdateProperty); }
            set { SetValue(startdateProperty, value); }
        }

        // Dependency property definition for StartDate
        public static readonly DependencyProperty startdateProperty =
            DependencyProperty.Register("startdate", typeof(DateTime), typeof(StartDateWindow), new PropertyMetadata(null));


        // Constructor for StartDateWindow
        public StartDateWindow()
        {
            StartDate = DateTime.Now; // Initialize StartDate with current date
            InitializeComponent();
        }

        // Event handler for clicking the Set button
        private void Btn_set(object sender, RoutedEventArgs e)
        {
            // Set the start date in the business logic layer
            s_bl.Clock.SetStartDate(StartDate);
            this.Close(); // Close the current window

            // Set schedule for all tasks

            var t = s_bl.Task.ReadAll().ToArray();
            for (int i = 0; i < t.Count(); i++)
            {
                s_bl.Task.SetScheduele(t[i]);
            }

       
            // Show success message
            MessageBox.Show("Dates assigned successfully", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}