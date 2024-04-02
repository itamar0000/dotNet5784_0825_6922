using PL.Task; // Import Task namespace
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

namespace PL
{
    /// <summary>
    /// Interaction logic for GetIdWindow.xaml
    /// </summary>
    public partial class GetIdWindow : Window
    {
        // Static reference to Business Logic layer
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        // Dependency property for Id
        public int Id
        {
            get { return (int)GetValue(IdProperty); }
            set { SetValue(IdProperty, value); }
        }

        // Dependency property definition for Id
        public static readonly DependencyProperty IdProperty =
            DependencyProperty.Register("Id", typeof(int), typeof(GetIdWindow), new PropertyMetadata(null));

        // Constructor for GetIdWindow
        public GetIdWindow()
        {
            InitializeComponent();
        }

        // Event handler for clicking the Enter button
        private void Btn_EnterId(object sender, RoutedEventArgs e)
        {
            try
            {
                // Retrieve engineer information using the provided ID
                s_bl.Engineer.Read(Id);
                this.Close(); // Close the current window
                // Open the TaskEngineerView window with the provided ID
                new TaskEngineerView(Id).Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
