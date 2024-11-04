using System.Windows.Controls;

namespace CryptoApp.Wpf.Shared.Navigation.Manager;

public interface IFrameNavigationManager
{
    INavigationService GetNavigationService(FrameType frameType);
    void InitializeFrame(Frame frame, FrameType frameType);
}