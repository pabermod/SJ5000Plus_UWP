using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Sockets;
using Windows.Networking;
using Windows.Storage.Streams;
using Vocabulary;

namespace SJ5000Plus
{
    /// <summary>
    /// Socket Client Class
    /// </summary>
    public class SocketClient
    {
        // Timeout of reception
        const int RECV_TIMEOUT = 1000;
        const uint _OutboundBufferSize = 512;
        const int _InboundBufferSize = 4096;

        // Private variables 
        private string _server;             // Server IP address
        private int _port;                  // Server port                     // Buffer for received data
        private StreamSocket _Socket = null;
        private HostName _CameraHost = null;

        private DataReader _reader = null;
        private DataWriter _writer = null;

        /// <summary>
        /// Constructor with camera IP and port as params
        /// </summary>
        public SocketClient(string server, int port)
        {
            _server = server;
            _port = port;

            _CameraHost = new HostName(_server);       
        }

        /// <summary>
        /// Connect operation. Will return true if successful
        /// </summary>
        public async Task<bool> Connect()
        {
            try
            {
                _Socket = new StreamSocket();
                _Socket.Control.OutboundBufferSizeInBytes = _OutboundBufferSize;

                await _Socket.ConnectAsync(_CameraHost, _port.ToString());

                // add after calling ConnectAsync on the StreamSocket Object
                _reader = new DataReader(_Socket.InputStream);
                _writer = new DataWriter(_Socket.OutputStream);

                return true;
            }
            catch (Exception exception)
            {
                Windows.UI.Popups.MessageDialog md = null;

                switch (SocketError.GetStatus(exception.HResult))
                {
                    case SocketErrorStatus.Unknown:
                        md = new Windows.UI.Popups.MessageDialog("Unknown Error: " + exception.Message, "Error");
                        await md.ShowAsync();
                        break;
                    case SocketErrorStatus.ConnectionTimedOut:
                        md = new Windows.UI.Popups.MessageDialog("Connection Timed Out", "Error");
                        await md.ShowAsync();
                        break;
                    case SocketErrorStatus.HostNotFound:
                        md = new Windows.UI.Popups.MessageDialog("Camera Not Found", "Error");
                        await md.ShowAsync();
                        break;
                    default:
                        md = new Windows.UI.Popups.MessageDialog("Error in connect: " + exception.Message, "Error");
                        await md.ShowAsync();
                        break;
                }
                return false;
            }          
        }

        /// <summary>
        /// Send operation. Will send a String
        /// </summary>
        public async Task Send(string Message)
        {
            // write a string to the OutputStream
            _writer.WriteString(Message);

            // commit and send the data in the OutputStream
            await _writer.StoreAsync();
        }

        /// <summary>
        /// Receive operation. Will receive a String
        /// </summary>
        public async Task<String> Receive()
        {
            // Container for the received Data
            string receivedData = string.Empty;

            // set the DataReader to only wait for available data
            _reader.InputStreamOptions = InputStreamOptions.Partial;

            // wait for the available data up to 512 bytes
            // count is the number of actually received bytes
            var count = await _reader.LoadAsync(_InboundBufferSize);

            // read the data as a string and store it in our container
            if (count > 0)
            {
                receivedData = _reader.ReadString(count);
            }

            return receivedData;
        }

        public async Task<bool> Disconnect()
        {
            await _Socket.CancelIOAsync();
    
            _reader.Dispose();
            _reader = null;

            _writer.Dispose();
            _writer = null;

            _Socket.Dispose();
            _Socket = null;

            return true;
        }
    }
}
