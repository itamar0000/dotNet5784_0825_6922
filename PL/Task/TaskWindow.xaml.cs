using BlApi;
using BO;
using Syncfusion.ProjIO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

       public class Temp
        {
            public TaskInList temp { get; set;}
            public  bool IsChecked { get; set; }
        }



        public List<Temp> TaskinList
        {
            get { return (List<Temp>)GetValue(TaskinListProperty); }
            set { SetValue(TaskinListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TaskinList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TaskinListProperty =
            DependencyProperty.Register("TaskinList", typeof(List<Temp>), typeof(TaskWindow), new PropertyMetadata(null));



        public List<BO.TaskInList> AddDependency { get; set; }
        //public IEnumerable<BO.Task> TaskList
        //{
        //    get { return (IEnumerable<BO.Task>)GetValue(TaskListProperty); }
        //    set { SetValue(TaskListProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for TaskList.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty TaskListProperty =
        //    DependencyProperty.Register("TaskList", typeof(IEnumerable<BO.Task>), typeof(TaskWindow), new PropertyMetadata(null));
        public List<BO.TaskInList> DelDependency { get; set; }


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
            // clear A:
            AddDependency = new List<BO.TaskInList>();
            DelDependency = new List<BO.TaskInList>();
            StartDate = s_bl.Clock.GetStartDate();
            //       TaskList = s_bl.Task.ReadAll();
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
                    // TaskList = (from BO.Task t in s_bl.Task.ReadAll()
                    //             where task.Dependencies==null || task.Dependencies.FirstOrDefault(item => item.Id == t.Id) == null
                    //             select t);
                    //  BO.TaskInList temp= new TaskInList { Id=task.Id, Alias=task.Alias, Status=task.Status, Description=task.Description };
                    //  TaskList=TaskList.Where(item=>item.Id != task.Id);
                    TaskinList = (from t in s_bl.Task.ReadAll()
                                  select new Temp()
                                  {
                                      temp = new BO.TaskInList { Id = t.Id, Description = t.Description, Status = t.Status, Alias = t.Alias },
                                      IsChecked = task.Dependencies.Any(item => item.Id == t.Id)
                                  }).ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            InitializeComponent();
        }


        private void Add_UpdateTask_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (id == 0)
                {

                    s_bl.Task.Create(task);
                    MessageBox.Show("Task added successfully", "Message", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                else
                {
                    foreach(var item in AddDependency)
                    {
                        foreach(var item2 in task.Dependencies)
                        {
                           if(item.Id==item2.Id)
                            {
                               AddDependency.Remove(item);
                                break;
                            }
                        }
                    }

                    task?.Dependencies?.AddRange(AddDependency);
                    foreach (var item in DelDependency)
                    {
                        foreach (var item2 in task.Dependencies)
                            if (item2.Id == item.Id)
                            {
                                task?.Dependencies?.Remove(item2);
                                break;
                            }
                    }
                    s_bl.Task.Update(task!);
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
            if (checkb?.IsChecked == true)
            {
                int Id = (int)checkb.Tag;
                BO.Task? task = s_bl.Task.Read(Id);
                BO.TaskInList tsk = new TaskInList { Id = Id, Alias = task.Alias, Status = task.Status, Description = task.Description };
                AddDependency.Add(tsk);

            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
      
            var checkb = sender as CheckBox;
            int Id = (int)checkb.Tag;
         
            BO.Task? task = s_bl.Task.Read(Id);
            BO.TaskInList tsk = new TaskInList { Id = Id, Alias = task.Alias, Status = task.Status, Description = task.Description };
                DelDependency.Add(tsk);
            foreach (var item in AddDependency)
            {
                if (item.Id == Id)
                {
                    AddDependency.Remove(item);
                    break;
                }
            }
        }


    }
}

