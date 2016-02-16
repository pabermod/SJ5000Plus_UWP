using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SJ5000Plus.Models;
using Vocabulary.Messages;
using Vocabulary.MessageCodecs;
using System.Net;

namespace SJ5000Plus.Services.CameraServices
{
    public class CameraService : ICameraService
    {
        private bool _connected;
        private SocketService _CameraSocket;
        private int _token = 0;

        public int token
        {
            get { return _token; }
        }

        public CameraService(IPAddress CameraIP, int Port)
        {
            _CameraSocket = new SocketService(CameraIP.ToString(), Port);
        }

        /// <summary>
        /// Send a string and returns true if sent
        /// </summary>
        private async Task<bool> Send(string Msg)
        {
            // Send the Message
            await _CameraSocket.Send(Msg);
            return true;
        }

        /// <summary>
        /// Receive a string
        /// </summary>
        private async Task<string> Receive()
        {
            string MsgReceived = await _CameraSocket.Receive();
            return MsgReceived;
        }

        /// <summary>
        /// Connect to the Camera and get Token
        /// </summary>
        public async Task<bool> Connect()
        {
            _connected = await _CameraSocket.Connect();
            if (_connected)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Get a new Token and if successfull assigns it to the Token property
        /// </summary>
        public async Task<bool> GetToken()
        {
            // Create Token messages
            UserTokenMessage GetTokenMsg = new UserTokenMessage();
            CamTokenMessage CamTokenMsg = new CamTokenMessage();

            UserTokenMessageCodec UserTokenCodec = new UserTokenMessageCodec();

            // Send the Msg
            if (await Send(await UserTokenCodec.Encode(GetTokenMsg)))
            {
                // If sent, get the response
                string MsgReceived = await _CameraSocket.Receive();

                CamTokenMessageCodec CamTokenCodec = new CamTokenMessageCodec();
                CamTokenMsg = await CamTokenCodec.Decode(MsgReceived);

                if (CamTokenMsg.rval != 0)
                {
                    return false; //Should throw exception
                }
                else if (CamTokenMsg.msg_id != GetTokenMsg.msg_id)
                {
                    return false; //Should throw exception
                }
                // Everything correct, return the token
                else
                {
                    _token = CamTokenMsg.param;
                    return true;
                }
            }
            else
            {
                // If we don't have a response
                return false; //Should throw exception
            }
        }

        /// <summary>
        /// Get all the settings and their current value
        /// </summary>
        public async Task<Vocabulary.Settings> GetCurrentValues()
        {
            // Create the message
            UserSettingsMessage SettMsg = new UserSettingsMessage(_token);

            // Get the codec
            UserSettingsMessageCodec UserSettCodec = new UserSettingsMessageCodec();

            // Send the message
            if (await Send(await UserSettCodec.Encode(SettMsg)))
            {
                // If sent, get the response
                string MsgReceived = await _CameraSocket.Receive();

                // Get the codec
                CamSettingsMessageCodec CamSettCodec = new CamSettingsMessageCodec();

                // Decode the string
                CamSettingsMessage CamSettMsg = await CamSettCodec.Decode(MsgReceived);

                if (CamSettMsg.rval != 0 || CamSettMsg.msg_id != SettMsg.msg_id)
                {
                    return null;
                }
                return CamSettMsg.param;
            }
            // If there is a problem
            return null;
        }

        /// <summary>
        /// Get all the possible values for a specific param
        /// </summary>
        public async Task<CamGetParamValuesMessage> GetParamValues(string param)
        {
            // Create the message
            UserGetParamValuesMessage GetValuesMsg = new UserGetParamValuesMessage(_token, param);

            // Get the codec
            UserGetParamValuesMessageCodec UserValuesCodec = new UserGetParamValuesMessageCodec();

            // Send the message
            if (await Send(await UserValuesCodec.Encode(GetValuesMsg)))
            {
                // If sent, get the response
                string MsgReceived = await _CameraSocket.Receive();

                // Get the codec
                CamGetParamValuesMessageCodec CamValuesCodec = new CamGetParamValuesMessageCodec();

                // Decode the string
                CamGetParamValuesMessage CamValuesMsg = await CamValuesCodec.Decode(MsgReceived);

                if (CamValuesMsg.rval != 0 || CamValuesMsg.msg_id != GetValuesMsg.msg_id)
                {
                    return null;
                }
                return CamValuesMsg;
            }
            // If there is a problem
            return null;
        }

        /// <summary>
        /// Set the value of a parameter
        /// </summary>
        public Task SetParamValue(string name, string selectedItem)
        {
            return Task.CompletedTask;
        }
    }
}
