﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;

namespace DXBStudio
{
    public class Client
    {
        private System.Net.Sockets.TcpClient tc;
        private long LogId;

        public Client(TcpClient tc, long LogId)
        {
            // TODO: Complete member initialization
            this.tc = tc;
            this.LogId = LogId;
            
        }
        public void AsynDo()
        {
            NetworkStream ns = tc.GetStream();
            try
            {
                Packet p = new Packet(tc,ns, LogId);
                ns.Close();
            }
            catch { ns.Close(); }
                        
        }

        public void Close()
        {
            try
            {
                tc.Close();
            }
            catch { }
        }
        ~Client()
        {
            Close();
        }
    }
}
