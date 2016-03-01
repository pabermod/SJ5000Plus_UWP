using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using SJ5000Plus.ViewModels;
using System.Collections.ObjectModel;

// La plantilla de elemento Página en blanco está documentada en http://go.microsoft.com/fwlink/?LinkId=234238

namespace SJ5000Plus.Views
{
    /// <summary>
    /// Una página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class CamSettingsPage : Page
    {
        //private Helpers _Helper;
        public CamSettingsPage()
        {
            InitializeComponent();

            //_Helper = new Helpers();
        }

        
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            //var index = Template10.Services.SerializationService.SerializationService
            //    .Json.Deserialize<int>(e.Parameter?.ToString());
            //MyPivot.SelectedIndex = index;
        }
        

        private async void DropDownOpened(object sender, object e)
        {
            //ViewModel.PopulateValues(sender as ComboBox);
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Check WiFi Connection
            //_isWiFiOn = await _Helper.CheckRadio();
            /*
            _isConnected = await _Helper.CheckWiFi();
            if (_isConnected)
            {
                Globals.SetIsConnected(true);
            }

            // Check Connection
            if (Globals.isConnected)
            {
                _Camera = new Camera(Globals.CameraIP, Globals.CameraPort);
            }
            else
            {
                _Helper.WifiSettingsDialog("No conectado a la camara");
            }
            if (_Camera != null)
            {
                _isConnected = await _Camera.Connect();
                if (_isConnected)
                {
                    await _Camera.GetToken();
                    await _Camera.PopulateSettings(ViewModel);
                }
            }   
            */       
        }
    }
}
