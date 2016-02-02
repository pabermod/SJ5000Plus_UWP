using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocabulary
{
    public abstract class JsonMessageCodec<T> : Icodec<T>
    {
        /// <summary>
        /// Encode the message to a string
        /// </summary>
        public override async Task<string> Encode(T message)
        {
            string messageString = await Task.Factory.StartNew(() => JsonConvert.SerializeObject(message));
            return messageString;
        }

        /// <summary>
        /// Decode the string to a message
        /// </summary>
        public override async Task<T> Decode(string source)
        {
            T Message = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<T>(source));
            return Message;
        }
    }
}
