using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocabulary.Messages
{
    public class UserStartVideoMessage : UserMessage
    {
        // ->{"msg_id":513,"token":1}
        public UserStartVideoMessage()
        {
            token = 0;
            msg_id = 513;
        }

        public UserStartVideoMessage(int token_value)
        {
            token = token_value;
            msg_id = 513;
        }
    }
}
