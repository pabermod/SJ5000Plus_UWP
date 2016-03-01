using Template10.Mvvm;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using SJ5000Plus.Services.CameraServices;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;

namespace SJ5000Plus.ViewModels
{
    public class ConnectPageViewModel : ViewModelBase
    {
        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            // Hide the Hamburger Menu
            Views.Shell.HamburgerMenu.IsFullScreen = true;
            //Camera = (App.Current as App).Camera;
            return Task.CompletedTask;
        }

        public override Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {
            //(App.Current as App).Camera = Camera as CameraService;
            return Task.CompletedTask;
        }

        public override Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            return Task.CompletedTask;
        }
    }
}
