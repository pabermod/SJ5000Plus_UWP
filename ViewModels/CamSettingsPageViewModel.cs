using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;

namespace SJ5000Plus.ViewModels
{
    public class CamSettingsPageViewModel : ViewModelBase
    {
        private string _video_resolution;
        public string video_resolution
        {
            get { return _video_resolution; }
            set { _video_resolution = value; base.RaisePropertyChanged(); }
        }
        private string _photo_size;
        public string photo_size
        {
            get { return _photo_size; }
            set { _photo_size = value; base.RaisePropertyChanged(); }
        }
        private ObservableCollection<string> _video_resolution_list;
        public ObservableCollection<string> video_resolution_list
        {
            get { return _video_resolution_list; }
            set { _video_resolution_list = value; base.RaisePropertyChanged(); }

        }
        private ObservableCollection<string> _photo_size_list;
        public ObservableCollection<string> photo_size_list
        {
            get { return _photo_size_list; }
            set { _photo_size_list = value; base.RaisePropertyChanged(); }
        }

        /// <summary>
        /// Returns the value of the property of the specified object
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
