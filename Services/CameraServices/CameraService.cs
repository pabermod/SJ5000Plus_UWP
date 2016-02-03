using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SJ5000Plus.Models;
using Vocabulary;
using Vocabulary.Messages;

namespace SJ5000Plus.Services.CameraServices
{
    public class CameraService : ICameraService
    {
        public Task<Settings> GetCurrentValues()
        {
            throw new NotImplementedException();
        }

        public Task<CamGetParamValuesMessage> GetParamValues(string param)
        {
            throw new NotImplementedException();
        }
    }
}
