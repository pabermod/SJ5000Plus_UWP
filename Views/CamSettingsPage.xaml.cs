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
        public CamSettingsPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var index = Template10.Services.SerializationService.SerializationService
                .Json.Deserialize<int>(e.Parameter?.ToString());
            MyPivot.SelectedIndex = index;
        }

        private async void DropDownOpened(object sender, object e)
        {
            ComboBox CBox = sender as ComboBox;
            if (CBox.Items.Count == 1)
            {
                string currentValue = CBox.SelectedItem.ToString();

                // To avoid ComboBox closing:
                // Deselect the current Item
                CBox.SelectedItem = null;

                //Populate Values
                await CameraFacade.PopulateValues(ViewModel, CBox.Name);

                // Select the value again
                CBox.SelectedItem = currentValue;

                // Now set the handler for SelectionChanged
                //CBox.SelectionChanged += DropDownSelectionChanged;             
            }
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await CameraFacade.PopulateSettings(ViewModel);
        }
    }
}
