using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Data;

namespace DXBStudio
{
    public class Ternimal
    {
        public static List<Ternimal> lTernimals = new List<Ternimal>();
        private int _Id;
        public int Id
        {
            get { return _Id; }
        }
        private int _state;
        public int State
        {
            get { return _state; }
        }
        public DateTime LastRecv;
        public DateTime NowRecv;
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
        public void SetTernimal(TcpClient _tc)
        {
            
        }
        public Ternimal(byte[] bId,bool _carNetType,string _romVersion,string _Phone,int _gprsperiod,Int64 _maker,DateTime regTime)
        {
            _Id = System.BitConverter.ToInt32(bId, 0);
            _CarNetType = _carNetType;
            _romversion = _romVersion;
            _phone = _Phone;
            _gprsPeriod = _gprsperiod;
            _Maker = _maker;
            _RegTime = regTime; 
        }
        public static void InitData()
        {
            DataTable dt = DBHelp.GetTerminalsSetup();

        }

    }

}
