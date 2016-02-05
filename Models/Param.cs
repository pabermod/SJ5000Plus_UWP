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
      
    }
}
