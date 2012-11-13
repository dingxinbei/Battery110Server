using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.NetworkInformation;

namespace DXBStudio
{
    public class NetWork
    {
        public static string[] GetLocalMacs()
        {
            List<string> ss = new List<string>();
            NetworkInterface[] nii = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface ni in nii)
            {
                ss.Add(ni.GetPhysicalAddress().ToString());
            }
            return ss.ToArray();
        }

        public static string[] GetLocalIps()
        {
            List<string> ss = new List<string>();
            IPAddress[] ipads = System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName());
            foreach ( IPAddress ip in ipads )
            {
                ss.Add(ip.ToString());
            }
            return ss.ToArray();
        }
    }
}
