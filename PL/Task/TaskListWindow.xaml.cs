using PL.Engineer;
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

namespace PL.Task
{
    /// <summary>
    /// Interaction logic for TaskListWindow.xaml
    /// </summary>
    public partial class TaskListWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public TaskListWindow()
        {
            InitializeComponent();
            TaskList = s_bl?.Task.ReadAll()!;

        }


        public IEnumerable<BO.Task> TaskList
        {
            get { return (IEnumerable<BO.Task>)GetValue(TaskListProperty); }
            set { SetValue(TaskListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RaskList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TaskListProperty =
            DependencyProperty.Register("TaskList", typeof(IEnumerable<BO.Task>), typeof(TaskListWindow), new PropertyMetadata(null));


        private void DoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.Task? TaskInList = (sender as ListView)?.SelectedItem as BO.Task;
            if (TaskInList != null)
            {
                new TaskWindow(TaskInList!.Id).ShowDialog();
                TaskList = s_bl.Task.ReadAll()!;
            }
        }
        public BO.Status TaskLevel { get; set; } = BO.Status.None;

        private void Combo_LevelChanged(object sender, SelectionChangedEventArgs e)
        {
            TaskList = (TaskLevel == BO.Status.None) ?
           s_bl?.Task.ReadAll()! : s_bl?.Task.ReadAll(item => item?.Status == TaskLevel)!;
        }

        private void AddClick(object sender, RoutedEventArgs e)
        {
            new TaskWindow().ShowDialog();
            TaskList = s_bl.Task.ReadAll()!;
        }
    }
}

