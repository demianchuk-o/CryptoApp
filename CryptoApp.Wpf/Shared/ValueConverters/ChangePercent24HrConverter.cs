using System.Globalization;
using System.Windows.Data;

namespace CryptoApp.Wpf.Shared.ValueConverters;

public class ChangePercent24HrConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is decimal changePercent)
        {
            return changePercent.ToString("P2", CultureInfo.InvariantCulture);
        }
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string percentString && decimal.TryParse(percentString.TrimEnd('%'), NumberStyles.Any, CultureInfo.InvariantCulture, out var changePercent))
        {
            return changePercent / 100;
        }
        return value;
    }
}