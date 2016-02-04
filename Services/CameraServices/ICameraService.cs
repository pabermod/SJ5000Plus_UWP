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
        Task<Settings> GetCurrentValues();

        Task<Vocabulary.Messages.CamGetParamValuesMessage> GetParamValues(string param);
    }
}
