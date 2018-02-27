using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Management;
using System.Net.NetworkInformation;

namespace Networking.Core
{
    public static class NetTools
    {
        public static IPAddress GetLocalIP(AddressFamily addrFamily = AddressFamily.InterNetwork)
        {
            List<string> listIP = new List<string>();

            ManagementClass mcNetworkAdapterConfig = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc_NetworkAdapterConfig = mcNetworkAdapterConfig.GetInstances();

            foreach (ManagementObject mo in moc_NetworkAdapterConfig)
            {
                string mServiceName = mo["ServiceName"] as string;

                //过滤非真实的网卡  
                if (!(bool)mo["IPEnabled"]) continue;

                if (mServiceName.ToLower().Contains("vmnetadapter")
                 || mServiceName.ToLower().Contains("ppoe")
                 || mServiceName.ToLower().Contains("bthpan")
                 || mServiceName.ToLower().Contains("tapvpn")
                 || mServiceName.ToLower().Contains("ndisip")
                 || mServiceName.ToLower().Contains("sinforvnic")) continue;


                if (mo["IPAddress"] is string[] mIPAddress)
                {
                    foreach (string ip in mIPAddress)
                    {
                        if (ip != "0.0.0.0")
                        {
                            listIP.Add(ip);
                        }
                    }
                }

                mo.Dispose();
            }

            foreach (string strIP in listIP)
            {
                IPAddress tempIP = IPAddress.Parse(strIP);
                if (tempIP.AddressFamily == addrFamily)
                {
                    return tempIP;
                }
            }

            return null;
        }

        public static bool PingIP(string ip)
        {
            Ping ping = new Ping();
            PingReply pingReply = ping.Send(ip);

            if (pingReply.Status == IPStatus.Success)
            {
                return true;
            }
            else
            {
                return false;
            } 
        }

        public static bool PingIP(IPAddress ip)
        {
            return PingIP(ip.ToString());
        }

        public static string ToIP(this EndPoint pt)
        {
            return pt.ToString().Split(':')[0];
        }

        public static int ToPort(this EndPoint pt)
        {
            return Convert.ToInt32(pt.ToString().Split(':')[1]);
        }
    }
}