using SJ5000Plus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SJ5000Plus.Services.CameraServices
{
    public class FakeCameraService : ICameraService
    {
        public async Task<Settings> GetCurrentValues()
        {
            Settings sett = new Settings();
            sett.video_resolution = "1080p";
            sett.video_standard = "NTSC";
            sett.video_quality = "Super Fine";
            sett.video_stamp = "Date/Time";
            sett.timelapse_video = "10";
            sett.loop_record = "5";
            sett.motion_detec_video = "High";

            sett.photo_size = "16 MP";
            sett.photo_quality = "Fine";
            sett.photo_stamp = "Date";
            sett.selfie_photo = "5";
            sett.burst_photo = "10";
            sett.autoshoot_photo = "3";
            return sett;
        }

        public async Task<Vocabulary.Messages.CamGetParamValuesMessage> GetParamValues(string param)
        {
            Vocabulary.Messages.CamGetParamValuesMessage Param = new Vocabulary.Messages.CamGetParamValuesMessage(0, 0, param, "settable", new List<string>());

            if (param.Equals("video_resolution_list"))
            {
                Param.options.Add("1080p");
                Param.options.Add("720p");
                Param.options.Add("480p");
            }
            else if (param.Equals("video_standard_list"))
            {
                Param.options.Add("NTSC");
                Param.options.Add("PAL");
            }
            else if (param.Equals("photo_size_list"))
            {
                Param.options.Add("20 MP");
                Param.options.Add("16 MP");
                Param.options.Add("12 MP");
            }
            else if (param.Equals("video_quality_list"))
            {
                Param.options.Add("Super Fine");
                Param.options.Add("Fine");
                Param.options.Add("Normal");
            }
            return Param;
        }
    }
}
