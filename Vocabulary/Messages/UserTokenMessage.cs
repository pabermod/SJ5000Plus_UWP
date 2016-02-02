using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocabulary.Messages
{
    /// <summary>
    /// Message to get the token for the next requests.
    /// The response is a CamTokenMessage
    /// </summary>
    public class UserTokenMessage : UserMessage
    {
        public UserTokenMessage()
        {
            token = 0;
            msg_id = 257;
        }
    }
}
