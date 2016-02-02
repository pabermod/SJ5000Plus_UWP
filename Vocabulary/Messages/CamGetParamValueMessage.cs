using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocabulary.Messages
{
    /// <summary>
    /// Message received from the Camera when asked for current param value
    /// </summary>
    public class CamGetParamValueMessage : CamParamMessage
    {
        /// <summary>
        /// Parameter asked for
        /// </summary>
        public string type { get; set; }

        public CamGetParamValueMessage()
        {
            rval = 0;
            msg_id = 1;
            type = string.Empty;
            param = string.Empty;
        }

        public CamGetParamValueMessage(int rval_value, string type_value, string param_value)
        {
            msg_id = 1;
            rval = rval_value;
            type = type_value;
            param = param_value;

        }
    }
}
