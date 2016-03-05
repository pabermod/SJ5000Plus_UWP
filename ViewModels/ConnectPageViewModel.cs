using SJ5000Plus.Services.CameraServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.Graphics.Display;
using Windows.Networking.Sockets;
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

        public bool ProgressVisibility
        {
            get { return _ProgressVisibility; }
            set { Set(ref _ProgressVisibility, value); }
        }

        public override Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {
            (App.Current as App).Camera = Camera;
            return Task.CompletedTask;
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            // Only Portrait orientation
            DisplayInformation.AutoRotationPreferences = DisplayOrientations.Portrait;

            // Hide the Hamburger Menu
            Views.Shell.HamburgerMenu.IsFullScreen = true;
            Camera = (App.Current as App).Camera;
            ButtonEnabled = true;
            return Task.CompletedTask;
        }

        public override Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            return Task.CompletedTask;
        }

        DelegateCommand _ConnectCommand;
        public DelegateCommand ConnectCommand
            => _ConnectCommand ?? (_ConnectCommand = new DelegateCommand(async () =>
            {
                //Views.Busy.SetBusy(true, _BusyText);
                Views.Busy.SetBusy(true, "Conectando");
                try
                {
                    if (await Camera.Connect())
                    {
                        if (await Camera.GetToken())
                        {
                            Views.Busy.SetBusy(false);
                            Globals.SetIsConnected(true);
                            NavigationService.Navigate(typeof(Views.MainPage));
                        }
                    }
                }
                catch (Exception exception)
                {
                    Windows.UI.Popups.MessageDialog md = null;

                    switch (SocketError.GetStatus(exception.HResult))
                    {
                        case SocketErrorStatus.Unknown:
                            md = new Windows.UI.Popups.MessageDialog("Unknown Error: " + exception.Message, "Error");
                            await md.ShowAsync();
                            break;
                        case SocketErrorStatus.ConnectionTimedOut:
                            md = new Windows.UI.Popups.MessageDialog("Connection Timed Out", "Error");
                            await md.ShowAsync();
                            break;
                        case SocketErrorStatus.HostNotFound:
                            md = new Windows.UI.Popups.MessageDialog("Camera Not Found", "Error");
                            await md.ShowAsync();
                            break;
                        default:
                            md = new Windows.UI.Popups.MessageDialog("Error in connect: " + exception.Message, "Error");
                            await md.ShowAsync();
                            break;
                    }
                }
                Views.Busy.SetBusy(false);
            }, () => !string.IsNullOrEmpty("Conectando")));
    }
}