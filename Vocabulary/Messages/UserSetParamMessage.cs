using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocabulary.Messages
{
    /// <summary>
    /// Message to be sent to change a specific param. 
    /// Response is a CamParamMessage with msg_id the same as sent.
    /// Param is the value set.
    /// </summary>
    public class UserSetParamMessage : UserGetParamValuesMessage
    {
        /// <summary>
        /// Parameter to be changed
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// User Message to set a camera option.
        /// </summary>
        public UserSetParamMessage()
        {
            msg_id = 2;
            token = 0;
            type = string.Empty;
            param = string.Empty;
        }

        /// <summary>
        /// User Message to set a camera option
        /// </summary>
        /// <param name="type_value">The option to be set</param>
        /// <param name="param_value">The value to be set to this option</param>
        public UserSetParamMessage(int token_value, string type_value, string param_value)
        {
            msg_id = 2;
            token = token_value;
            type = type_value;
            param = param_value;
        }
    }
}
