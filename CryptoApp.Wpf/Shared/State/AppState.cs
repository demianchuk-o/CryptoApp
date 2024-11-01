namespace CryptoApp.Wpf.Shared;

public class AppState
{
    public bool IsLoading { get; private set; }
    public bool IsLoaded { get; private set; }
    public bool IsError { get; private set; }
    public string? ErrorMessage { get; private set; }
    
    private AppState() {}
    
    public static AppState Loading() => new AppState { IsLoading = true };
    public static AppState Loaded() => new AppState { IsLoaded = true };
    public static AppState Error(string errorMessage) => new AppState { IsError = true, ErrorMessage = errorMessage };
    
}