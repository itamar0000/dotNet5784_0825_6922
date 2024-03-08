﻿using System;
using PL.Engineer;
using PL.Task;
using PL.messages;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PL.Manager;

namespace PL;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class ManagerWindow : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    public ManagerWindow()
    {
        InitializeComponent();
        DataContext = this;

        TaskListAll = s_bl.Task.ReadAll();
        TaskList = TaskListAll.Where(item => item.CompleteDate != null);
        ProgressBarValue = (TaskList.Count() * 100) / TaskListAll.Count();
    }

    public int ProgressBarValue
    {
        get { return (int)GetValue(ProgressBarValueProperty); }
        set { SetValue(ProgressBarValueProperty, value); }
    }

    // Using a DependencyProperty as the backing store for ProgressBarValue.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty ProgressBarValueProperty =
        DependencyProperty.Register("ProgressBarValue", typeof(int), typeof(ManagerWindow), new PropertyMetadata(null));



    public IEnumerable<BO.Task> TaskList
    {
        get { return (IEnumerable<BO.Task>)GetValue(TaskListProperty); }
        set { SetValue(TaskListProperty, value); }
    }

    // Using a DependencyProperty as the backing store for RaskList.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty TaskListProperty =
        DependencyProperty.Register("TaskList", typeof(IEnumerable<BO.Task>), typeof(ManagerWindow), new PropertyMetadata(null));

    IEnumerable<BO.Task> TaskListAll;

    private void BtnEngineers_Click(object sender, RoutedEventArgs e)
    {
        new EngineerListWindow().Show();
    }

    private void BtnInit_Click(object sender, RoutedEventArgs e)
    {
        MessageBoxResult mbResult = MessageBox.Show("Are you sure you want to iniitalize the data?", "Initalize", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
        if (mbResult == MessageBoxResult.Yes)
        {
            s_bl.InitializeDB();
            s_bl.ResetTime();
        }
    }

    private void BtnTasks_Click(object sender, RoutedEventArgs e)
    {
        new TaskListWindow().Show();
    }

    private void Button_Click_Gantt(object sender, RoutedEventArgs e)
    {
        new GanttWindow().Show();
    }

    private void Btn_reset(object sender, RoutedEventArgs e)
    {
        MessageBoxResult mbResult = MessageBox.Show("Are you sure you want to reset the data?", "Initalize", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
        if (mbResult == MessageBoxResult.Yes)
        {
            s_bl.ResetDB();
        }
    }

    private void Btn_setschedule(object sender, RoutedEventArgs e)
    {
        if (s_bl.Clock.GetStartDate() == null)
        {
            new StartDateWindow().Show();
           
        }

    }  
}