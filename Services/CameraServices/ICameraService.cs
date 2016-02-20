using SJ5000Plus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SJ5000Plus.Services.CameraServices
{
    public interface ICameraService
    {
        int token { get; set; }
        Task<Vocabulary.Settings> GetCurrentValues();
        Task<Vocabulary.Messages.CamGetParamValuesMessage> GetParamValues(string param);
        Task<bool> SetParamValue(string param, string value, string permission);
        Task<bool> Disconnect();
        Task<bool> Connect();
        Task<bool> GetToken();
        Task<bool> TakePhoto();
        Task<bool> StartVideo();
        Task<bool> StopVideo();
    }
}
