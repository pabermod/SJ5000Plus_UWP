using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using SJ5000Plus.ViewModels;
using Vocabulary.MessageCodecs;
using Vocabulary.Messages;

namespace SJ5000Plus
{
    public class Camera
    {
        private bool _connected;
        private SocketClient _CameraSocket;
        private int _token = 0;
        private string _permission = string.Empty;

        public int token
        {
            get { return _token; }
        }
        public Camera(IPAddress CameraIP, int Port)
        {
            _CameraSocket = new SocketClient(CameraIP.ToString(), Port);       
        }

        public async Task PopulateSettings(CamSettingsPageViewModel settings)
        {
            Vocabulary.Settings ReceivedSettings = await GetSettings();

            settings.video_resolution_list.Add(ReceivedSettings.video_resolution);
            settings.CurrentValues.video_resolution = ReceivedSettings.video_resolution;
            settings.photo_size_list.Add(ReceivedSettings.photo_size);
            settings.CurrentValues.photo_size = ReceivedSettings.photo_size;
        }

        public async Task PopulateValues(CamSettingsPageViewModel cameraSettings, string name)
        {
            CamGetParamValuesMessage Values = await GetParamValues(name);
            ObservableCollection<string> Source = (ObservableCollection<string>)cameraSettings.GetPropertyValue(name);

            foreach (var item in Values.options)
            {
                Source.Add(item);
            }

            // Remove the first one (so it isn't repeated)
            Source.RemoveAt(0);

            _permission = Values.permission;
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
        /// Request permission to set value of params. Returns true if permission granted
        /// </summary>
        private async Task<bool> RequestPermission()
        {
            // Create the message
            UserRequestEditParamMessage UserMsg = new UserRequestEditParamMessage(_token);

            // Get the codec
            UserRequestEditParamMessageCodec UserMsgCodec = new UserRequestEditParamMessageCodec();

            // Send the message
            if (await Send(await UserMsgCodec.Encode(UserMsg)))
            {
                // If sent get the response
                string MsgReceived = await _CameraSocket.Receive();

                // Get the codec
                CameraMessageCodec CamMsgCodec = new CameraMessageCodec();

                // Decode the string
                CameraMessage CamMsg = await CamMsgCodec.Decode(MsgReceived);

                if (CamMsg.rval != 0 || CamMsg.msg_id != UserMsg.msg_id)
                {
                    return false;
                }
                return true;
            }
            // Couldn't send
            return false;
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
        /// Get all the settings and their current value
        /// </summary>
        public async Task<Vocabulary.Settings> GetSettings()
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
        /// Get the current value of a parameter
        /// </summary>
        public async Task<string> GetParamValue(string param)
        {
            // Create the message
            UserGetParamValueMessage GetPar = new UserGetParamValueMessage(param, _token);

            // Get the codec
            UserGetParamValueMessageCodec UserGetParCodec = new UserGetParamValueMessageCodec();

            // Send the message
            if (await Send(await UserGetParCodec.Encode(GetPar)))
            {
                // If sent, get the response
                string MsgReceived = await _CameraSocket.Receive();

                // Get the codec
                CamGetParamValueMessageCodec CamGetParCodec = new CamGetParamValueMessageCodec();

                // Decode the string
                CamGetParamValueMessage CamGetPar = await CamGetParCodec.Decode(MsgReceived);

                if (CamGetPar.rval != 0 || CamGetPar.msg_id != GetPar.msg_id)
                {
                    return null;
                }
                return CamGetPar.param;
            }
            // If there is a problem
            return null;
        }

        /// <summary>
        /// Set the value of a parameter
        /// </summary>
        /// <param name="permission">Permission obtained from a CamGetParamValues object</param>
        public async Task<bool> SetParamValue(string param, string value)
        {
            // If not settable request permission
            if (!_permission.Equals("settable"))
            {
                // If permission not granted/error return null
                if (!await RequestPermission())
                {
                    return false;
                }
            }
            // If everything ok continue

            // Create the message
            UserSetParamMessage UserMsg = new UserSetParamMessage(_token, param, value);

            // Get the codec
            UserSetParamMessageCodec UsrMsgCodec = new UserSetParamMessageCodec();

            // Send the message
            if (await Send(await UsrMsgCodec.Encode(UserMsg)))
            {
                // If sent, get the response
                string MsgReceived = await _CameraSocket.Receive();

                // Get the codec
                CamParamMessageCodec CamMsgCodec = new CamParamMessageCodec();

                // Decode the string
                CamParamMessage CamMsg = await CamMsgCodec.Decode(MsgReceived);

                if (CamMsg.rval != 0 || CamMsg.msg_id != UserMsg.msg_id)
                {
                    return false;
                }
                return true;
            }
            // If there is a problem
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
                    return false;
                }
                else if (CamTokenMsg.msg_id != GetTokenMsg.msg_id)
                {
                    return false;
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
                return false;
            }
        }

        /// <summary>
        /// Disconnect the Socket. Will return false if succesful
        /// </summary>
        public async Task<bool> Disconnect()
        {
            _connected = !(await _CameraSocket.Disconnect());
            return _connected;
        }
    }
}
