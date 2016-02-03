using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vocabulary.Messages;

namespace Vocabulary.MessageCodecs
{
    public class CamSettingsMessageCodec : JsonMessageCodec<CamSettingsMessage>
    {
        /// <summary>
        /// Decode the string to a message
        /// </summary>
        public override async Task<CamSettingsMessage> Decode(string source)
        {
            //CamSettingsMessage Message = await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<T>(source));
            var settings = new JsonSerializerSettings { Converters = new[] { new ArrayToObjectConverter<Settings>() } };
            CamSettingsMessage Message = await Task.Factory.StartNew(
                () => JsonConvert.DeserializeObject<CamSettingsMessage>(source,settings));

            return Message;
        }
    }
}
