using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocabulary.Messages
{
    /// <summary>
    /// Message to get the current value of a Param.
    /// Response will be a CamGetParamValue
    /// </summary>
    public class UserGetParamValueMessage : UserMessage
    {
        public string type { get; set; }

        public UserGetParamValueMessage()
        {
            token = 0;
            msg_id = 1;
        }

        public UserGetParamValueMessage(string type_value, int token_value)
        {
            msg_id = 1;
            token = token_value;
            type = type_value;
        }
    }
}
