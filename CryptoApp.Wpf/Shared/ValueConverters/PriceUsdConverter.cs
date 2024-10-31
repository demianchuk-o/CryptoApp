using System.Globalization;
using System.Windows.Data;

namespace CryptoApp.Wpf.Shared.ValueConverters;

public class PriceUsdConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is decimal priceUsd)
        {
            return priceUsd.ToString("C2", CultureInfo.InvariantCulture);
        }
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string priceString && decimal.TryParse(priceString, NumberStyles.Currency, CultureInfo.InvariantCulture, out var priceUsd))
        {
            return priceUsd;
        }
        return value;
    }
}