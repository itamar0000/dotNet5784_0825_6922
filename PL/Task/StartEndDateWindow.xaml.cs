using BO;
using DO;
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

namespace PL.Task;

/// <summary>
/// Interaction logic for StartEndDateWindow.xaml
/// </summary>
public partial class StartEndDateWindow : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    public DateTime? Start
    {
        get { return (DateTime?)GetValue(StartProperty); }
        set { SetValue(StartProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Startdate.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty StartProperty =
        DependencyProperty.Register("Start", typeof(DateTime?), typeof(StartEndDateWindow), new PropertyMetadata(null));

    public DateTime? Enddate
    {
        get { return (DateTime?)GetValue(EnddateProperty); }
        set { SetValue(EnddateProperty, value); }
    }

    // Using a DependencyProperty as the backing store for Startdate.  This enables animation, styling, binding, etc...
    public static readonly DependencyProperty EnddateProperty =
        DependencyProperty.Register("Enddate", typeof(DateTime?), typeof(StartEndDateWindow), new PropertyMetadata(null));

    BO.Task Currentask;

    public StartEndDateWindow(int id)
    {
        InitializeComponent();
        Currentask = s_bl.Task.Read(id)!;
        Start = Currentask.StartDate;
        Enddate = Currentask.CompleteDate;
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {      
        try
        {
            Currentask.StartDate = Start;
            Currentask.CompleteDate = Enddate;
            s_bl.Task.UpdateDatesForEngineerWork(Currentask);
            MessageBox.Show("Dates assaigned succusfully", "message", MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            Close();
        }
    }
}
