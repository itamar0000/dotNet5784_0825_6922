using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace PL.Manager
{
    public class DateDifferenceMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values != null && values.Length == 2 && values[0] is DateTime scheduledDate && values[1] is DateTime forecastDate)
            {
                return (forecastDate - scheduledDate).TotalDays;
            }

            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Interaction logic for GantWindow.xaml
    /// </summary>
    public partial class GanttWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        public GanttWindow()
        {
            InitializeComponent();
            TaskList = s_bl?.Task.ReadAll()!;
        }



        public TimeSpan WidthWindow  
        {
            get { return (TimeSpan)GetValue(WidthWindowProperty); }
            set { SetValue(WidthWindowProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WidthWindowProperty =
            DependencyProperty.Register("WidthWindow", typeof(TimeSpan), typeof(GanttWindow), new PropertyMetadata(0));




        public IEnumerable<BO.Task> TaskList
        {
            get { return (IEnumerable<BO.Task>)GetValue(TaskListProperty); }
            set { SetValue(TaskListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TaskList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TaskListProperty =
            DependencyProperty.Register("TaskList", typeof(IEnumerable<BO.Task>), typeof(GanttWindow), new PropertyMetadata(null));



        /*
             // Calculate the duration for each task
            foreach (var task in TaskList)
            {
                if (task.StartDate.HasValue && task.ScheduledDate.HasValue)
                {
                    task.Duration = (task.ScheduledDate.Value - task.StartDate.Value).TotalDays;
                }
                else
                {
                    task.Duration = 0; // Or handle accordingly if either start date or scheduled date is missing
                }
            }
         */

    }
}
