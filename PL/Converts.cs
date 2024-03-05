namespace PL;

using BlApi;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;


class ConvertIdToContent : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (int)value == 0 ? "Add" : "Update";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
class ConvertIdToEnable : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (int)value == 0 ? true : false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
class ConvertDatetimeToEnable : IValueConverter
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (DateTime?)value >= s_bl.Clock.GetStartDate() ? true : false;
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

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

public class TimeSpanToWidthConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is TimeSpan timeSpan)
        {
            // You can adjust the conversion logic as per your requirements
            double totalDays = timeSpan.TotalDays;
            return totalDays * 3; // You may adjust the multiplier as needed
        }

        return DependencyProperty.UnsetValue;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
public class DateToCanvasLeftConverter : IValueConverter
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is DateTime dateTime)
        { 
            
            DateTime startDate = (DateTime)value;
            DateTime endDate = (DateTime)s_bl.Clock.GetStartDate()!;

            double percentage = (startDate - endDate).Days;
            return new Thickness(percentage * 3, 0, 0, 0);
        }

        return 0; // Default value
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}


public class DateTimeToStringConverter : IValueConverter
{
    // Convert DateTime to string
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is DateTime dateTime)
        {
            // You can customize the format as per your requirement
            return dateTime.ToString();
        }

        return string.Empty;
    }

    // Convert back from string to DateTime (not implemented for this example)
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string stringValue)
        {
            if (DateTime.TryParseExact(stringValue, "yyyy-MM-dd HH:mm:ss", culture, DateTimeStyles.None, out DateTime result))
            {
                return result;
            }
        }

        // Return DependencyProperty.UnsetValue to indicate conversion failure
        return DependencyProperty.UnsetValue;
    }
}

public class DatetimeToBackgroundConverter : IMultiValueConverter
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values[3] is List<BO.TaskInList> tasks)
        {
           if( tasks.Any(item => item.Status == BO.Status.InJeopardy))
            {
                foreach (var task in tasks)
                {
                   BO.Task temp =  s_bl.Task.Read(task.Id);
                    temp.Status = BO.Status.InJeopardy;
                }
                if (values[4] is int Id)
                {
                    BO.Task temp = s_bl.Task.Read(Id);
                    temp.Status=BO.Status.InJeopardy;
                }
                return Brushes.Red;

            }
        }
        if (values[2] is BO.Status status)
            if(status==BO.Status.InJeopardy)
                return Brushes.Red;

        if (values[0] is DateTime dateTime)
        {
            if (dateTime <= s_bl.CurrentClock)
            {
                return Brushes.Green;
            }
            if (dateTime > s_bl.CurrentClock)
            {
                return Brushes.Red;
            }
            return Brushes.LightGreen;
        }
        else
        {
            if (values[1] is DateTime dateTime1)
            {
                if(dateTime1 >= s_bl.CurrentClock)
                {
                    return Brushes.LightGreen;
                }
                
            }
            return Brushes.Red;
        }

    }


    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
