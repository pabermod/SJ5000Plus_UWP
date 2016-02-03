using SJ5000Plus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vocabulary;
using Vocabulary.Messages;

namespace SJ5000Plus.Services.CameraServices
{
    public interface ICameraService
    {
        Task<Settings> GetCurrentValues();

        Task<CamGetParamValuesMessage> GetParamValues(string param);
    }
}
