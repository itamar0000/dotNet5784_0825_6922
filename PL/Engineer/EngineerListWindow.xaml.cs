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

namespace PL.Engineer
{
    /// <summary>
    /// Interaction logic for EngineerListWindow.xaml
    /// </summary>
    public partial class EngineerListWindow : Window, INotifyPropertyChanged
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public event PropertyChangedEventHandler? PropertyChanged;

        public EngineerListWindow()
        {
            StartNameOfEngineer = "";
            InitializeComponent();
            EngineerListAll = s_bl?.Engineer.ReadAll()!;
            EngineerList = EngineerListAll;
        }

        public IEnumerable<BO.Engineer> EngineerList
        {
            get { return (IEnumerable<BO.Engineer>)GetValue(EngineerListProperty); }
            set { SetValue(EngineerListProperty, value); }
        }

        public static readonly DependencyProperty EngineerListProperty =
            DependencyProperty.Register("EngineerList", typeof(IEnumerable<BO.Engineer>), typeof(EngineerListWindow), new PropertyMetadata(null));

        IEnumerable<BO.Engineer> EngineerListAll;

        public BO.EngineerExperience EngineerLevel { get; set; } = BO.EngineerExperience.None;

        private void Combo_LevelChanged(object sender, SelectionChangedEventArgs e)
        {
            EngineerList = (EngineerLevel == BO.EngineerExperience.None) ?
            s_bl?.Engineer.ReadAll()! : s_bl?.Engineer.ReadAll(item => item.Level == EngineerLevel)!;
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            new EngineerWindow().ShowDialog();
            EngineerList = s_bl.Engineer.ReadAll()!;
        }

        private void DoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.Engineer? EngineerInList = (sender as ListView)?.SelectedItem as BO.Engineer;
            if (EngineerInList != null)
            {
                new EngineerWindow(EngineerInList!.Id).ShowDialog();
                EngineerList = s_bl.Engineer.ReadAll()!;
            }
        }


        public string StartNameOfEngineer
        {
            get { return (string)GetValue(StartNameOfEngineerProperty); }
            set { SetValue(StartNameOfEngineerProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StartNameOfEngineer.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StartNameOfEngineerProperty =
            DependencyProperty.Register("StartNameOfEngineer", typeof(string), typeof(EngineerListWindow), new PropertyMetadata(null));


        private void TextBox_SearchEngineers(object sender, TextChangedEventArgs e)
        {
            StartNameOfEngineer = (sender as TextBox)!.Text.ToLower();
            if (StartNameOfEngineer == "")
                EngineerList = EngineerListAll;
            else
                EngineerList = EngineerListAll.Where(item => item.Name.ToLower().Contains(StartNameOfEngineer));
        }
    }
}
