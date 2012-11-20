using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;

namespace DXBStudio
{
    public class Client
    {
        private System.Net.Sockets.TcpClient tc;
        private long LogId;
        private NetworkStream ns;
        public Client(TcpClient tc, long LogId)
        {
            // TODO: Complete member initialization
            this.tc = tc;
            this.LogId = LogId;
            
        }
        public void AsynDo()
        {
           
            try
            {
                ns = tc.GetStream();
                while (tc.Connected)
                {
                    Packet p = new Packet(tc, ns, LogId);
                }
            }
            catch {//错误不一定需要关闭ns
                Close();
            }
                        
        }

        public void Close()
        {
            try
            {
                ns.Close();
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
