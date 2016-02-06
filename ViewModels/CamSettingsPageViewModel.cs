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
        public Models.Settings Settings { get; set; }

        /// <summary>
        /// ViewModel Constructor. Initializes the camera service, the lists and populates them.
        /// </summary>
        public CamSettingsPageViewModel()
        {
            _camService = new FakeCameraService();
            Settings = new Models.Settings();
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
            Vocabulary.Settings CurrentValues = task.Result;

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

            /*
            photo_size_list.Add(CurrentValues.photo_size);
            photo_stamp_list.Add(CurrentValues.photo_stamp);
            photo_quality_list.Add(CurrentValues.photo_quality);
            selfie_photo_list.Add(CurrentValues.selfie_photo);
            burst_photo_list.Add(CurrentValues.burst_photo);
            autoshoot_photo_list.Add(CurrentValues.autoshoot_photo);
            */
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
                CamGetParamValuesMessage Param = await _camService.GetParamValues(CBox.Name);

                // Get the Param and update its values with reflexion
                Models.Param parameter = (Models.Param)GetPropertyValue(CBox.Name);

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
                //CBox.SelectionChanged += DropDownSelectionChanged;

            }
        }

        /// <summary>
        /// Returns the value of the property of Settings
        /// </summary>
        private object GetPropertyValue(string propertyName)
        {
            try
            {
                return this.Settings.GetType().GetProperty(propertyName).GetValue(this.Settings, null);
            }
            catch { return null; }
        }

    }
}
