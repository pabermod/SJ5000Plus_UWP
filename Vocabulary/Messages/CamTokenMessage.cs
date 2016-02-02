using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocabulary.Messages
{
    /// <summary>
    /// Message received from camera when asked for the token.
    /// </summary>
    public class CamTokenMessage : CameraMessage
    {
        /// <summary>
        /// Token received from the camera
        /// </summary>
        public int param { get; set; }

        /// <summary>
        /// Empty constructor
        /// </summary>
        public CamTokenMessage()
        {
            param = 0;
            msg_id = 257;
            rval = 0;
        }

        /// <summary>
        /// Constructor with initial data
        /// </summary>
        public CamTokenMessage(int msg_id_value, int rval_value, int param_value)
        {
            msg_id = msg_id_value;
            rval = rval_value;
            param = param_value;
        }
    }
}
