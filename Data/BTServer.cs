using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using Data;

namespace DXBStudio
{
    public class BTServer
    {
        #region Data Member Define
        private Int64 _logID = 0;
        public Int64 LogId
        {
            get { return _logID; }
        }
        private string _mac;
        public string Mac
        {
            get
            {
                return _mac;
            }
        }
        private int _port = 8888;
        public int Port {
            get { return _port; }
        }
        private string _ip = "0.0.0.0";
        public string Ip {
            get { return _ip; }
        }

        #endregion
        public BTServer()
        {
            string[] ss = DXBStudio.NetWork.GetLocalMacs();
            if (ss.Length <= 0)
                throw new Exception("local mac address is not finding!");
            else
                _mac = ss[0];
            _logID = DBHelp.Login(Mac,ref _ip,ref _port);

            if (LogId <= 0)
            {
                throw new Exception("It is Failed that server Logined in Database !");
            }
        }
        //0,初始，1，监听，……
        private int _state = 0;
        public int State { get { return _state; } }
        //打开倾听端口，异步
        
        public void Open()
        {
            //TcpClient tc = new TcpClient(Ip,Port);
            IPAddress ipAd ;
            if (IPAddress.TryParse(Ip, out ipAd))
            {
                TcpListener tl = new TcpListener(ipAd, Port);
                tl.Start();
                while (true)
                {
                    TcpClient tc = tl.AcceptTcpClient();
                    Client c = new Client(tc,LogId);
                    System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ThreadStart(c.AsynDo));
                    th.Start();
                }
            }
            else
                throw new Exception("String isnot IpAddress ");
        }
    }
}
