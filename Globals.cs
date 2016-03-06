using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SJ5000Plus
{
    public static class Globals
    {
        // Parameterless constructor required for static class
        static Globals()
        {
            isConnected = false;
            CameraIP = IPAddress.Parse("192.168.42.1");
            CameraPort = 7878;
            LocalIP = null;
        } // default value

        // public get, and private set for strict access control
        public static bool isConnected { get; set; }

        public static IPAddress CameraIP { get; set; }

        public static IPAddress LocalIP { get; set; }

        public static int CameraPort { get; set; }
    }
}
