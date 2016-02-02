using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SJ5000Plus.ViewModels;

namespace SJ5000Plus
{
    public class CameraFacade
    {
        public static async Task PopulateSettings(CamSettingsPageViewModel settings)
        {
            settings.video_resolution_list = new ObservableCollection<string>();
            settings.video_resolution_list.Add("Three");
            settings.video_resolution = "Three";

            settings.photo_size_list = new ObservableCollection<string>();
            settings.photo_size_list.Add("Four");
            settings.photo_size = "Four";
        }

        public static async Task PopulateValues(CamSettingsPageViewModel cameraSettings, string name)
        {
            ObservableCollection<string> Values = (ObservableCollection<string>)cameraSettings.GetPropertyValue(name);
            Values.Add("One");
            Values.Add("Two");
            Values.Add("Three");
            Values.Add("Four");
            Values.Add("Five");
            Values.Add("Six");

            // Remove the first one (so it isn't repeated)
            Values.RemoveAt(0);
        }
    }
}
