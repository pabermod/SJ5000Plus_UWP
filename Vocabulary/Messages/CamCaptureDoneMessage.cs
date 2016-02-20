using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocabulary.Messages
{
    public class CamCaptureDoneMessage : CamStartCaptureMessage
    {
        public static new string photo_expected = "photo_taken";
        public static new string video_expected = "video_record_complete";

        public string param { get; set; }
    }
}
