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
        private const string sConnectDB = @"Data Source="+sDbAddress+","+sDbPort+";Initial Catalog=Battery110;User ID="+sDbUser+";Password="+sDbPass;
        private const string sRecvDataDb = @"Data Source=" + sDbAddress + "," + sDbPort + ";Initial Catalog=BatteryLog;User ID=" + sDbUser + ";Password=" + sDbPass;
        /// <summary>
        /// 创建连接
        /// </summary>
        /// <returns></returns>
        public static SqlConnection CreateSQLConnect()
        {
            string sc = sConnectDB;
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

        public static void LogPacket(byte[] bb, long LogId)
        {
            //throw new NotImplementedException();

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
    }
}
