using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using CryptoApp.Core.CryptoCurrencies;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace CryptoApp.Wpf.Shared.UI;

public partial class PriceChart : UserControl, INotifyPropertyChanged
{
    private OhlcSeries _ohlcSeries;
    public PriceChart()
    {
        InitializeComponent();
        DataContext = this;
        
        Labels = new List<string>();
        _ohlcSeries = new OhlcSeries
        {
            Title = "Price",
            Values = new ChartValues<OhlcPoint>()
        };

        SeriesCollection = new SeriesCollection { _ohlcSeries };
    }
    
    public static readonly DependencyProperty CandlesProperty = 
        DependencyProperty.Register(
            nameof(Candles),
            typeof(IEnumerable<CandleData>),
            typeof(PriceChart),
            new PropertyMetadata(default(IEnumerable<CandleData>), OnCandlesChanged));

    public IEnumerable<CandleData> Candles
    {
        get => (IEnumerable<CandleData>)GetValue(CandlesProperty);
        set => SetValue(CandlesProperty, value);
    }
    
    private static void OnCandlesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is PriceChart priceChart)
        {
            priceChart.UpdateChart();
        }
    }
    
    public SeriesCollection SeriesCollection { get; set; }

    private List<string> _labels = [];
    public List<string> Labels
    {
        get => _labels;
        set
        {
            _labels = value;
            OnPropertyChanged(nameof(Labels));
        }
    }

    private void UpdateChart()
    {
        if(Candles is null || !Candles.Any())
        {
            _ohlcSeries.Values.Clear();
            Labels.Clear();
            return;
        }
        
        var olhcValues = new ChartValues<OhlcPoint>();
        var labels = new List<string>();
        int i = 0;
        foreach (var candleData in Candles)
        {
            if (i == 0)
            {
                Console.WriteLine($"O: {candleData.Open} H: {candleData.High} L: {candleData.Low} C: {candleData.Close}");
                Console.WriteLine($"Label{candleData.Period.ToString("dd/MM/yyyy HH:mm")}");
                i++;
            }
            olhcValues.Add(new OhlcPoint(
                open: (double)candleData.Open,
                high: (double)candleData.High,
                low: (double)candleData.Low,
                close: (double)candleData.Close));
            
            labels.Add(candleData.Period.ToString("dd/MM/yyyy HH:mm"));
        }
        
        _ohlcSeries.Values.Clear();
        _ohlcSeries.Values.AddRange(olhcValues);
        Labels = labels;
        
        OnPropertyChanged(nameof(SeriesCollection));
    }
    
    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}