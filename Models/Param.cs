using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;

namespace SJ5000Plus.Models
{
    public class Param : BindableBase
    {
        
        private string _permission;

        public string permission
        {
            get { return _permission; }
            set { Set(ref _permission, value); }
        }

        private string _currentValue;

        public string currentValue
        {
            get { return _currentValue; }
            set { Set(ref _currentValue, value); }
        }
        
        public ObservableCollection<string> Values { get; set; }

        
        public Param()
        {
            permission = string.Empty;
            currentValue = string.Empty;
            Values = new ObservableCollection<string>();
        }

        /// <summary>
        /// Add or update the Param with the specified value
        /// </summary>
        /// <param name="value">Value to be updated into the Param</param>
        /// <returns></returns>
        public void AddOrUpdate(string value)
        {
            if (value == null || value == string.Empty)
            {
                currentValue = string.Empty;
            }
            else if (Values.Count == 0)
            {
                Values.Add(value);
                currentValue = value;
            }
            else if (Values.Count == 1)
            {
                Values.Remove(currentValue);
                Values.Add(value);
                currentValue = value;
            }
            else if (currentValue != value)
            {
                // More than 1 value. Suppose the Collection is filled with
                // all the possible values
                currentValue = value;
            }
        }

    }
}
