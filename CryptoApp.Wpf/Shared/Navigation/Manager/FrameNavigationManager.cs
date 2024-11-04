using System.Windows.Controls;

namespace CryptoApp.Wpf.Shared.Navigation.Manager;

public class FrameNavigationManager : IFrameNavigationManager
{
    private readonly Dictionary<FrameType, INavigationService> _navigationServices = [];

    public INavigationService GetNavigationService(FrameType frameType)
    {
        if(!_navigationServices.ContainsKey(frameType))
            throw new InvalidOperationException($"Navigation service for {frameType} frame type is not registered");
        
        return _navigationServices[frameType];
    }

    public void InitializeFrame(Frame frame, FrameType frameType)
    {
        if(_navigationServices.ContainsKey(frameType))
            throw new InvalidOperationException($"Frame of type {frameType} is already initialized");

        var navigationService = new NavigationService();
        navigationService.Initialize(frame);
        _navigationServices.Add(frameType, navigationService);
    }
}