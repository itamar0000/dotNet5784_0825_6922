﻿using PL.Engineer;
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
    /// Interaction logic for TaskEngineerView.xaml
    /// </summary>
    public partial class TaskEngineerView : Window
    {

        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public IEnumerable<BO.Task> TaskInLists
        {
            get { return (IEnumerable<BO.Task>)GetValue(TaskInListsProperty); }
            set { SetValue(TaskInListsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Engineer.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TaskInListsProperty =
            DependencyProperty.Register("TaskInLists", typeof(IEnumerable<BO.Task>), typeof(TaskEngineerView), new PropertyMetadata(null));




       
        public TaskEngineerView(int Id)
        {
            try
            {

                InitializeComponent();
                TaskInLists = s_bl.Task.ReadAll(Item => Item?.Engineer?.Id == Id);
             
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Get the DataContext of the button, which should be the Task object
                if (sender is Button button && button.DataContext is BO.Task task)
                {
                   
                    if (button.Content == "Start")
                    {
                        task.StartDate = DateTime.Now;
                        s_bl.Task.UpdateDatesForEngineerWork(task);
                        MessageBox.Show("Task started successfully", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                        TaskInLists=s_bl.Task.ReadAll(Item => Item?.Engineer?.Id == task.Engineer.Id);
                    }
                    else
                    {
                        task.CompleteDate = DateTime.Now;
                        s_bl.Task.UpdateDatesForEngineerWork(task);
                        MessageBox.Show("Task ended successfully", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                        TaskInLists = s_bl.Task.ReadAll(Item => Item?.Engineer?.Id == task.Engineer.Id);


                    }


                    // You might want to update your UI bindings or notify changes to reflect the changes made to the task object
                }
            }catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
