using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocabulary.Messages
{
    public class CamStartCaptureMessage : Message
    {
        public const int msg_id_expected = 7;
        public const string photo_expected = "start_photo_capture";
        public const string video_expected = "start_video_record";

        public string type { get; set; }
    }
}
