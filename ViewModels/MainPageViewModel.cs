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
        private bool _VideoButtonEnabled;
        private bool _PhotoButtonEnabled;
        private string _ConnectionStatus = "Conectado";
        private Visibility _ProgressVisibility;
        private ICameraService Camera;
        private Symbol _videoIcon;

        public MainPageViewModel()
        {
            ProgressVisibility = Visibility.Collapsed;
            VideoButtonEnabled = false;
            PhotoButtonEnabled = false;
            VideoIcon = Symbol.Video;
        }

        public Symbol VideoIcon
        {
            get { return _videoIcon; }
            set { Set(ref _videoIcon, value); }
        }

        public bool VideoButtonEnabled
        {
            get { return _VideoButtonEnabled; }
            set { Set(ref _VideoButtonEnabled, value); }
        }

        public bool PhotoButtonEnabled
        {
            get { return _PhotoButtonEnabled; }
            set { Set(ref _PhotoButtonEnabled, value); }
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
            
            if (Camera.isConnected)
            {
                VideoButtonEnabled = true;
                PhotoButtonEnabled = true;
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
            VideoButtonEnabled = false;
            PhotoButtonEnabled = false;
            ConnectionStatus = "Taking Photo...";
            ProgressVisibility = Visibility.Visible;

            string photo_location = await Camera.TakePhoto();   
                    
            if (photo_location != null)
            {
                ConnectionStatus = "Photo Taken: " + photo_location;
            }
            VideoButtonEnabled = true;
            PhotoButtonEnabled = true;
            ProgressVisibility = Visibility.Collapsed;
        }

        private async void VideoButtonClick(object context)
        {
            VideoButtonEnabled = false;
            PhotoButtonEnabled = false;
            ProgressVisibility = Visibility.Visible;
            if (Camera.isRecording)
            {
                ConnectionStatus = "Stopping Video...";
                string photo_location = await Camera.StopVideo();
                if (photo_location != null)
                {
                    ConnectionStatus = "Video Taken: " + photo_location;
                }
                VideoIcon = Symbol.Video;
                Camera.isRecording = false;
                ProgressVisibility = Visibility.Collapsed;
                PhotoButtonEnabled = true;
            }
            else
            {
                ConnectionStatus = "Taking Video...";
                await Camera.StartVideo();
                VideoIcon = Symbol.Stop;
                Camera.isRecording = true;
                ConnectionStatus = "Recording";
            }        
            VideoButtonEnabled = true;
            ProgressVisibility = Visibility.Collapsed;
        }
    }
}