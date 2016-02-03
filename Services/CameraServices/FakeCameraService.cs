using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vocabulary;
using Vocabulary.Messages;

namespace SJ5000Plus.Services.CameraServices
{
    public class FakeCameraService : ICameraService
    {
        public async Task<Settings> GetCurrentValues()
        {
            Settings sett = new Settings();
            sett.video_resolution = "1080p";
            sett.photo_size = "16 MP";
            sett.video_standard = "NTSC";
            return sett;
        }

        public async Task<CamGetParamValuesMessage> GetParamValues(string param)
        {
            CamGetParamValuesMessage Param = new CamGetParamValuesMessage(0, 0, param, "settable", new List<string>());

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
            return Param;
        }
    }
}
