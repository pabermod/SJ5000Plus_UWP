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
            Settings = new Models.Settings();
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            Camera = (App.Current as App).Camera;
            await PopulateAllSettings();
        }

        public override Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {
            (App.Current as App).Camera = Camera;
            return Task.CompletedTask;
        }

        /// <summary>
        /// Populate the lists with the current values
        /// </summary>
        private async Task PopulateAllSettings()
        {
            // Get the current values
            Vocabulary.Settings CurrentValues = await Camera.GetCurrentValues();

            // Populate the current value and the lists
            Models.Settings.AddOrUpdate(Settings.autoshoot_photo, CurrentValues.autoshoot_photo);
            Models.Settings.AddOrUpdate(Settings.auto_bkl_off, CurrentValues.auto_bkl_off);
            Models.Settings.AddOrUpdate(Settings.auto_pwr_off, CurrentValues.auto_pwr_off);
            Models.Settings.AddOrUpdate(Settings.burst_photo, CurrentValues.burst_photo);
            Models.Settings.AddOrUpdate(Settings.buzzer, CurrentValues.buzzer);
            Models.Settings.AddOrUpdate(Settings.cardvr_switch, CurrentValues.cardvr_switch);
            Models.Settings.AddOrUpdate(Settings.date_disp_fmt, CurrentValues.date_disp_fmt);
            Models.Settings.AddOrUpdate(Settings.delay_pwroff, CurrentValues.delay_pwroff);
            Models.Settings.AddOrUpdate(Settings.language, CurrentValues.language);
            Models.Settings.AddOrUpdate(Settings.light_freq, CurrentValues.light_freq);
            Models.Settings.AddOrUpdate(Settings.loop_record, CurrentValues.loop_record);
            Models.Settings.AddOrUpdate(Settings.meter_mode, CurrentValues.meter_mode);
            Models.Settings.AddOrUpdate(Settings.mic_vol, CurrentValues.mic_vol);
            Models.Settings.AddOrUpdate(Settings.motion_detec_video, CurrentValues.motion_detec_video);
            Models.Settings.AddOrUpdate(Settings.osd_switch, CurrentValues.osd_switch);
            Models.Settings.AddOrUpdate(Settings.photo_quality, CurrentValues.photo_quality);
            Models.Settings.AddOrUpdate(Settings.photo_size, CurrentValues.photo_size);
            Models.Settings.AddOrUpdate(Settings.photo_stamp, CurrentValues.photo_stamp);
            Models.Settings.AddOrUpdate(Settings.rotate_image, CurrentValues.rotate_image);
            Models.Settings.AddOrUpdate(Settings.save_low_resolution_clip, CurrentValues.save_low_resolution_clip);
            Models.Settings.AddOrUpdate(Settings.selfie_photo, CurrentValues.selfie_photo);
            Models.Settings.AddOrUpdate(Settings.status_led_switch, CurrentValues.status_led_switch);
            Models.Settings.AddOrUpdate(Settings.stream_out_type, CurrentValues.stream_out_type);
            Models.Settings.AddOrUpdate(Settings.timelapse_photo, CurrentValues.timelapse_photo);
            Models.Settings.AddOrUpdate(Settings.timelapse_video, CurrentValues.timelapse_video);
            Models.Settings.AddOrUpdate(Settings.video_quality, CurrentValues.video_quality);
            Models.Settings.AddOrUpdate(Settings.video_resolution, CurrentValues.video_resolution);
            Models.Settings.AddOrUpdate(Settings.video_stamp, CurrentValues.video_stamp);
            Models.Settings.AddOrUpdate(Settings.video_standard, CurrentValues.video_standard);
            Models.Settings.AddOrUpdate(Settings.wifi_led_switch, CurrentValues.wifi_led_switch);
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
                CamGetParamValuesMessage Param = await Camera.GetParamValues(CBox.Name);

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
