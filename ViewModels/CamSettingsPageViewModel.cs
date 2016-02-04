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
using Windows.UI.Xaml.Controls;

namespace SJ5000Plus.ViewModels
{
    public class CamSettingsPageViewModel : ViewModelBase
    {
        private ICameraService _camService;
        private string _permission;

        /// <summary>
        /// Permission to set a Param
        /// </summary>
        public string permission
        {
            get { return _permission; }
            set { Set(ref _permission, value); }
        }

        public Models.Settings CurrentValues { get; set; }


        public ObservableCollection<string> video_resolution_list { get; set; }
        public ObservableCollection<string> video_quality_list { get; set; }
        public ObservableCollection<string> video_standard_list { get; set; }
        public ObservableCollection<string> video_stamp_list { get; set; }
        public ObservableCollection<string> timelapse_video_list { get; set; }
        public ObservableCollection<string> loop_record_list { get; set; }
        public ObservableCollection<string> motion_detection_list { get; set; }

        public ObservableCollection<string> photo_size_list { get; set; }
        public ObservableCollection<string> photo_stamp_list { get; set; }
        public ObservableCollection<string> photo_quality_list { get; set; }
        //public ObservableCollection<string> timelapse_photo_list { get; set; }
        public ObservableCollection<string> selfie_photo_list { get; set; }
        public ObservableCollection<string> burst_photo_list { get; set; }
        public ObservableCollection<string> autoshoot_photo_list { get; set; }

        public ObservableCollection<string> status_led_switch_list { get; set; }
        public ObservableCollection<string> delay_pwroff_list { get; set; }
        public ObservableCollection<string> rotate_image_list { get; set; }
        public ObservableCollection<string> mic_vol_list { get; set; }
        public ObservableCollection<string> language_list { get; set; }
        public ObservableCollection<string> date_disp_fmt_list { get; set; }
        public ObservableCollection<string> auto_bkl_off_list { get; set; }
        public ObservableCollection<string> auto_pwr_off_list { get; set; }
        public ObservableCollection<string> light_freq_list { get; set; }
        public ObservableCollection<string> meter_mode_list { get; set; }

        /// <summary>
        /// ViewModel Constructor. Initializes the camera service, the lists and populates them.
        /// </summary>
        public CamSettingsPageViewModel()
        {
            // Set the Camera Service
            _camService = new FakeCameraService();

            // Initialise the lists
            video_resolution_list = new ObservableCollection<string>();
            video_quality_list = new ObservableCollection<string>();
            video_standard_list = new ObservableCollection<string>();
            video_stamp_list = new ObservableCollection<string>();
            timelapse_video_list = new ObservableCollection<string>();
            loop_record_list = new ObservableCollection<string>();
            motion_detection_list = new ObservableCollection<string>();
            photo_size_list = new ObservableCollection<string>();
            photo_stamp_list = new ObservableCollection<string>();
            photo_quality_list = new ObservableCollection<string>();
            //timelapse_photo_list = new ObservableCollection<string>();
            selfie_photo_list = new ObservableCollection<string>();
            burst_photo_list = new ObservableCollection<string>();
            autoshoot_photo_list = new ObservableCollection<string>();
            status_led_switch_list = new ObservableCollection<string>();
            delay_pwroff_list = new ObservableCollection<string>();
            rotate_image_list = new ObservableCollection<string>();
            mic_vol_list = new ObservableCollection<string>();
            language_list = new ObservableCollection<string>();
            date_disp_fmt_list = new ObservableCollection<string>();
            auto_bkl_off_list = new ObservableCollection<string>();
            auto_pwr_off_list = new ObservableCollection<string>();
            light_freq_list = new ObservableCollection<string>();
            meter_mode_list = new ObservableCollection<string>();

            // Populate all list with the current value
            PopulateAllSettings();
        }

        /// <summary>
        /// Populate the lists with the current values
        /// </summary>
        private void PopulateAllSettings()
        {
            // Get the current values
            var task = _camService.GetCurrentValues();
            task.Wait();
            CurrentValues = task.Result;

            // Add the current value to the lists
            video_resolution_list.Add(CurrentValues.video_resolution);
            video_standard_list.Add(CurrentValues.video_standard);
            video_quality_list.Add(CurrentValues.video_quality);
            video_stamp_list.Add(CurrentValues.video_stamp);
            timelapse_video_list.Add(CurrentValues.timelapse_video);
            loop_record_list.Add(CurrentValues.loop_record);
            motion_detection_list.Add(CurrentValues.motion_detec_video);

            photo_size_list.Add(CurrentValues.photo_size);
            photo_stamp_list.Add(CurrentValues.photo_stamp);
            photo_quality_list.Add(CurrentValues.photo_quality);
            selfie_photo_list.Add(CurrentValues.selfie_photo);
            burst_photo_list.Add(CurrentValues.burst_photo);
            autoshoot_photo_list.Add(CurrentValues.autoshoot_photo);
        }

        public async void PopulateValues(ComboBox CBox)
        {
            if (CBox.Items.Count == 1)
            {
                string currentValue = CBox.SelectedItem.ToString();

                // To avoid ComboBox closing:
                // Deselect the current Item
                CBox.SelectedItem = null;

                //Get the Values
                CamGetParamValuesMessage Param = await _camService.GetParamValues(CBox.Name);

                // Get the collection
                ObservableCollection<string> Source = (ObservableCollection<string>)GetPropertyValue(CBox.Name);

                // Update it
                foreach (var item in Param.options)
                {
                    Source.Add(item);
                }

                // Remove the first one (so it isn't repeated)
                Source.RemoveAt(0);

                // Select the value again
                CBox.SelectedItem = currentValue;

                _permission = Param.permission;

                // Now set the handler for SelectionChanged
                //CBox.SelectionChanged += DropDownSelectionChanged;
            }
        }

        /// <summary>
        /// Returns the value of the property of the specified property;
        /// </summary>
        public object GetPropertyValue(string propertyName)
        {
            try
            {
                return this.GetType().GetProperty(propertyName).GetValue(this, null);
            }
            catch { return null; }
        }
    }
}
