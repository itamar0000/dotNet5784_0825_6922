namespace PL;

using BlApi;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;


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
        if (values[2] is BO.Status status)
        {
            if (status == BO.Status.InJeopardy)
                return Brushes.Red;
            else if(status==BO.Status.Done)
            {
                return Brushes.Green;
            }
        }
     

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
                if (dateTime1 >= s_bl.CurrentClock)
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

public class ImagePathConverter : IValueConverter
{
    // Convert from image path to BitmapImage
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string imagePath && !string.IsNullOrEmpty(imagePath))
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.UriSource = new Uri(imagePath);
            image.EndInit();
            return image;
        }

        return null;
    }

    // Convert from BitmapImage to image path (not implemented in this case)
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
public class DatetoContentConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
             DateTime? dateTime = (DateTime?)value;
            // Check if DateTime is assigned
            if (dateTime != null)
            {
                return "End";
            }
            else
            {
                return "Start";
            }
      


    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        // This method is not used in one-way binding
        throw new NotImplementedException();
    }
}
public class DatetoEnableConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        DateTime? dateTime = (DateTime?)value;
        if(dateTime!=null)
        {
            return false;
        }
        else
        {
            return true;
        }

     
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        // This method is not used in one-way binding
        throw new NotImplementedException();
    }
}