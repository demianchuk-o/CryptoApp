using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CryptoApp.Wpf.Shared.ValueConverters;

public class NumberConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value?.ToString();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not string strValue)
        {
            return DependencyProperty.UnsetValue;
        }
        
        if (decimal.TryParse(strValue, NumberStyles.Any, culture, out decimal result))
        {
            return result;
        }

        if (string.IsNullOrEmpty(strValue))
        {
            return 0;
        }

        return DependencyProperty.UnsetValue;
    }
}