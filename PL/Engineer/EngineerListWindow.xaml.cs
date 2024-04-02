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
        // Static reference to Business Logic layer
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        // Event handler for property changes
        public event PropertyChangedEventHandler? PropertyChanged;

        // Constructor for EngineerListWindow
        public EngineerListWindow()
        {
            StartNameOfEngineer = "";
            InitializeComponent();

            // Initialize EngineerListAll with all engineers
            EngineerListAll = s_bl?.Engineer.ReadAll()!;
            EngineerList = EngineerListAll;
        }

        // Property for the list of engineers to be displayed
        public IEnumerable<BO.Engineer> EngineerList
        {
            get { return (IEnumerable<BO.Engineer>)GetValue(EngineerListProperty); }
            set { SetValue(EngineerListProperty, value); }
        }

        // Dependency property definition for EngineerList
        public static readonly DependencyProperty EngineerListProperty =
            DependencyProperty.Register("EngineerList", typeof(IEnumerable<BO.Engineer>), typeof(EngineerListWindow), new PropertyMetadata(null));

        // List containing all engineers
        IEnumerable<BO.Engineer> EngineerListAll;

        // Property for the selected engineer level filter
        public BO.EngineerExperience EngineerLevel { get; set; } = BO.EngineerExperience.None;

        // Event handler for the selection change in the level filter combo box
        private void Combo_LevelChanged(object sender, SelectionChangedEventArgs e)
        {
            // Filter engineer list based on selected level
            EngineerList = (EngineerLevel == BO.EngineerExperience.None) ?
                s_bl?.Engineer.ReadAll()! : s_bl?.Engineer.ReadAll(item => item.Level == EngineerLevel)!;
        }

        // Event handler for the Add button click
        private void AddClick(object sender, RoutedEventArgs e)
        {
            // Open EngineerWindow to add a new engineer
            new EngineerWindow().ShowDialog();
            // Refresh engineer list
            EngineerList = s_bl.Engineer.ReadAll()!;
        }

        // Event handler for double-clicking on an engineer item in the list
        private void DoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Retrieve the selected engineer from the list
            BO.Engineer? EngineerInList = (sender as ListView)?.SelectedItem as BO.Engineer;
            if (EngineerInList != null)
            {
                // Open EngineerWindow to edit the selected engineer
                new EngineerWindow(EngineerInList!.Id).ShowDialog();
                // Refresh engineer list
                EngineerList = s_bl.Engineer.ReadAll()!;
            }
        }

        // Property for the search text of engineers by name
        public string StartNameOfEngineer
        {
            get { return (string)GetValue(StartNameOfEngineerProperty); }
            set { SetValue(StartNameOfEngineerProperty, value); }
        }

        // Dependency property definition for StartNameOfEngineer
        public static readonly DependencyProperty StartNameOfEngineerProperty =
            DependencyProperty.Register("StartNameOfEngineer", typeof(string), typeof(EngineerListWindow), new PropertyMetadata(null));

        // Event handler for searching engineers by name
        private void TextBox_SearchEngineers(object sender, TextChangedEventArgs e)
        {
            // Update the search string with the entered text
            StartNameOfEngineer = (sender as TextBox)!.Text.ToLower();
            // Filter engineer list based on search string
            if (StartNameOfEngineer == "")
                EngineerList = EngineerListAll;
            else
                EngineerList = EngineerListAll.Where(item => item.Name.ToLower().Contains(StartNameOfEngineer));
        }
    }
}