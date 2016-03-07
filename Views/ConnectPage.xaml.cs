using System;
using SJ5000Plus.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace SJ5000Plus.Views
{
    public sealed partial class ConnectPage : Page
    {
        public ConnectPage()
        {
            InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Disabled;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            ContextControl.SelectionChanged += ContextControl_SelectionChanged;
        }

        void ContextControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Make sure that the navigation buttons are updated by forcing focus to the FlipView
            FlipViewCamera.Focus(Windows.UI.Xaml.FocusState.Pointer);
        }

    }
}
