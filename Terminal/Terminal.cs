using System;
using System.Collections.Generic;
using System.Text;
using DXBStudio;

namespace Terminal
{
    public class Terminal
    {
        public delegate void _Message(string sMessage);
        public event _Message Message;
        public delegate void _ConnectState(State state);
        public event _ConnectState ConnectState;
        public bool bRandomTermanlNo = false;
        private uint _TerminalNo = 0xFFFFFFFF;
        private string _phone = "018907130240";
        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }
        private byte _NetType = 0x00;
        public byte NetType
        {
            get { return _NetType; }
            set { _NetType = value; }
        }
        private byte[] _Version = {0x01,0x02,0x03 };
        public string Version
        {
            get { return BitConverter.ToString(_Version).Replace('-', '.'); }
            set {
                string ss = value.Replace(".","");//.Split('.');
                Encoding.ASCII.GetBytes(ss).CopyTo(_Version,0);
            }
        }
        public uint TerminalNo
        {
            get { return _TerminalNo; }
        }
        private void SetTerminalNo(byte[] bb)
        {
            _TerminalNo = System.BitConverter.ToUInt16(bb,0);
        }
        private void ChangeState(State s)
        {
            if (_ConState != s)
            {
                _ConState = s;
                if (ConnectState != null)
                {
                    ConnectState(s);
                }
            }
        }
        private State _ConState = State.disConnect;
        public State ConState
        {
            get { return _ConState; }
        }
        public enum State
        {
            disConnect = 0,//未连接或断开连接
            Connect =1 ,//连接中
            Send = 2,//发送数据
            Recv =3//接受数据
        }
        private bool bRecv;
        private Server S;
        private System.Net.Sockets.TcpClient tc;
        private System.Net.Sockets.NetworkStream ns;
        public Terminal(Server s)
        {
            S = s;
            try
            {
                tc = new System.Net.Sockets.TcpClient(s.Address, s.Port);
                tc.Connect(s.Address, s.Port);
                ns = tc.GetStream();

                System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ThreadStart(Recv));

                ChangeState(State.Connect);
            }
            catch { ChangeState(State.disConnect); }
        }

        ~Terminal()
        {
            Close();
        }

        private void Recv()
        {
            
            byte[] bb = new byte[Packet.maxLen-1];
            while (bRecv)
            {
                int iRe = ns.Read(bb,0,Packet.maxLen-1);
                ChangeState(State.Recv);
                if (iRe > Packet.minLen && iRe < Packet.maxLen)//包大小符合要求
                {
                    if (bb[0] == Packet.bBegin[0] && bb[1] == Packet.bBegin[1] && bb[iRe - 2] == Packet.bEnd[0] && bb[iRe - 1] == Packet.bEnd[1]) //包初始结构符合要求
                    {
                        ushort ucrc = CRC.CRC16.CRC16_ccitt(bb, 2, iRe - 6 );
                        if (bb[iRe - 4] == CRC.CRC16.Lo(ucrc) && bb[iRe - 3] == CRC.CRC16.Hi(ucrc))//crc 校验 通过
                        {
                            int i = 2;
                            //包序号
                            byte bNo = bb[i++];
                            //TerminalNo
                            byte[] bTerminal = new byte[4];
                            bTerminal[0] = bb[i++];
                            bTerminal[1] = bb[i++];
                            bTerminal[2] = bb[i++];
                            bTerminal[3] = bb[i++];
                            //////////////////////////////////////
                            //比较终端号码
                            //注册，后比较，其实是写入。
                            //握手，需比较，但是没有注册 比较失败。

                            //其他，需比较，符合处理；不符合，不处理。
                            //////////////////////////////////////
                            //协议号
                            byte[] bCommand = new byte[2];
                            bCommand[0] = bb[i++];
                            bCommand[1] = bb[i++];
                            ushort Command = System.BitConverter.ToUInt16(bCommand, 0);
                            //////////////////////////////////////
                            //信息内容长度
                            int dataLen = System.BitConverter.ToInt16(bb, i);
                            i = i + 2;//2位
                            /////////////////////////////
                            //根据不同的协议号，返回不同的处理
                            switch (Command)
                            {
                                case (ushort)Packet.Commands.Register: ReponseRegister(bb,bTerminal,dataLen,i); break;
                                case (ushort)Packet.Commands.Hand: ResponseHand(bb,bTerminal,dataLen,i); break;
                                //不合理的 不可能存在的协议号
                                default: return;
                            }
                            //录入数据库

                            //接收处理完毕
                            if (tc.Connected)
                                ChangeState(State.Connect);
                            else
                                ChangeState(State.disConnect);
                        }
                    }
                }
            }
        }

        private void ResponseHand(byte[] bb,byte[] bTerminalNo,int infoLen, int i)
        {
            byte bState = bb[i++];
            string s = "";
            switch(bState){
                case (byte)Packet.HandState.DisConnect: s="断开"; break;
                case (byte)Packet.HandState.Normal: s="连接";break;
                case (byte)Packet.HandState.UnRegister: s = "未注册"; ; break;
                default:  break;
            }
            if (bRandomTermanlNo)
            {
                
                s = "随机终端号模式："+BitConverter.ToString(bTerminalNo)+"；收到握手包；状态 " + s;
            }
            else
            {
                if (System.BitConverter.ToUInt16(bTerminalNo, 0) == TerminalNo)
                {
                    s = "固定终端号模式:" + BitConverter.ToString(bTerminalNo) + "；收到握手包；状态 " + s;
                }
                else
                {
                    s = "固定终端号模式:" + BitConverter.ToString(bTerminalNo) + "；收到握手包，终端号 不匹配；状态 " + s;
                }
            }
            RecvMessage(s);
        }

        private void RecvMessage(string s)
        {
            if (Message != null)
                Message(s);
        }

        private void ReponseRegister(byte[] bb, byte[] bTerminalNo, int infoLen, int i)
        {
            byte bState = bb[i++];
            string s = "";
            switch (bState)
            {
                case 0x00: s = "注册成功"; break;
                case 0x01: s = "加密验证"; break;
                default: break;
            }

            if (bRandomTermanlNo)
            {
                s = "随机终端号模式:" + BitConverter.ToString(bTerminalNo) + "；收到注册包；状态 " + s;

            }
            else
            {
                if (System.BitConverter.ToUInt16(bTerminalNo, 0) == TerminalNo)
                {
                    s = "固定终端号模式:" + BitConverter.ToString(bTerminalNo) + "；收到注册包；第一次注册；状态 " + s;
                }
                else
                {
                    s = "固定终端号模式:" + BitConverter.ToString(bTerminalNo) + "；收到注册包；已注册过；状态 " + s;
                }
            }
            _TerminalNo = System.BitConverter.ToUInt16(bb, i);
            RecvMessage(s);
        }
        private Random r = new Random();
        /// <summary>
        /// 发送 握手包，
        /// </summary>
        /// <param name="b">随机 或 固定模式</param>
        public bool SendHand(bool b)
        {
            if (ConState != State.Connect)
            {
                return false;
            }
            bRandomTermanlNo = b;
            if (b)
            {
                _TerminalNo = (uint)r.Next(int.MinValue, int.MaxValue);
            }
            byte[] bb = Packet.MakeTerminalHandPacket(TerminalNo, 1);

            if (tc.Connected)
            {
                ChangeState(State.Send);
                ns.Write(bb, 0, bb.Length);
                ns.Flush();
                ChangeState(State.Connect);
                return true;
            }
            ChangeState(State.disConnect);
            return false;
        }
        /// <summary>
        /// 发送 注册包，
        /// </summary>
        /// <param name="b">随机 或 固定模式</param>
        public bool SendRegister(bool b)
        {
            if (ConState != State.Connect)
            {
                return false;
            }
            bRandomTermanlNo = b;
            if (b)
            {
                _phone = "01" + r.Next(10, 99).ToString() + r.Next(0,99999999).ToString().PadLeft(8,'0');
            }
            byte[] bb = Packet.MakeTerminalRegisterPacket(_phone, 1);

            if (tc.Connected)
            {
                ChangeState(State.Send);
                ns.Write(bb, 0, bb.Length);
                ns.Flush();
                ChangeState(State.Connect);
                return true;
            }
            ChangeState(State.disConnect);
            return false;
        }

        public void Close()
        {
            bRecv = false;
            try { ns.Close(); }
            catch { }
            try { tc.Close(); }
            catch { }
        }
    }
}
