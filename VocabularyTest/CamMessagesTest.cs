using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using System.Threading.Tasks;
using Vocabulary.MessageCodecs;
using Vocabulary.Messages;

namespace VocabularyTest
{
    [TestClass]
    public class CamMessagesTest
    {
        [TestMethod]
        public void DeserializeSettingsMessage()
        {
            string MsgReceived = "{ \"rval\": 0, \"msg_id\": 3, \"param\": [ { \"camera_clock\": \"2015-09-03 06:37:43\" }, { \"video_standard\": \"NTSC\" }, { \"app_status\": \"idle\" }, { \"stream_out_type\": \"rtsp\" }, { \"save_low_resolution_clip\": \"off\" }, { \"video_resolution\": \"1920x1080 60P 16:9\" }, { \"video_stamp\": \"off\" }, { \"video_quality\": \"S.Fine\" }, { \"timelapse_video\": \"off\" }, { \"photo_size\": \"16M (4608x3456 4:3)\" }, { \"photo_stamp\": \"off\" }, { \"photo_quality\": \"S.Fine\" }, { \"timelapse_photo\": \"off\" }, { \"selfie_photo\": \"off\" }, { \"burst_photo\": \"off\" }, { \"autoshoot_photo\": \"off\" }, { \"loop_record\": \"off\" }, { \"motion_detec_video\": \"off\" }, { \"status_led_switch\": \"off\" }, { \"wifi_led_switch\": \"on\" }, { \"osd_switch\": \"on\" }, { \"cardvr_switch\": \"off\" }, { \"delay_pwroff\": \"off\" }, { \"rotate_image\": \"normal\" }, { \"mic_vol\": \"4\" }, { \"language\": \"en\" }, { \"date_disp_fmt\": \"ymd\" }, { \"auto_bkl_off\": \"60\" }, { \"auto_pwr_off\": \"180\" }, { \"light_freq\": \"60hz\" }, { \"meter_mode\": \"60hz\" }, { \"buzzer\": \"on\" } ] }";

            // Get the codec
            CamSettingsMessageCodec CamSettCodec = new CamSettingsMessageCodec();

            CamSettingsMessage CamSettMsg = null;

            // Decode the string
            Task.Run(async () =>
            {
                CamSettMsg = await CamSettCodec.Decode(MsgReceived);
            }).GetAwaiter().GetResult();

            Assert.AreEqual(3, CamSettMsg.msg_id); //Must be 3
            Assert.AreEqual(0, CamSettMsg.rval);
        }
    }
}
