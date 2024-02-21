using BlApi;
using BO;
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
    /// Interaction logic for TaskWindow.xaml
    /// </summary>
    /// 

    public partial class TaskWindow : Window
    {

        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();



        public IEnumerable<BO.TaskInList> A { get; set; }
        public IEnumerable<BO.Task> TaskList
        {
            get { return (IEnumerable<BO.Task>)GetValue(TaskListProperty); }
            set { SetValue(TaskListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TaskList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TaskListProperty =
            DependencyProperty.Register("TaskList", typeof(IEnumerable<BO.Task>), typeof(TaskWindow), new PropertyMetadata(null));


        public DateTime? StartDate
        {
            get { return (DateTime?)GetValue(StartDateProperty); }
            set { SetValue(StartDateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StartDate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StartDateProperty =
            DependencyProperty.Register("StartDate", typeof(DateTime?), typeof(TaskWindow), new PropertyMetadata(null));



        public BO.Task task
        {
            get { return (BO.Task)GetValue(TaskProperty); }
            set { SetValue(TaskProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        int id;
        public static readonly DependencyProperty TaskProperty =
            DependencyProperty.Register("task", typeof(BO.Task), typeof(TaskWindow), new PropertyMetadata(null));


        public TaskWindow(int Id = 0)
        {
            InitializeComponent();
            // clear A:
            A = new List<BO.TaskInList>();
            StartDate = s_bl.Clock.GetStartDate();
            TaskList = s_bl.Task.ReadAll();
            id = Id;
            if (Id == 0)
            {
                task = new BO.Task();
            }
            else
            {
                try
                {
                    task = s_bl.Task.Read(Id)!;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        private void Add_UpdateTask_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (id == 0)
                {
                    
                    s_bl.Task.Create(task );
                    MessageBox.Show("Task added successfully", "Message", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                else
                {
                    task.Dependencies = A.ToList();
                    s_bl.Task.Update(task);
                    MessageBox.Show("Task updated successfully", "Message", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var checkb = sender as CheckBox;
            if(checkb?.IsChecked == true)
            {
                int Id = (int)checkb.Tag;
                BO.Task? task=s_bl.Task.Read(Id);
                BO.TaskInList tsk = new TaskInList { Id = Id, Alias = task.Alias, Status = task.Status, Description = task.Description };
                A.Append(tsk);
                
            }
        }
    }
}

