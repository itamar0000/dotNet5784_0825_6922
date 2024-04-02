namespace PL;

using BlApi;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System;
using System.Drawing;
using System.Drawing.Imaging;

/// <summary>
/// Converts ID to content based on its value.
/// </summary>
class ConvertIdToContent : IValueConverter
{
    /// <inheritdoc/>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (int)value == 0 ? "Add" : "Update";
    }

    /// <inheritdoc/>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// Converts ID to enable state based on its value.
/// </summary>
class ConvertIdToEnable : IValueConverter
{
    /// <inheritdoc/>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (int)value == 0 ? true : false;
    }

    /// <inheritdoc/>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// Converts DateTime to enable state based on the current clock.
/// </summary>
class ConvertDatetimeToEnable : IValueConverter
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    /// <inheritdoc/>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (DateTime?)value >= s_bl.Clock.GetStartDate() ? true : false;
    }

    /// <inheritdoc/>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// Converts date difference to total days.
/// </summary>
public class DateDifferenceMultiConverter : IMultiValueConverter
{
    /// <inheritdoc/>
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values != null && values.Length == 2 && values[0] is DateTime scheduledDate && values[1] is DateTime forecastDate)
        {
            return (forecastDate - scheduledDate).TotalDays;
        }

        return null;
    }

    /// <inheritdoc/>
    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// Converts TimeSpan to width.
/// </summary>
public class TimeSpanToWidthConverter : IValueConverter
{
    /// <inheritdoc/>
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

    /// <inheritdoc/>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// Converts date to canvas left position.
/// </summary>
public class DateToCanvasLeftConverter : IValueConverter
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    /// <inheritdoc/>
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

    /// <inheritdoc/>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// Converts DateTime to string and vice versa.
/// </summary>
public class DateTimeToStringConverter : IValueConverter
{
    // Convert DateTime to string
    /// <inheritdoc/>
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
    /// <inheritdoc/>
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


/// <summary>
/// Converts DateTime to background color based on various conditions.
/// </summary>
public class DatetimeToBackgroundConverter : IMultiValueConverter
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

    /// <inheritdoc/>
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values[2] is BO.Status status)
        {
            if (status == BO.Status.InJeopardy)
                return Brushes.Red;
            else if (status == BO.Status.Done)
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

    /// <inheritdoc/>
    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

/// <summary>
/// Converts image path to a BitmapImage.
/// </summary>
public class ImagePathConverter : IValueConverter
{
    private const string DefaultImagePath = @"..\Images\defaultImageOfEngineer.jpg";

    /// <inheritdoc/>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string encodedImageText)
        {
            if (string.IsNullOrEmpty(encodedImageText))
            {
                // Load the default image
                return new BitmapImage(new Uri(DefaultImagePath, UriKind.Relative));
            }

            try
            {
                byte[] imageData = System.Convert.FromBase64String(encodedImageText);
                BitmapImage image = new BitmapImage();
                using (MemoryStream stream = new MemoryStream(imageData))
                {
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.StreamSource = stream;
                    image.EndInit();
                }
                return image;
            }
            catch (Exception ex)
            {
                // Return the default image if an error occurs during conversion
                return new BitmapImage(new Uri(DefaultImagePath, UriKind.Relative));
            }
        }
        else
        {
            // Return the default image for non-string input
            return new BitmapImage(new Uri(DefaultImagePath, UriKind.Relative));
        }
    }

    /// <inheritdoc/>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}

/// <summary>
/// Converts date to content based on its value.
/// </summary>
public class DatetoContentConverter : IValueConverter
{
    /// <inheritdoc/>
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

    /// <inheritdoc/>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        // This method is not used in one-way binding
        throw new NotImplementedException();
    }
}

/// <summary>
/// Converts date to enable state based on its value.
/// </summary>
public class DatetoEnableConverter : IValueConverter
{
    /// <inheritdoc/>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        DateTime? dateTime = (DateTime?)value;
        if (dateTime != null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    /// <inheritdoc/>
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        // This method is not used in one-way binding
        throw new NotImplementedException();
    }
}