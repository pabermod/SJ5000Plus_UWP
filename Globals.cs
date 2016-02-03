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
        // parameterless constructor required for static class
        static Globals()
        {
            isConnected = false;
            CameraIP = IPAddress.Parse("192.168.42.1");
            CameraPort = 7878;
            LocalIP = null;
        } // default value

        // public get, and private set for strict access control
        public static bool isConnected { get; private set; }

        public static IPAddress CameraIP { get; private set; }

        public static IPAddress LocalIP { get; private set; }

        public static int CameraPort { get; private set; }


        // isConnected can be changed only via this method
        public static void SetIsConnected(bool newbool)
        {
            isConnected = newbool;
        }

        public static void SetCameraIP(IPAddress newIP)
        {
            CameraIP = newIP;
        }

        public static void SetCameraPort(int newint)
        {
            CameraPort = newint;
        }

        public static void SetLocalIP(IPAddress newIP)
        {
            LocalIP = newIP;
        }
    }
}
