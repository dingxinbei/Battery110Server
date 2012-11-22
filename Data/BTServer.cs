using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;

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
        private static string _mac;
        public static string Mac
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
            Init();
        }
        static BTServer()
        {
            //throw new NotImplementedException();
            string[] ss = DXBStudio.NetWork.GetLocalMacs();
            if (ss.Length <= 0)
                throw new Exception("local mac address is not finding!");
            else
                _mac = ss[0].Trim();
        }
        private void Init()
        {
            
            _logID = DBHelp.Login(Mac, ref _ip, ref _port);

            if (LogId <= 0)
            {
                throw new Exception("It is Failed that server Logined in Database !");
            }
            _state = 0;
        }

        public BTServer(string _sIp, int _port)
        {
            // TODO: Complete member initialization
            DBHelp.SaveServerConfing(Mac,_sIp, _port);
            Init();
        }
        //-1,初始化失败,0,初始，1，监听，……
        private int _state = -1;
        public int State { get { return _state; } }
        //打开倾听端口，异步
        private bool isclose = false;
        private TcpListener tl;
        public void Open()
        {
            //TcpClient tc = new TcpClient(Ip,Port);
            IPAddress ipAd ;
            if (IPAddress.TryParse(Ip, out ipAd))
            {
                tl = new TcpListener(ipAd, Port);
                tl.Start();
                _state = 1;
                while (!isclose)
                {
                    try
                    {
                        TcpClient tc = tl.AcceptTcpClient();
                        Client c = new Client(tc, LogId);
                        System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ThreadStart(c.AsynDo));
                        th.Start();
                    }
                    catch { }
                }

                tl.Stop();
                _state = 0;
            }
            else
                throw new Exception("String isnot IpAddress ");

        }
        public void Close()
        {
            isclose = true;
            DBHelp.CloseServer(LogId);
            try
            {
                tl.Stop();
            }
            catch { }
            _state = 0;
        }
    }
}
