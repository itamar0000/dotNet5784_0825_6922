using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace PL.Task
{
    /// <summary>
    /// Interaction logic for SetEngineerWindow.xaml
    /// </summary>
    public partial class SetEngineerWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public SetEngineerWindow(BO.EngineerExperience? experience, EventHandler<EngineerSelectedEventArgs> EngineerSelectedHandler)
        {
            InitializeComponent();
            EngineerList = s_bl?.Engineer.ReadAll(engineer => (int)engineer!.Level >= (int)experience!)!;
            EngineerSelected += EngineerSelectedHandler;
        }

        public IEnumerable<BO.Engineer> EngineerList // get list of all engineers
        {
            get { return (IEnumerable<BO.Engineer>)GetValue(EngineerListProperty); }
            set { SetValue(EngineerListProperty, value); }
        }

        public static readonly DependencyProperty EngineerListProperty =
    DependencyProperty.Register("EngineerList", typeof(IEnumerable<BO.Engineer>), typeof(SetEngineerWindow), new PropertyMetadata(null));



        public event EventHandler<EngineerSelectedEventArgs> EngineerSelected;

        private void SelectedEngineer(object sender, MouseButtonEventArgs e)
        {
            BO.Engineer? engineerInList = (sender as ListView)?.SelectedItem as BO.Engineer;
            if (engineerInList != null)
            {
                int id = engineerInList!.Id!; // convert from int? to int
                EngineerSelected?.Invoke(this, new EngineerSelectedEventArgs { SelectedEngineerId = id });
                Close();
            }
        }
    }
}