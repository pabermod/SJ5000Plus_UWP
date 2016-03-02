using SJ5000Plus.Services.CameraServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;

namespace SJ5000Plus.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private bool _CameraButtonsEnabled;
        private string _ConnectionStatus = "No conectado";
        private bool _ProgressVisibility;
        private ICameraService Camera;

        public MainPageViewModel()
        {
            ProgressVisibility = false;
            CameraButtonsEnabled = false;
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                ConnectionStatus = "Conectado";
            }
        }

        public bool CameraButtonsEnabled
        {
            get { return _CameraButtonsEnabled; }
            set { Set(ref _CameraButtonsEnabled, value); }
        }

        public string ConnectionStatus
        {
            get { return _ConnectionStatus; }
            set { Set(ref _ConnectionStatus, value); }
        }

        public ICommand PhotoButtonClickCommand
        {
            get { return new DelegateCommand<object>(PhotoButtonClick, CanExecutePhotoClick); }
        }

        public bool ProgressVisibility
        {
            get { return _ProgressVisibility; }
            set { Set(ref _ProgressVisibility, value); }
        }

        public void GotoAbout() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 2);

        public void GotoDetailsPage() =>
            NavigationService.Navigate(typeof(Views.DetailPage), ConnectionStatus);

        public void GotoPrivacy() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 1);

        public void GotoSettings() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 0);

        public override Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {
            (App.Current as App).Camera = Camera as CameraService;
            return Task.CompletedTask;
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            Views.Shell.HamburgerMenu.IsFullScreen = false;
            Camera = (App.Current as App).Camera;
            return Task.CompletedTask;
        }

        public override Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            return Task.CompletedTask;
        }

        private bool CanExecutePhotoClick(object context)
        {
            //this is called to evaluate whether PhotoButtonClick can be called
            return true;
        }

        private async void PhotoButtonClick(object context)
        {
            CameraButtonsEnabled = false;
            ProgressVisibility = true;
            string photo_location = await Camera.TakePhoto();
            ProgressVisibility = false;
            ConnectionStatus = photo_location;
            CameraButtonsEnabled = true;
        }
    }
}