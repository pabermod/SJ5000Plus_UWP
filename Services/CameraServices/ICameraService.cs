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
        bool isConnected { get; set; }
        bool isRecording { get; set; }
        int token { get; set; }
        Task<Vocabulary.Settings> GetCurrentValues();
        Task<Vocabulary.Messages.CamGetParamValuesMessage> GetParamValues(string param);
        Task<bool> SetParamValue(string param, string value, string permission);
        Task<bool> Disconnect();
        Task<bool> Connect();
        Task<bool> GetToken();
        Task<string> TakePhoto();
        Task<bool> StartVideo();
        Task<string> StopVideo();
    }
}
