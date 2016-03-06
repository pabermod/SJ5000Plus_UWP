using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocabulary.Messages
{
    public class UserStopVideoMessage : UserMessage
    {
        public UserStopVideoMessage()
        {
            token = 0;
            msg_id = 514;
        }

        public UserStopVideoMessage(int token_value)
        {
            token = token_value;
            msg_id = 514;
        }
    }
}
