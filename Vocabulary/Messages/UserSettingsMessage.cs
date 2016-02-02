using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocabulary.Messages
{
    /// <summary>
    /// Message to get all camera setings and the current value
    /// </summary>
    public class UserSettingsMessage : UserMessage
    {
        /// <summary>
        /// Empty constructor
        /// </summary>
        public UserSettingsMessage()
        {
            token = 0;
            msg_id = 3;
        }

        /// <summary>
        /// Constructor with initial data.
        /// </summary>
        public UserSettingsMessage(int token_value)
        {
            token = token_value;
            msg_id = 3;
        }
    }
}
