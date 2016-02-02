using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocabulary.Messages
{
    /// <summary>
    /// User Message to get param values. Response is a CamGetParamMessage
    /// </summary>
    public class UserGetParamValuesMessage : UserMessage
    {
        /// <summary>
        /// Parameter asked for
        /// </summary>
        public string param { get; set; }

        /// <summary>
        /// Empty constructor
        /// </summary>
        public UserGetParamValuesMessage()
        {
            msg_id = 9;
            token = 0;
            param = string.Empty;
        }

        /// <summary>
        /// Constructor with initial data
        /// </summary>
        public UserGetParamValuesMessage(int token_value, string param_value)
        {
            msg_id = 9;
            token = token_value;
            param = param_value;
        }
    }
}
