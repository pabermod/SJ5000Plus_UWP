using SJ5000Plus.Services.CameraServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;

namespace SJ5000Plus.ViewModels
{
    public class ConnectPageViewModel : ViewModelBase
    {
        private bool _ButtonEnabled;
        private bool _ProgressVisibility;
        private ICameraService Camera;

        public bool ButtonEnabled
        {
            get { return _ButtonEnabled; }
            set { Set(ref _ButtonEnabled, value); }
        }

        public ICommand MyButtonClickCommand
        {
            get { return new DelegateCommand<object>(ConnectButtonClick, CanExecuteClick); }
        }

        public bool ProgressVisibility
        {
            get { return _ProgressVisibility; }
            set { Set(ref _ProgressVisibility, value); }
        }

        public override Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {
            (App.Current as App).Camera = Camera as CameraService;
            return Task.CompletedTask;
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            // Hide the Hamburger Menu
            Views.Shell.HamburgerMenu.IsFullScreen = true;
            Camera = (App.Current as App).Camera;
            return Task.CompletedTask;
        }

        public override Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            return Task.CompletedTask;
        }

        private bool CanExecuteClick(object context)
        {
            //this is called to evaluate whether ConnectButtonClick can be called
            return true;
        }

        private async void ConnectButtonClick(object context)
        {
            //this is called when the button is clicked
            ButtonEnabled = false;
            ProgressVisibility = true;
            if (await Camera.Connect())
            {
                await Camera.GetToken();
                Globals.SetIsConnected(true);
                ButtonEnabled = true;
            }
            ProgressVisibility = false;
            ButtonEnabled = true;
        }
    }
}