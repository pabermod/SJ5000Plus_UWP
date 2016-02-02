using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocabulary.Messages
{
    /// <summary>
    /// Message received from camera that includes a string
    /// </summary>
    public class CamParamMessage : CameraMessage
    {
        public string param { get; set; }

        /// <summary>
        /// Empty constructor
        /// </summary>
        public CamParamMessage()
        {
            param = string.Empty;
            msg_id = 0;
            rval = 0;
        }

        /// <summary>
        /// Constructor with data
        /// </summary>
        public CamParamMessage(int msg_id_value, int rval_value, string param_value)
        {
            msg_id = msg_id_value;
            rval = rval_value;
            param = param_value;
        }
    }
}
