using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing.IndexedProperties;
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

namespace PL.Engineer
{
    /// <summary>
    /// Interaction logic for EngineerWindow.xaml
    /// </summary>
    public partial class EngineerWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public BO.Engineer engineer
        {
            get { return (BO.Engineer)GetValue(EngineerProperty); }
            set { SetValue(EngineerProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...


        public static readonly DependencyProperty EngineerProperty =
           DependencyProperty.Register("engineer", typeof(BO.Engineer), typeof(EngineerWindow), new PropertyMetadata(null));

       
        public EngineerWindow(int Id = 0)
        {
            InitializeComponent();

            if(Id==0)
            {
                engineer = new BO.Engineer();
            }
            else
            {
                if(s_bl.Engineer.Read(Id)==null)
                {
                    MessageBox.Show("Engineer not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Close();
                    return;
                }
                engineer= s_bl.Engineer.Read(Id)!;
            }

        }
       

        private void Add_UpdateEngineer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (s_bl.Engineer.Read(engineer.Id)==null)
                {
                    s_bl.Engineer.Create(engineer);
                    MessageBox.Show("Engineer added successfully", "Message", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                else
                {
                    s_bl.Engineer.Update(engineer);
                    MessageBox.Show("Engineer updated successfully", "Message", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }
    }
}
