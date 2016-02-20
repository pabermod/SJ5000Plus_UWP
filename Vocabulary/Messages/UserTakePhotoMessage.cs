using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vocabulary.Messages
{
    // Take Photo
    // ->{"msg_id":769,"token":1}
    // <-{"rval":0,"msg_id":769}
    // <-{ "msg_id": 7, "type": "start_photo_capture" }
    // <-{ "msg_id": 7, "type": "photo_taken" ,"param":"/tmp/fuse_d/DCIM/100MEDIA/SJCM0004.jpg"}
    // <-{ "msg_id": 7, "type": "photo_taken" ,"param":"/tmp/fuse_d/DCIM/100MEDIA/SJCM0004.jpg"}
    public class UserTakePhotoMessage : UserMessage
    {
        public UserTakePhotoMessage()
        {
            token = 0;
            msg_id = 769;
        }

        public UserTakePhotoMessage(int token_value)
        {
            token = token_value;
            msg_id = 769;
        }
    }
}
