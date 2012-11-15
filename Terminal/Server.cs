using System;
using System.Collections.Generic;
using System.Text;

namespace Terminal
{
    public class Server
    {
        private string _Address;
        public string Address
        {
            get { return _Address; }
        }
        private int _Port;
        public int Port
        {
            get { return _Port; }
        }
        public Server(string sIporDomain,int port)
        {
            _Port = port;
            _Address = sIporDomain;
        }
    }
}
