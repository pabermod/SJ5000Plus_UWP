using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocabulary.Messages
{
    /// <summary>
    /// Simple User Message
    /// </summary>
    public class UserMessage : Message
    {
        public int token { get; set; }

        public UserMessage()
        {
            token = 0;
            msg_id = 0;
        }

        public UserMessage(int msg_id_value, int token_value)
        {
            msg_id = msg_id_value;
            token = token_value;
        }
    }
}
