using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace CryptoApp.Wpf.Shared.ValueConverters;

public class PercentageToBrushConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is decimal changePercent)
        {
            return changePercent >= 0 ? Brushes.SeaGreen : Brushes.Firebrick;
        }
        return Brushes.Black;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not SolidColorBrush brush)
        {
            return 0;
        }
        
        return brush.Color == Brushes.SeaGreen.Color ? 1 : -1;
    }
}