using Template10.Mvvm;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using SJ5000Plus.Services.CameraServices;

namespace SJ5000Plus.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public CameraService Camera = null;
        string _Value = string.Empty;
        public string Value { get { return _Value; } set { Set(ref _Value, value); } }

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

        public MainPageViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                Value = "Designtime value";
                _Token = 1;
                ConnectionStatus = "Conectado";
            }
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            /*
            if (Camera == null)
            {
                Camera = new CameraService(Globals.CameraIP, Globals.CameraPort);
            }
           
            if (parameter != null)
            {
                Camera = parameter as CameraService;
            }
            */
            if (state.Any())
            {
                Value = state[nameof(Value)]?.ToString();
                state.Clear();
            }
            return Task.CompletedTask;
        }

        public override Task OnNavigatedFromAsync(IDictionary<string, object> state, bool suspending)
        {
            if (suspending)
            {
                state[nameof(Value)] = Value;
            }
            return Task.CompletedTask;
        }

        public override Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            args.Parameter = Camera;
            return Task.CompletedTask;
        }

        public async void Connect()
        {
            if (await (App.Current as App).Camera.Connect())
            {
                await (App.Current as App).Camera.GetToken();
                ConnectionStatus = "Conectado";
                Token = (App.Current as App).Camera.token;
            }
        }        

        public void GotoDetailsPage() =>
            NavigationService.Navigate(typeof(Views.DetailPage), Value);

        public void GotoSettings() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 0);

        public void GotoPrivacy() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 1);

        public void GotoAbout() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 2);

    }
}

