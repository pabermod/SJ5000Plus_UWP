using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocabulary.Messages
{
    /// <summary>
    /// Message received from the Camera when asked for param values
    /// </summary>
    public class CamGetParamValuesMessage : CamParamMessage
    {
        /// <summary>
        /// Indicates if the user has permission to edit the param
        /// </summary>
        public string permission { get; set; }
        /// <summary>
        /// Contains the possible values for this param
        /// </summary>
        public List<string> options { get; set; }

        /// <summary>
        /// Empty Constructor
        /// </summary>
        public CamGetParamValuesMessage()
        {
            param = string.Empty;
            rval = 0;
            msg_id = 0;
            permission = string.Empty;
            options = null;
        }

        /// <summary>
        /// Constructor with data
        /// </summary>
        public CamGetParamValuesMessage(int msg_id_value, int rval_value, string param_value, 
            string permission_value, List<string> options_value)
        {
            msg_id = msg_id_value;
            rval = rval_value;
            param = param_value;
            permission = permission_value;
            options = options_value;
        }
    }
}
