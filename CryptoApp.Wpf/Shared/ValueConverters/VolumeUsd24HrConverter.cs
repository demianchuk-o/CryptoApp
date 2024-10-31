using System.Globalization;
using System.Windows.Data;

namespace CryptoApp.Wpf.Shared.ValueConverters;

public class VolumeUsd24HrConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is decimal volumeUsd)
        {
            return volumeUsd.ToString("N0", CultureInfo.InvariantCulture);
        }
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string volumeString && decimal.TryParse(volumeString, NumberStyles.Any, CultureInfo.InvariantCulture, out var volumeUsd))
        {
            return volumeUsd;
        }
        return value;
    }
}