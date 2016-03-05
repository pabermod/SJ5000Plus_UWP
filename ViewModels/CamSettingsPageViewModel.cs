using SJ5000Plus.Services.CameraServices;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Template10.Mvvm;
using Vocabulary;
using Vocabulary.Messages;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace SJ5000Plus.ViewModels
{
    public class CamSettingsPageViewModel : ViewModelBase
    {
        public ICameraService Camera;

        /// <summary>
        /// ViewModel Constructor. Initializes the camera service, the lists and populates them.
        /// </summary>
        public CamSettingsPageViewModel()
        {
            Settings = new Models.Settings();
        }

        public Models.Settings Settings { get; set; }

        public override Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {
            (App.Current as App).Camera = Camera;
            return Task.CompletedTask;
        }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            Camera = (App.Current as App).Camera;
            await PopulateAllSettings();
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

        /// <summary>
        /// Populate the lists with the current values
        /// </summary>
        private async Task PopulateAllSettings()
        {
            // Get the current values
            Vocabulary.Settings CurrentValues = await Camera.GetCurrentValues();

            // Populate the Params
            Settings.autoshoot_photo.AddOrUpdate(CurrentValues.autoshoot_photo);
            Settings.auto_bkl_off.AddOrUpdate(CurrentValues.auto_bkl_off);
            Settings.auto_pwr_off.AddOrUpdate(CurrentValues.auto_pwr_off);
            Settings.burst_photo.AddOrUpdate(CurrentValues.burst_photo);
            Settings.buzzer.AddOrUpdate(CurrentValues.buzzer);
            Settings.cardvr_switch.AddOrUpdate(CurrentValues.cardvr_switch);
            Settings.date_disp_fmt.AddOrUpdate(CurrentValues.date_disp_fmt);
            Settings.delay_pwroff.AddOrUpdate(CurrentValues.delay_pwroff);
            Settings.language.AddOrUpdate(CurrentValues.language);
            Settings.light_freq.AddOrUpdate(CurrentValues.light_freq);
            Settings.loop_record.AddOrUpdate(CurrentValues.loop_record);
            Settings.meter_mode.AddOrUpdate(CurrentValues.meter_mode);
            Settings.mic_vol.AddOrUpdate(CurrentValues.mic_vol);
            Settings.motion_detec_video.AddOrUpdate(CurrentValues.motion_detec_video);
            Settings.osd_switch.AddOrUpdate(CurrentValues.osd_switch);
            Settings.photo_quality.AddOrUpdate(CurrentValues.photo_quality);
            Settings.photo_size.AddOrUpdate(CurrentValues.photo_size);
            Settings.photo_stamp.AddOrUpdate(CurrentValues.photo_stamp);
            Settings.rotate_image.AddOrUpdate(CurrentValues.rotate_image);
            Settings.save_low_resolution_clip.AddOrUpdate(CurrentValues.save_low_resolution_clip);
            Settings.selfie_photo.AddOrUpdate(CurrentValues.selfie_photo);
            Settings.status_led_switch.AddOrUpdate(CurrentValues.status_led_switch);
            Settings.timelapse_photo.AddOrUpdate(CurrentValues.timelapse_photo);
            Settings.timelapse_video.AddOrUpdate(CurrentValues.timelapse_video);
            Settings.video_quality.AddOrUpdate(CurrentValues.video_quality);
            Settings.video_resolution.AddOrUpdate(CurrentValues.video_resolution);
            Settings.video_stamp.AddOrUpdate(CurrentValues.video_stamp);
            Settings.video_standard.AddOrUpdate(CurrentValues.video_standard);
            Settings.wifi_led_switch.AddOrUpdate(CurrentValues.wifi_led_switch);
        }
    }
}