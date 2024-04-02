using PL.Engineer;
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
    /// Interaction logic for TaskListWindow.xaml
    /// </summary>
    public partial class TaskListWindow : Window, INotifyPropertyChanged
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Constructor for TaskListWindow.
        /// </summary>
        public TaskListWindow()
        {
            InitializeComponent();
            StartAliasOfTask = "";
            TaskListAll = s_bl?.Task.ReadAll()!;
            TaskList = TaskListAll;
        }

        /// <summary>
        /// Gets or sets the task list.
        /// </summary>
        public IEnumerable<BO.Task> TaskList
        {
            get { return (IEnumerable<BO.Task>)GetValue(TaskListProperty); }
            set { SetValue(TaskListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TaskList. This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TaskListProperty =
            DependencyProperty.Register("TaskList", typeof(IEnumerable<BO.Task>), typeof(TaskListWindow), new PropertyMetadata(null));

        IEnumerable<BO.Task> TaskListAll;

        /// <summary>
        /// Handles the double-click event on the ListView.
        /// </summary>
        private void DoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.Task? TaskInList = (sender as ListView)?.SelectedItem as BO.Task;
            if (TaskInList != null)
            {
                new TaskWindow(TaskInList!.Id).ShowDialog();
                TaskList = s_bl.Task.ReadAll()!;
            }
        }

        /// <summary>
        /// Gets or sets the task level.
        /// </summary>
        public BO.Status TaskLevel { get; set; } = BO.Status.None;

        /// <summary>
        /// Handles the selection changed event of the combo box for task level.
        /// </summary>
        private void Combo_LevelChanged(object sender, SelectionChangedEventArgs e)
        {
            TaskList = (TaskLevel == BO.Status.None) ?
           s_bl?.Task.ReadAll()! : s_bl?.Task.ReadAll(item => item?.Status == TaskLevel)!;
        }

        /// <summary>
        /// Handles the click event for adding a new task.
        /// </summary>
        private void AddClick(object sender, RoutedEventArgs e)
        {
            new TaskWindow().ShowDialog();
            TaskList = s_bl.Task.ReadAll()!;
        }

        /// <summary>
        /// Gets or sets the start alias of the task.
        /// </summary>
        public string StartAliasOfTask
        {
            get { return (string)GetValue(StartAliasOfTaskProperty); }
            set { SetValue(StartAliasOfTaskProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StartAliasOfTask. This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StartAliasOfTaskProperty =
            DependencyProperty.Register("StartAliasOfTask", typeof(string), typeof(TaskListWindow), new PropertyMetadata(null));

        /// <summary>
        /// Handles the text changed event for searching tasks.
        /// </summary>
        private void TextBox_SearchTasks(object sender, TextChangedEventArgs e)
        {
            StartAliasOfTask = (sender as TextBox)!.Text.ToLower();
            if (StartAliasOfTask == "")
                TaskList = TaskListAll;
            else
                TaskList = TaskListAll.Where(item => item.Alias.ToLower().Contains(StartAliasOfTask));
        }
    }
}
