using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Radios;
using Windows.Devices.WiFi;
using Windows.Networking.Connectivity;
using Windows.System;
using Windows.UI.Popups;

namespace SJ5000Plus
{
    class Helpers
    {
        /// <summary>
        /// If WiFi is off, will try to turn it on. Then it will return true if the WiFi is on.
        /// </summary>
        public async Task<bool> CheckRadio()
        {
            IReadOnlyList<Radio> MyRadios = await Radio.GetRadiosAsync();
            foreach (var item in MyRadios)
            {
                if (item.Kind == RadioKind.WiFi)
                {
                    RadioAccessStatus AccessStatus = await item.SetStateAsync(RadioState.On);
                    if (AccessStatus != RadioAccessStatus.Allowed)
                    {
                        bool result = await Launcher.LaunchUriAsync(new Uri("ms-settings:privacy-radios"));
                    }
                }
                if (item.State == RadioState.On)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Returns true if the current connection is wifi
        /// </summary>
        public async Task<bool> CheckWiFi()
        {
            // Get the WiFi Adapter
            WiFiAdapter Adapter = await GetWiFiAdapter();

            if (Adapter != null)
            {
                // Check if WiFi is on
                ConnectionProfile Network = await Adapter.NetworkAdapter.GetConnectedProfileAsync();

                if (Network != null)
                {
                    //&& Network.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.LocalAccess
                    string ip = string.Empty;

                    // Get the Ip
                    ip = (from hn in NetworkInformation.GetHostNames()
                          where hn.IPInformation?.NetworkAdapter != null
                             && hn.IPInformation.NetworkAdapter.NetworkAdapterId
                             == Network.NetworkAdapter.NetworkAdapterId
                          select hn).First().CanonicalName;

                    if (ip != string.Empty)
                    {
                        IPAddress TheIP = IPAddress.Parse(ip);
                        Globals.SetLocalIP(TheIP);
                        string[] nums = ip.Split('.');
                        string CamIP = nums[0] + "." + nums[1] + "." + nums[2] + ".1";
                        Globals.SetCameraIP(IPAddress.Parse(CamIP));
                        return true;
                    }
                    return false;
                }
                return false;
            }
            return false;
        }

        private async Task<WiFiAdapter> GetWiFiAdapter()
        {
            IReadOnlyList<WiFiAdapter> WifiAdapterList = await WiFiAdapter.FindAllAdaptersAsync();
            // Get the first adapter (or null if there isn't any)
            return WifiAdapterList.FirstOrDefault();
        }

        public async void ShowDialog(string message)
        {
            var messageDialog = new MessageDialog(message);
            messageDialog.Commands.Add(new UICommand("Close", null, 1));
            messageDialog.DefaultCommandIndex = 1;
            var commandChosen = await messageDialog.ShowAsync();
        }

        public async void WifiSettingsDialog(string message)
        {
            var messageDialog = new MessageDialog(message);
            messageDialog.Commands.Add(new UICommand("Exit", Exit, 0));
            messageDialog.Commands.Add(new UICommand("WiFi Settings", WiFiSettings, 1));
            messageDialog.DefaultCommandIndex = 1;
            var commandChosen = await messageDialog.ShowAsync();
        }

        private void Exit(IUICommand command)
        {
            App.Current.Exit();
        }

        private async void WiFiSettings(IUICommand command)
        {
            bool result = await Launcher.LaunchUriAsync(new Uri("ms-settings-wifi:"));
        }
    }
}
