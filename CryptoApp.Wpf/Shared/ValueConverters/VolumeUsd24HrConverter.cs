using System.Globalization;
using System.Windows.Data;

namespace CryptoApp.Wpf.Shared.ValueConverters;

public class VolumeUsd24HrConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is decimal volumeUsd)
        {
            return string.Format(new CultureInfo("en-US"), "{0:C}", volumeUsd);
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