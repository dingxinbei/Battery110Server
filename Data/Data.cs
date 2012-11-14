using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Data;

namespace DXBStudio
{
    public class Terminal
    {
        /// <summary>
        /// ////////////////////////////////////////////
        /// </summary>
        /// <param name="iState"></param>
        public delegate void _StateChange(Terminal sender,ConnectState cs);
        public event _StateChange StateChange;

        public delegate void _RecvData(Terminal sender);
        public event _RecvData RecvData;
        /// <summary>
        /// /////////////////////////////////////////////////
        /// </summary>
        public static List<Terminal> lTerminals = new List<Terminal>();
        private uint _Id;
        public  int RowIndex = -1;
        
        public uint Id
        {
            get { return _Id; }
        }
        private System.Threading.Thread th;
        //状态若为00，表示连接正常。若为02，则表示终端未注册。若为03，则表示连接已断开。
        private ConnectState _state;
        public ConnectState State
        {
            get { return _state; }
        }
        public enum ConnectState : byte
        {
            Normal = 0x00,
            UnRegister = 0x01,
            Disconnect = 0x03
        }
        private DateTime _LastRecv;
        public DateTime LastRecv
        {
            get { return _LastRecv; }
        }
        private DateTime _NowRecv;
        public DateTime NowRecv {
            get { return _NowRecv; }
        }
        private TcpClient tc;
        private string _phone;
        public string Phone
        {
            get { return _phone; }
        }
        private bool _CarNetType;
        public bool CarNetType
        {
            get { return _CarNetType; }
        }
        private string _romversion;
        public string RomVersion
        {
            get { return _romversion; }
        }
        private int _gprsPeriod;
        public int GPRSPeriod
        {
            get { return _gprsPeriod; }
        }
        private Int64 _Maker;
        public Int64 MakerId
        {
            get { return _Maker; }
        }
        private DateTime _RegTime;
        public DateTime RegisterTime
        {
            get { return _RegTime; }
        }

        public void SetTcpClient(TcpClient _tc)
        {
            tc = _tc;
            _state = ConnectState.Normal;
        }
        //public Terminal( bool _carNetType, string _romVersion, string _Phone)
        //{
        //    //_Id = System.BitConverter.ToUInt32(bId, 0);
        //    _CarNetType = _carNetType;
        //    _romversion = _romVersion;
        //    _phone = _Phone;
        //    //获取分配的ID，或者已存在的ID



        //    //_gprsPeriod = _gprsperiod;
        //    //_Maker = _maker;
        //    //_RegTime = regTime;
        //    _state = ConnectState.Disconnect;//数据库获取,未链接的
        //    th = new System.Threading.Thread(new System.Threading.ThreadStart(CheckConnect));
        //    th.Start();
        //}
        /// <summary>
        /// 从数据库获取出来的
        /// </summary>
        /// <param name="bId"></param>
        /// <param name="_carNetType"></param>
        /// <param name="_romVersion"></param>
        /// <param name="_Phone"></param>
        /// <param name="_gprsperiod"></param>
        /// <param name="_maker"></param>
        /// <param name="regTime"></param>
        public Terminal(byte[] bId, bool _carNetType, string _romVersion, string _Phone, int _gprsperiod, Int64 _maker, DateTime regTime)
        {
            _Id = System.BitConverter.ToUInt32(bId, 0);
            _CarNetType = _carNetType;
            _romversion = _romVersion;
            _phone = _Phone;
            _gprsPeriod = _gprsperiod;
            _Maker = _maker;
            _RegTime = regTime;
            _state = ConnectState.Disconnect;//数据库获取,未链接的
            th = new System.Threading.Thread(new System.Threading.ThreadStart(CheckConnect));
            th.Start();
        }
        public static void InitData(string sMac)
        {
            lTerminals.Clear();
            DataTable dt = DBHelp.GetTerminalsSetup(sMac);
            foreach (DataRow dr in dt.Rows)
            {
                lTerminals.Add(new Terminal((byte[])dr[1], (bool)dr[2], (string)dr[3], (string)dr[4], (int)dr[5], (Int64)dr[6], (DateTime)dr[7]));
            }
        }
        public static void ReleaseALL()
        {
            foreach (Terminal t in lTerminals)
            {
                t.Close();
            }
            lTerminals.Clear();
        }
        private bool bCheckConnect = true;
        /// <summary>
        /// 检测数组的数量和是否关联，网络是否断开
        /// </summary>
        private void CheckConnect()
        {
            while (bCheckConnect)
            {
                if (tc != null)
                {
                    if (tc.Connected)
                    {
                        if (_state == ConnectState.Disconnect)
                        {
                            _state = ConnectState.Normal;
                            if (StateChange != null)
                                StateChange(this,State);
                        }
                    }
                    else
                    {
                        if (_state == ConnectState.Normal)
                        {
                            _state = ConnectState.Disconnect;
                            if (StateChange != null)
                                StateChange(this,State);
                        }
                    }
                }
                System.Threading.Thread.Sleep(1000);
            }
        }
        public void Close()
        {
            bCheckConnect = false;
           
            try { tc.Close(); }
            catch { }
            try
            {
                th.Abort();
            }
            catch { }
        }

        public void SetRecvTime()
        {
            //throw new NotImplementedException();
            _LastRecv = _NowRecv;
            _NowRecv = DateTime.Now;

            if (RecvData != null)
                RecvData(this);
        }
    }
}
