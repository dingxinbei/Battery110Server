using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;

namespace DXBStudio
{
    public class Packet
    {
        public const int minLen = 15;
        public const byte[] bBegin = {0x78,0x78 };
        // 《255
        private byte bNo = 1;
        //终端号，Server分配的
        private byte[] bTerminalNo = new byte[4];
        public UInt32 TerminalNo
        {
            get {
                return System.BitConverter.ToUInt32(bTerminalNo, 0);
            }
        }
        //协议号 2位
        private byte[] bCommand = new byte[2];
        public ushort Command
        {
            get { return System.BitConverter.ToUInt16(bCommand,0); }
        }
        //协议号，对应不同的处理。
        public enum Commands:ushort//2byte 位 枚举
        {
            Register = 1,
            Hand = 2,
            Random = 3,
            Key = 4,

            BatteryManageInfo = 0x10,
            ChangeInfo = 0x11,
            CarControlInfo = 0x12,
            MotorInfo = 0x13,

            GPSInfo = 0x1B,

            CanUpInfo = 0x20,
            CanDownInfo = 0x21,

            CmdUpModel = 0x60,
            CmdHand = 0x61,
            CmdUpData = 0x62,
            CmdReadData = 0x63,
            CmdFlashCrc = 0x64,

            SetDataTranslateType = 0x80,
            SetTcpIpAndPort = 0x82,
            SetTcpDomainAndPort = 0x84,
            SetTerminalMessageNo = 0x96,

            SetUpTypeAndPeriod = 0x90,
            SetTerminalCarInfoType = 0x94,
            SetTerminalCarInfoPeriod = 0x96,

            SetTerminalCanModel = 0xA0,
            SetTermalRecvCanId = 0xA2,

            QueryDataTransType = 0x81,
            QueryTcpIpAndPort = 0x83,
            QueryTcpDomainAndPort = 0x85,
            QueryTerminalMessageNo = 0x87,

            QueryCarInfoUpPeriod = 0x91,
            QueryCarInfoUpDataType = 0x93,
            QueryCarInfoDataType = 0x95,
            QueryCarInfoPeriod = 0x97,

            QureyTermalCanModel = 0xA1,
            QueryTermalCanId = 0xA3
            
        }
        //计算后填充 从 bNo开始，
        public byte[] bCrc = { 0x00, 0x00 };
        public const byte[] bEnd = { 0x0D, 0x0A };
        private Int64 LogId;
        public Packet(TcpClient tc,NetworkStream ns,Int64 _Logid)
        {   
            int Len ;
            //数据超长，不合理
            if(int.TryParse(ns.Length.ToString(),out Len))
                return;
            byte[] bb = new byte[Len];
            //读取到的数据不合理
            if (ns.Read(bb, 0, Len) <= minLen)
                return;
            LogId = _Logid;
            int i = 0;
            
            if (Len > minLen)
                if (bb[i++] == bBegin[0] && bb[i++] == bBegin[1] && bb[Len-2]==bEnd[0] && bb[Len-1]==bEnd[1])
                {
                    //凡是符合开头和结尾的数据包，crc 通过才入库                    
                    //crc 校验                                            // 结尾长度//crc长度///开头长度
                    ushort ucrc = CRC.CRC16.CRC16_ccitt(bb, 2, Len - bEnd.Length - 2 - bBegin.Length);
                    if (bb[Len - 3] == CRC.CRC16.Hi(ucrc) && bb[Len - 4] == CRC.CRC16.Lo(ucrc))
                    {    //录入数据库
                        DBHelp.LogPacket(bb, LogId);
                    }
                    else
                        return;
                    //包序号 位，不比较
                    i++;
                    /////////////////////
                    //终端号
                    bTerminalNo[0] = bb[i++];
                    bTerminalNo[1] = bb[i++];
                    bTerminalNo[2] = bb[i++];
                    bTerminalNo[3] = bb[i++];
                    /////////////////////////////
                    //协议号
                    bCommand[0] = bb[i++];
                    bCommand[1] = bb[i++];
                    /////////////////////////////
                    //根据不同的协议号，返回不同的处理
                    switch (Command)
                    {
                        case (int)Commands.Register: register(tc,ns); break;
                        case (int)Commands.Hand: hand(tc,ns); break;
                        //不合理的 不可能存在的协议号
                        default: return;
                    }

                }
                else
                    throw new Exception("Packet Begin or End is Wrong");
            else
                throw new Exception("Packet Len Less");

        }
        /// <summary>
        /// 回应握手协议
        /// </summary>
        /// <param name="ns"></param>
        private void hand(TcpClient tc,NetworkStream ns)
        {
            //throw new NotImplementedException();
            //比较是否有相同的 终端号，判断同终端号的状态
            foreach (Ternimal t in Ternimal.lTernimals)
            {
                if (TerminalNo == t.Id)
                {
                    t.SetRecvTime();
                    //发送 00 或 03 
                    byte[] bb = MakeHandPackage((byte)t.State);
                    ns.Write(bb, 0, bb.Length);
                    ns.Flush();
                    ns.Close();
                    if (t.State == Ternimal.ConnectState.Disconnect)
                    {
                        t.SetTcpClient(tc);
                    }
                    else
                        tc.Close();
                    return;
                }
            }
            //状态若为00，表示连接正常。若为02，则表示终端未注册。若为03，则表示连接已断开。
            
            //发送02
            byte[] bbb = MakeHandPackage(0x02);
            ns.Write(bbb,0,bbb.Length);
            ns.Flush();
            ns.Close();
            tc.Close();
        }

        private byte[] MakeHandPackage(byte p)
        {
            //throw new NotImplementedException();
            /////////////////////////////////序号长度////////////////////////////////信息长度//
            byte[] bb =new byte[minLen+1];
            /////////////////////
            int i = 0;
            bBegin.CopyTo(bb,0);
            i=i+bBegin.Length;
            //////////////////////
            if (bNo == 255)
                bNo = 1;
            bb[i++] = bNo++;
            ////////////////////
            bTerminalNo.CopyTo(bb,i);
            i=i+bTerminalNo.Length;
            /////////////////////////
            System.BitConverter.GetBytes(((ushort)Commands.Hand)).CopyTo(bb, i);
            i = i + sizeof(ushort);
            ///////////////////////////
            bb[i++] = 0x00; bb[i++] = 0x01;//信息长度为1
            ///////////////////////////状态 00 02 03
            bb[i++] = p;
            //协议体中从“信息序列号”到“信息内容”（包括“信息序列号”、“ 信息内容”）这部分数据的 CRC-ITU 值。
            ushort ucrc  = CRC.CRC16.CRC16_ccitt(bb, 2, minLen + 1 - bBegin.Length - bEnd.Length); 
            bb[i++] = CRC.CRC16.Lo(ucrc);
            bb[i++] = CRC.CRC16.Hi(ucrc);
            ////////////////////////////////////////////
            bb[i++] = bEnd[0];
            bb[i++] = bEnd[1];

            return bb;
        }
        /// <summary>
        /// 回应注册协议
        /// </summary>
        /// <param name="ns"></param>
        private void register(TcpClient tc, NetworkStream ns)
        {
            //throw new NotImplementedException();
        }
    }
}
