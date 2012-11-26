using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Data;
using System.ComponentModel;

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
        private UInt32 _Id;
        public  int RowIndex = -1;
        [Browsable(true)]
        [Description("连接终端的ID"),DisplayName("终端ID")]
        public UInt32 Id
        {
            get { return _Id; }
        }
        private System.Threading.Thread th;
        //状态若为00，表示连接正常。若为02，则表示终端未注册。若为03，则表示连接已断开。
        private ConnectState _state;
        [Browsable(true)]
        [Description("连接状态"),DisplayName("连接状态")]
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
        [Browsable(false)]
        [Description("上次接收到数据的时间"), DisplayName("接收时间")]
        public DateTime LastRecv
        {
            get { return _LastRecv; }
        }
        private DateTime _NowRecv;
        [Browsable(true)]
        [Description("当前接收到数据的时间"), DisplayName("接收时间")]
        public DateTime NowRecv {
            get { return _NowRecv; }
        }
        private TcpClient tc;
        private string _phone;
        [Browsable(true)]
        [Description("终端唯一ID，为一手机号码"), DisplayName("终端号")]
        public string Phone
        {
            get { return _phone; }
        }
        private bool _CarNetType;
        [Browsable(true)]
        [Description("网络类型，true：RS485，false：CAN网络"), DisplayName("网络类型")]
        public bool CarNetType
        {
            get { return _CarNetType; }
        }
        private string _romversion;
        [Browsable(true)]
        [Description("终端固件版本号"), DisplayName("固件版本")]
        public string RomVersion
        {
            get { return _romversion; }
        }
        private int _gprsPeriod;
        [Browsable(true)]
        [Description("每次刷新的时间s"), DisplayName("跟新周期")]
        public int GPRSPeriod
        {
            get { return _gprsPeriod; }
            set { _gprsPeriod = value; }
        }
        private Int64 _Maker;
        [Browsable(true)]
        [Description("输入者ID"), DisplayName("用户ID")]
        public Int64 MakerId
        {
            get { return _Maker; }
        }
        private DateTime _RegTime;
        [Browsable(true)]
        [Description("注册时间"), DisplayName("注册时间")]
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
                if (dr[5] == DBNull.Value)
                    dr[5] = 0;
                AppTerminal(new Terminal((byte[])dr[1], (bool)dr[2], (string)dr[3], (string)dr[4], (int)dr[5], (Int64)dr[6], (DateTime)dr[7]));
            }
        }
        public static void AppTerminal(Terminal tt)
        {
            foreach (Terminal t in lTerminals)
            {
                if (t.Id == tt.Id)
                {
                    tt.Close();
                    return;
                }
            }
            lTerminals.Add(tt);
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
