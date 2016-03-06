using SJ5000Plus.Services.CameraServices;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using System;
using Template10.Mvvm;
using Template10.Services.NavigationService;
using Windows.Graphics.Display;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SJ5000Plus.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private bool _CameraButtonsEnabled;
        private string _ConnectionStatus = "No conectado";
        private Visibility _ProgressVisibility;
        private ICameraService Camera;
        private Windows.UI.Xaml.Controls.Symbol _videoIcon;
        private bool _isStatusBarPresent;
        private Windows.UI.ViewManagement.StatusBar _StatusBar;

        public MainPageViewModel()
        {
            ProgressVisibility = Visibility.Collapsed;
            CameraButtonsEnabled = false;
            VideoIcon = Symbol.Video;
            _isStatusBarPresent = Windows.Foundation.Metadata.ApiInformation
                .IsTypePresent("Windows.UI.ViewManagement.StatusBar");
            if (_isStatusBarPresent)
            {
                _StatusBar = Windows.UI.ViewManagement.StatusBar.GetForCurrentView();
            }
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                ConnectionStatus = "Conectado";
            }
        }

        public Symbol VideoIcon
        {
            get { return _videoIcon; }
            set { Set(ref _videoIcon, value); }
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

        public ICommand VideoButtonClickCommand
        {
            get { return new DelegateCommand<object>(VideoButtonClick, CanExecutePhotoClick); }
        }

        public Visibility ProgressVisibility
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
            (App.Current as App).Camera = Camera;
            return Task.CompletedTask;
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            Camera = (App.Current as App).Camera;
            // Clear History so ConnectPage is never accessed through Navigation History
            NavigationService.ClearHistory();

            // Any orientation
            DisplayInformation.AutoRotationPreferences = DisplayOrientations.None;
            Views.Shell.HamburgerMenu.IsFullScreen = false;
            
            if (Camera.isConnected)
            {
                CameraButtonsEnabled = true;
            }
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
            ConnectionStatus = "Taking Photo...";
            if (_isStatusBarPresent)
            {
                _StatusBar.ProgressIndicator.Text = "Taking Photo...";
                await _StatusBar.ProgressIndicator.ShowAsync();
            }
            ProgressVisibility = Visibility.Visible;

            string photo_location = await Camera.TakePhoto();   
                    
            if (photo_location != null)
            {
                ConnectionStatus = "Photo Taken: " + photo_location;
            }
            await _StatusBar.ProgressIndicator.HideAsync();
            CameraButtonsEnabled = true;
            ProgressVisibility = Visibility.Collapsed;
        }

        private async void VideoButtonClick(object context)
        {
            CameraButtonsEnabled = false;
            if (_isStatusBarPresent)
                await _StatusBar.ProgressIndicator.ShowAsync();
            else
                ProgressVisibility = Visibility.Visible;
            if (Camera.isRecording)
            {
                string photo_location = await Camera.StopVideo();
                if (photo_location != null)
                {
                    if (_isStatusBarPresent)
                    {
                        _StatusBar.ProgressIndicator.Text = "Video Taken: " + photo_location;
                        await _StatusBar.ProgressIndicator.ShowAsync();
                    }
                    ConnectionStatus = photo_location;
                }
                VideoIcon = Windows.UI.Xaml.Controls.Symbol.Video;
                Camera.isRecording = false;
            }
            else
            {
                await Camera.StartVideo();
                VideoIcon = Windows.UI.Xaml.Controls.Symbol.Stop;
                Camera.isRecording = true;
            }        
            CameraButtonsEnabled = true;
            if (_isStatusBarPresent)
                await _StatusBar.ProgressIndicator.HideAsync();
            else
                ProgressVisibility = Visibility.Collapsed;
        }
    }
}