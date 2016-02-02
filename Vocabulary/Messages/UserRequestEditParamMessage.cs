using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocabulary.Messages
{
    /// <summary>
    /// Message to request to edit param
    /// in case permission is not settable. Response is a CameraMessage
    /// </summary>
    public class UserRequestEditParamMessage : UserMessage
    {
        public UserRequestEditParamMessage()
        {
            token = 0;
            msg_id = 260;
        }

        public UserRequestEditParamMessage(int token_value)
        {
            token = token_value;
            msg_id = 260;
        }
    }
}
