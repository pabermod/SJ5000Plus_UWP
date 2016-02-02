using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocabulary.Messages
{
    /// <summary>
    /// Simple Camera Message
    /// </summary>
    public class CameraMessage : Message
    {
        public int rval { get; set; }

        /// <summary>
        /// Empty constructor
        /// </summary>
        public CameraMessage()
        {
            rval = 0;
            msg_id = 0;
        }

        /// <summary>
        /// Constructor with initial data.
        /// </summary>
        public CameraMessage(int msg_id_value, int rval_value)
        {
            msg_id = msg_id_value;
            rval = rval_value;
        }
    }
}
