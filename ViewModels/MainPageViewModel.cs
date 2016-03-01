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
    public class MainPageViewModel : ViewModelBase
    {
        private ICameraService Camera;
        string _ButtonText = string.Empty;
        public string ButtonText { get { return _ButtonText; } set { Set(ref _ButtonText, value); } }

        private string _ConnectionStatus = "No conectado";
        public string ConnectionStatus
        {
            get { return _ConnectionStatus; }
            set { Set(ref _ConnectionStatus, value); }
        }

        private int _Token = 0;
        public int Token
        {
            get { return _Token; }
            private set { Set(ref _Token, value); }
        }

        private bool _ProgressVisibility;

        public bool ProgressVisibility
        {
            get { return _ProgressVisibility; }
            set { Set(ref _ProgressVisibility, value); }
        }

        private bool _ButtonEnabled;

        public bool ButtonEnabled
        {
            get { return _ButtonEnabled; }
            set { Set(ref _ButtonEnabled, value); }
        }

        private bool _CameraButtonsEnabled;

        public bool CameraButtonsEnabled
        {
            get { return _CameraButtonsEnabled; }
            set { Set(ref _CameraButtonsEnabled, value); }
        }

        public MainPageViewModel()
        {
            ButtonEnabled = true;
            ProgressVisibility = false;
            CameraButtonsEnabled = false;
            ButtonText = "Conectar";
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                ButtonText = "Designtime value";
                _Token = 1;
                ConnectionStatus = "Conectado";
            }
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            Camera = (App.Current as App).Camera;
            return Task.CompletedTask;
        }

        public override Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {
            (App.Current as App).Camera = Camera as CameraService;
            return Task.CompletedTask;
        }

        public override Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            return Task.CompletedTask;
        }

        public ICommand MyButtonClickCommand
        {
            get { return new DelegateCommand<object>(ConnectButtonClick, CanExecuteClick); }
        }

        private bool CanExecuteClick(object context)
        {
            //this is called to evaluate whether ConnectButtonClick can be called
            if (ButtonEnabled)
                return true;
            else
                return false;
        }

        private async void ConnectButtonClick(object context)
        {
            //this is called when the button is clicked
            ButtonEnabled = false;
            ProgressVisibility = true;
            if (Globals.isConnected)
            {
                if (await Camera.Disconnect())
                {
                    Globals.SetIsConnected(false);
                    ConnectionStatus = "Desconectado";
                    ButtonText = "Conectar";
                    CameraButtonsEnabled = false;
                }
            }
            else
            {
                if (await Camera.Connect())
                {
                    await Camera.GetToken();
                    Token = Camera.token;
                    Globals.SetIsConnected(true);
                    ConnectionStatus = "Conectado";
                    ButtonText = "Desconectar";
                    CameraButtonsEnabled = true;
                }
            }
            ProgressVisibility = false;
            ButtonEnabled = true;
        }

        public ICommand PhotoButtonClickCommand
        {
            get { return new DelegateCommand<object>(PhotoButtonClick, CanExecutePhotoClick); }
        }

        private bool CanExecutePhotoClick(object context)
        {
            //this is called to evaluate whether PhotoButtonClick can be called
            return true;
        }

        private async void PhotoButtonClick(object context)
        {
            ButtonEnabled = false;
            CameraButtonsEnabled = false;
            ProgressVisibility = true;
            string photo_location = await Camera.TakePhoto();
            ProgressVisibility = false;
            ConnectionStatus = photo_location;
            CameraButtonsEnabled = true;
            ButtonEnabled = true;
        }


        public void GotoDetailsPage() =>
            NavigationService.Navigate(typeof(Views.DetailPage), ButtonText);

        public void GotoSettings() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 0);

        public void GotoPrivacy() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 1);

        public void GotoAbout() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 2);

    }
}

