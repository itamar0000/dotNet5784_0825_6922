﻿using BlApi;
using BO;
using DO;
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
        private int engineerId = 0;

        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public class Temp
        {
            public TaskInList temp { get; set; }
            public bool IsChecked { get; set; }
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

        public List<BO.TaskInList> DelDependency { get; set; }

        /// <summary>
        /// start date of the task
        /// </summary>
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

        // Using a DependencyProperty as the backing store for MyProperty.  This    enables animation, styling, binding, etc...
        int id;
        public static readonly DependencyProperty TaskProperty =
            DependencyProperty.Register("task", typeof(BO.Task), typeof(TaskWindow), new PropertyMetadata(null));


        public TaskWindow(int Id = 0)
        {
            AddDependency = new List<BO.TaskInList>();
            DelDependency = new List<BO.TaskInList>();
            StartDate = s_bl.Clock.GetStartDate();
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
                    EngineerId = (task.Engineer != null) ? (int)task.Engineer.Id : 0;

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
                if (EngineerId != 0)
                {
                    task.Engineer = new EngineerInTask { Id = EngineerId, Name = s_bl.Engineer.Read(EngineerId)!.Name };
                }

                if (id == 0)
                {

                    s_bl.Task.Create(task);
                    MessageBox.Show("Task added successfully", "Message", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                else
                {
                    // var copyOfAddDependen = AddDependency.All(item => item.Id != 0);
                    var copyOfAddDependency = AddDependency.ToArray();
                    for (int i = 0; i < copyOfAddDependency.Length; ++i)
                    {
                        foreach (var item2 in task.Dependencies)
                        {
                            if (copyOfAddDependency[i].Id == item2.Id)
                            {
                                AddDependency.Remove(copyOfAddDependency[i]);
                                break;
                            }
                        }
                    }



                    task?.Dependencies?.AddRange(AddDependency);
                    task?.Dependencies?.RemoveAll(item => DelDependency.Contains(item));
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


        /// <summary>
        /// Handles the event when a checkbox is checked.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var checkb = sender as CheckBox;
            if (checkb?.IsChecked == true)
            {
                int Id = (int)checkb.Tag;
                BO.Task? task = s_bl.Task.Read(Id);
                BO.TaskInList tsk = new TaskInList { Id = Id, Alias = task.Alias, Status = task.Status, Description = task.Description };
                AddDependency.Add(tsk);

                var copyOfDelDependency = DelDependency.ToArray();
                for (int i = 0; i < copyOfDelDependency.Length; ++i)
                {
                    if (copyOfDelDependency[i].Id == Id)
                    {
                        DelDependency.Remove(copyOfDelDependency[i]);
                        break;
                    }
                }

            }
        }

        // Other methods and properties...



        /// <summary>
        /// Handles the event when a checkbox is unchecked.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            var checkb = sender as CheckBox;
            int Id = (int)checkb.Tag;

            BO.Task? tasks = s_bl.Task.Read(Id);
            BO.TaskInList tsk = new TaskInList { Id = Id, Alias = task.Alias, Status = task.Status, Description = task.Description };
            foreach (var item in AddDependency)
            {
                if (item.Id == Id)
                {
                    DelDependency.Add(item);
                    break;
                }
            }
            foreach (var item in task.Dependencies)
            {
                if (item.Id == Id)
                {
                    DelDependency.Add(item);
                    break;
                }
            }
        }

        /// <summary>
        /// Handles the event when the Set Engineer button is clicked.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="e">The event arguments.</param>
        private void SetEngineer_Click(object sender, RoutedEventArgs e)
        {
            var window = new SetEngineerWindow(BO.EngineerExperience.None, EngineerSelectedHandler);
            if (task.Complexity != null)
                window = new SetEngineerWindow(task.Complexity, EngineerSelectedHandler);
            window.ShowDialog();
        }

        /// <summary>
        /// Handles the selection of an engineer.
        /// </summary>
        /// <param name="sender">The sender object.</param>
        /// <param name="args">The event arguments containing the selected engineer ID.</param>
        private void EngineerSelectedHandler(object sender, EngineerSelectedEventArgs args)
        {
            EngineerId = args.SelectedEngineerId;
        }

        /// <summary>
        /// Gets or sets the ID of the engineer.
        /// </summary>
        public int EngineerId
        {
            get { return (int)GetValue(EngineerIdProperty); }
            set { SetValue(EngineerIdProperty, value); }
        }

        /// <summary>
        /// Dependency property for the EngineerId.
        /// </summary>
        public static readonly DependencyProperty EngineerIdProperty =
            DependencyProperty.Register("EngineerId", typeof(int), typeof(TaskWindow), new PropertyMetadata(0));

    }
}