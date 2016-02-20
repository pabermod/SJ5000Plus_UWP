using SJ5000Plus.Services.CameraServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using Vocabulary;
using Vocabulary.Messages;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Template10.Services.NavigationService;

namespace SJ5000Plus.ViewModels
{
    public class CamSettingsPageViewModel : ViewModelBase
    {
        public ICameraService Camera;
        public Models.Settings Settings { get; set; }


        /// <summary>
        /// ViewModel Constructor. Initializes the camera service, the lists and populates them.
        /// </summary>
        public CamSettingsPageViewModel()
        {
            //_camService = new FakeCameraService();
            Settings = new Models.Settings();
            //PopulateAllSettings();
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {           
            if (parameter != null)
            {
                Camera = parameter as CameraService;
            }
            await PopulateAllSettings();
        }

        /// <summary>
        /// Populate the lists with the current values
        /// </summary>
        private async Task PopulateAllSettings()
        {
            // Get the current values
            Vocabulary.Settings CurrentValues = await (App.Current as App).Camera.GetCurrentValues();

            // Populate the current value and the lists
            Settings.video_resolution.Values.Add(CurrentValues.video_resolution);
            Settings.video_standard.Values.Add(CurrentValues.video_standard);
            Settings.video_quality.Values.Add(CurrentValues.video_quality);
            Settings.video_stamp.Values.Add(CurrentValues.video_stamp);
            Settings.timelapse_video.Values.Add(CurrentValues.timelapse_video);
            Settings.loop_record.Values.Add(CurrentValues.loop_record);
            Settings.motion_detec_video.Values.Add(CurrentValues.motion_detec_video);

            Settings.video_resolution.currentValue = CurrentValues.video_resolution;
            Settings.video_standard.currentValue = CurrentValues.video_standard;
            Settings.video_quality.currentValue = CurrentValues.video_quality;
            Settings.video_stamp.currentValue = CurrentValues.video_stamp;
            Settings.timelapse_video.currentValue = CurrentValues.timelapse_video;
            Settings.loop_record.currentValue = CurrentValues.loop_record;
            Settings.motion_detec_video.currentValue = CurrentValues.motion_detec_video;
            Settings.photo_size.currentValue = CurrentValues.photo_size;
            Settings.photo_stamp.currentValue = CurrentValues.photo_stamp;
            Settings.photo_quality.currentValue = CurrentValues.photo_quality;
            Settings.selfie_photo.currentValue = CurrentValues.selfie_photo;
            Settings.burst_photo.currentValue = CurrentValues.burst_photo;
            Settings.autoshoot_photo.currentValue = CurrentValues.autoshoot_photo;


            Settings.photo_size.Values.Add(CurrentValues.photo_size);
            Settings.photo_stamp.Values.Add(CurrentValues.photo_stamp);
            Settings.photo_quality.Values.Add(CurrentValues.photo_quality);
            Settings.selfie_photo.Values.Add(CurrentValues.selfie_photo);
            Settings.burst_photo.Values.Add(CurrentValues.burst_photo);
            Settings.autoshoot_photo.Values.Add(CurrentValues.autoshoot_photo);
            
        }

        /// <summary>
        /// Populate a combobox with the possible values
        /// </summary>
        public async void PopulateValues(ComboBox CBox)
        {
            if (CBox.Items.Count == 1)
            {
                string currentValue = CBox.SelectedItem.ToString();

                // To avoid ComboBox closing:
                // Deselect the current Item
                CBox.SelectedItem = null;

                //Get the Values
                CamGetParamValuesMessage Param = await (App.Current as App).Camera.GetParamValues(CBox.Name);

                // Get the Param and update its values with reflexion
                Models.Param parameter = (Models.Param)GetParamByName(CBox.Name);

                foreach (var item in Param.options)
                {
                    parameter.Values.Add(item);
                }
                
                // Remove the first one (so it isn't repeated)
                parameter.Values.RemoveAt(0);

                // Select the value again
                parameter.currentValue = currentValue;

                parameter.permission = Param.permission;

                // Now set the handler for SelectionChanged
                CBox.SelectionChanged += DropDownSelectionChanged;
            }
        }

        private async void DropDownSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox CBox = sender as ComboBox;
            Models.Param parameter = (Models.Param)GetParamByName(CBox.Name);
            await Camera.SetParamValue(CBox.Name, CBox.SelectedItem.ToString(), parameter.permission);
        }

        /// <summary>
        /// Returns the specified Param object by its name (from a Settings object)
        /// </summary>
        private object GetParamByName(string propertyName)
        {
            try
            {
                return this.Settings.GetType().GetProperty(propertyName).GetValue(this.Settings, null);
            }
            catch { return null; }
        }
    }
}
