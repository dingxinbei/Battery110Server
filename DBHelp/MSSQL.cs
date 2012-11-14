using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace DXBStudio
{
    public class DBHelp
    {
        public static string sDbAddress = @"db.battery110.com";
        public static string sDbPort = "3200";
        public static string sDbUser = "sa";
        public static string sDbPass = "sasa";
        private static string sConnectDB = @"Data Source="+sDbAddress+","+sDbPort+";Initial Catalog=Battery110;User ID="+sDbUser+";Password="+sDbPass;
        private static string sRecvDataDb = @"Data Source=" + sDbAddress + "," + sDbPort + ";Initial Catalog=BatteryLog;User ID=" + sDbUser + ";Password=" + sDbPass;
        /// <summary>
        /// 创建连接
        /// </summary>
        /// <returns></returns>
        public static SqlConnection CreateSQLConnect()
        {
            string sc = sConnectDB;
            return new SqlConnection(sc);
        }
        public static SqlConnection CreateLogConnect()
        {
            string sc = sRecvDataDb;
            return new SqlConnection(sc);
        }
        /// <summary>
        /// 输入Mac IP Port
        /// </summary>
        /// <param name="Mac"></param>
        /// <param name="_ip"></param>
        /// <param name="_port"></param>
        /// <returns>一个Log ID，用于记录正常运行并退出</returns>
        public static long Login(string Mac, ref string _ip, ref int _port)
        {
            //throw new NotImplementedException();
            try
            {
                using (System.Data.SqlClient.SqlConnection sc = DBHelp.CreateSQLConnect())
                {
                    sc.Open();
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Login";
                    cmd.Connection = sc;
                    cmd.Parameters.Add("@Mac", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@LogId", System.Data.SqlDbType.BigInt);
                    cmd.Parameters.Add("@Ip", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@Port", System.Data.SqlDbType.Int);
                    cmd.Parameters["@Ip"].Direction = ParameterDirection.Output;
                    cmd.Parameters["@Port"].Direction = ParameterDirection.Output;
                    cmd.Parameters["@LogId"].Direction = ParameterDirection.Output;
                    cmd.Parameters["@Mac"].Value = Mac;
                    cmd.ExecuteNonQuery();

                    sc.Close();
                    if (cmd.Parameters["@Ip"].Value != null)
                    {
                        _ip = cmd.Parameters["@Ip"].Value.ToString();
                    }
                    if (cmd.Parameters["@Port"].Value != null)
                    {
                        _port = (int)cmd.Parameters["@Ip"].Value;
                    }
                    //tsbStatus.Text = "数据库连接成功！";
                    return (long)(cmd.Parameters["@LogId"].Value);
                }
            }
            catch { }//tsbStatus.Text = "数据库连接失败！"; }
            return 0;
        }

        

        public static DataTable GetTerminalsSetup(string sMac)
        {
            //throw new NotImplementedException();
            DataTable dt = new DataTable();
            try
            {
                using (System.Data.SqlClient.SqlConnection sc = DBHelp.CreateSQLConnect())
                {
                    sc.Open();
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                    cmd.Connection = sc;
                    cmd.CommandText = "select * from dbo.TerminalSetupInfo where [state]>=0 and inserver = '"+sMac+"'";
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);
                    sc.Close();
                }
            }
            catch { }//tsbStatus.Text = "数据库连接失败！"; }
            return dt;
        }

        public static void CloseServer(long LogId)
        {
            //throw new NotImplementedException();
            try
            {
                using (System.Data.SqlClient.SqlConnection sc = DBHelp.CreateSQLConnect())
                {
                    sc.Open();
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                    cmd.Connection = sc;
                    cmd.CommandText = "update dbo.ServerLog set LeaveTime = GETDATE() where bID="+LogId.ToString();
                    cmd.ExecuteNonQuery();
                    sc.Close();
                }
            }
            catch { }//tsbStatus.Text = "数据库连接失败！"; }
        }

        public static void SaveServerConfing(string smac,string _sIp, int _port)
        {
            //throw new NotImplementedException();
            try
            {
                using (System.Data.SqlClient.SqlConnection sc = DBHelp.CreateSQLConnect())
                {
                    sc.Open();
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = sc;
                    cmd.CommandText = "SaveServerConfig";
                    cmd.Parameters.Add("@Mac", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@Ip", System.Data.SqlDbType.VarChar);
                    cmd.Parameters.Add("@Port", System.Data.SqlDbType.Int);
                    cmd.Parameters["@Mac"].Value = smac;
                    cmd.Parameters["@Ip"].Value = _sIp;
                    cmd.Parameters["@Port"].Value = _port;
                    cmd.ExecuteNonQuery();
                    sc.Close();
                }
            }
            catch { }//tsbStatus.Text = "数据库连接失败！"; }
        }

        public static void LogPacket(byte[] bb, int iNo, byte[] bTerminalNo, byte[] bCommand, int dataLen, int i, long LogId)
        {
            //throw new NotImplementedException();
            try
            {
                using (System.Data.SqlClient.SqlConnection sc = DBHelp.CreateLogConnect())
                {
                    sc.Open();
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                    cmd.Connection = sc;
                    cmd.CommandText = "LoginData";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@data", SqlDbType.VarBinary);
                    cmd.Parameters.Add("@No", SqlDbType.Int);
                    cmd.Parameters.Add("@Terminal", SqlDbType.Binary);
                    cmd.Parameters.Add("@Command", SqlDbType.Binary);
                    cmd.Parameters.Add("@infoLen", SqlDbType.Int);
                    cmd.Parameters.Add("@infodata", SqlDbType.Binary);
                    cmd.Parameters.Add("@LogId", SqlDbType.BigInt);
                    cmd.Parameters["@data"].Value = bb;
                    cmd.Parameters["@No"].Value = iNo;
                    cmd.Parameters["@TerminalNo"].Value = bTerminalNo;
                    cmd.Parameters["@Command"].Value = bCommand;
                    cmd.Parameters["@infoLen"].Value = dataLen;
                    byte[] bbb = new byte[dataLen];
                    bb.CopyTo(bbb,i);
                    cmd.Parameters["@infodata"].Value = bbb;
                    cmd.Parameters["@LogId"].Value = LogId;
                    cmd.ExecuteNonQuery();
                    sc.Close();
                }
            }
            catch { }
        }
        /// <summary>
        /// 终端注册
        /// </summary>
        /// <param name="bTerminalNo"></param>
        /// <param name="sphone"></param>
        /// <param name="bType"></param>
        /// <param name="sversion"></param>
        /// <param name="bReRegister"></param>
        /// <returns></returns>
        public static bool Register(string mac,out byte[] bTerminalNo, string sphone, bool bType, string sversion,out bool bReRegister)
        {
            bTerminalNo = new byte[4] ;bReRegister = false;
            try
            {
                using (System.Data.SqlClient.SqlConnection sc = DBHelp.CreateSQLConnect())
                {
                    sc.Open();
                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
                    cmd.Connection = sc;
                    cmd.CommandText = "Register";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Phone", SqlDbType.VarChar);
                    cmd.Parameters.Add("@Type", SqlDbType.Bit);
                    cmd.Parameters.Add("@Version", SqlDbType.VarChar);
                    cmd.Parameters["@Phone"].Value = sphone;
                    cmd.Parameters["@Type"].Value = bType;
                    cmd.Parameters["@Version"].Value = sversion;
                    cmd.Parameters.Add("@mac", SqlDbType.VarChar);
                    cmd.Parameters["@mac"].Value = mac;

                    cmd.Parameters.Add("@TerminalNo", SqlDbType.Binary);
                    cmd.Parameters.Add("@ReReg", SqlDbType.Bit);
                    cmd.Parameters["@TerminalNo"].Direction = ParameterDirection.Output;
                    cmd.Parameters["@ReReg"].Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();
                    sc.Close();

                    bTerminalNo = (byte[])cmd.Parameters["@TerminalNo"].Value;
                    bReRegister = (bool)cmd.Parameters["@ReReg"].Value;

                    return true;
                }
            }
            catch { }

            return false;
        }
    }
}
