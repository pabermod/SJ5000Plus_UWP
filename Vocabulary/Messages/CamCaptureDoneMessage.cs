using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocabulary.Messages
{
    public class CamCaptureDoneMessage : Message
    {
        public const int msg_id_expected = 7;
        public const string photo_expected = "photo_taken";
        public const string video_expected = "video_record_complete";

        public string type { get; set; }
        public string param { get; set; }
    }
}
