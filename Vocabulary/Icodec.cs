using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocabulary
{
    public abstract class Icodec<T>
    {
        /// <summary>
        /// Abstract Function to encode a message
        /// </summary>
        public abstract Task<string> Encode(T message);

        /// <summary>
        /// Abstract Function to decode a message
        /// </summary>
        public abstract Task<T> Decode(string source);
    }
}
