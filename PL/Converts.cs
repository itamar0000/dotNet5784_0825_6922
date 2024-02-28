namespace PL;

using BlApi;
using System.Globalization;
using System.Windows;
using System.Windows.Data;


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
            // Assuming that the Canvas width is fixed at 800 and the date range is from January 1st to December 31st
            double canvasWidth = 800;
            DateTime startDate = new DateTime(dateTime.Year, 1, 1);
            DateTime endDate = new DateTime(dateTime.Year, 12, 31);

            double totalDays = (dateTime - startDate).Days;
            double percentage = totalDays / (endDate - startDate).Days;
            double leftPosition = percentage * canvasWidth;
            if (dateTime <= s_bl.Clock.GetStartDate() )
                return new Thickness(0, 0, 0, 0);
            return new Thickness(leftPosition, 0, 0, 0);
        }

        return 0; // Default value
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}


