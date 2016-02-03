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

        public string permission
        {
            get { return _permission; }
            set { Set(ref _permission, value); }
        }

        private Settings _CurrentValues;
        public Settings CurrentValues
        {
            get { return _CurrentValues; }
            set { Set(ref _CurrentValues, value); }
        }
        /*
        private string _video_resolution;
        public string video_resolution
        {
            get { return _video_resolution; }
            set { Set(ref _video_resolution, value); }
        }
        private string _photo_size;
        public string photo_size
        {
            get { return _photo_size; }
            set { Set(ref _photo_size, value); }
        }
        */
        public ObservableCollection<string> video_resolution_list { get; set; }
        public ObservableCollection<string> photo_size_list { get; set; }
        public ObservableCollection<string> video_standard_list { get; set; }

        public CamSettingsPageViewModel()
        {
            // Set the Camera Service
            _camService = new FakeCameraService();

            // Initialise the lists
            video_resolution_list = new ObservableCollection<string>();
            photo_size_list = new ObservableCollection<string>();
            video_standard_list = new ObservableCollection<string>();


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
            photo_size_list.Add(CurrentValues.photo_size);
            video_standard_list.Add(CurrentValues.video_standard);
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
