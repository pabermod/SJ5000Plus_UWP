using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocabulary.Messages
{
    /// <summary>
    /// Simple Message
    /// </summary>
    public class Message
    {
        public int msg_id { get; set; }

        /// <summary>
        /// Constructor without data
        /// </summary>
        public Message()
        {
            msg_id = 0;
        }

        /// <summary>
        /// Constructor with initial data
        /// </summary>
        public Message(int msg_id_value)
        {
            msg_id = msg_id_value;
        }
    }
}
