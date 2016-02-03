using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vocabulary;

namespace SJ5000Plus.Models
{

    public class Camera : INotifyPropertyChanged
    {
        public enum PermissionSetParam
        {
            Readonly,
            Settable
        };

        public Settings CurrentValues { get; set; }

        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }   
}
