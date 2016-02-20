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
        public CameraService Camera = null;
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

        public MainPageViewModel()
        {
            ButtonEnabled = true;
            ProgressVisibility = false;
            ButtonText = "Conectar";
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                ButtonText = "Designtime value";
                _Token = 1;
                ConnectionStatus = "Conectado";
            }
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {        
            if (parameter != null)
            {
                Camera = parameter as CameraService;
            }
            else if (Camera == null)
            {
                Camera = new CameraService(Globals.CameraIP, Globals.CameraPort);
            }
            if (state.Any())
            {
                ButtonText = state[nameof(ButtonText)]?.ToString();
                state.Clear();
            }
            return Task.CompletedTask;
        }

        public override Task OnNavigatedFromAsync(IDictionary<string, object> state, bool suspending)
        {
            if (suspending)
            {
                state[nameof(ButtonText)] = ButtonText;
            }
            return Task.CompletedTask;
        }

        public override Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            args.Parameter = Camera;
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
                    ConnectionStatus = "Desconectado";
                    Globals.SetIsConnected(false);
                    ConnectionStatus = "Desconectado";
                    ButtonText = "Conectar";
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
                }
            }
            ProgressVisibility = false;
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

