using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Vocabulary.Messages
{
    /// <summary>
    /// Message received from camera when asked for all the settings
    /// </summary>
    public class CamSettingsMessage : CameraMessage
    {
        /// <summary>
        /// List of all params with the current value
        /// </summary>
        public Settings param { get; set; }
  
        /// <summary>
        /// Empty constructor
        /// </summary>
        
        public CamSettingsMessage()
        {
            msg_id = 0;
            rval = 0;
            param = null;
        }

        /// <summary>
        /// Constructor with initial data
        /// </summary>
        public CamSettingsMessage(int msg_id_value, int rval_value, Settings param_value)
        {
            msg_id = msg_id_value;
            rval = rval_value;
            param = param_value;
        }
        
    }

}
