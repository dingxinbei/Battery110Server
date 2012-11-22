using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Terminal
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        private static string serverAddress = "127.0.0.1";
        //private const string serverAddress = "server.battery110.com";
        private void Main_Load(object sender, EventArgs e)
        {
            try
            {
                System.IO.StreamReader sr = System.IO.File.OpenText("config.ini");
                serverAddress = sr.ReadLine();
                sr.Close();
            }
            catch { }

            Text += "  :"+serverAddress;

            initData();

            tt.Tick += new EventHandler(tt_Tick);
        }
        private Database d;
        private Terminal t;
        private void initData()
        {
            d = new Database();
            d.Error += new Database.error(d_Error);

            Server s = new Server(serverAddress, 8888);

            t = new Terminal(s);

            tbPeriod.Text = "5";//默认5秒发送一次
            tbTerminalID.Text = t.Phone.ToString();
            tbTerminalNo.Text = t.TerminalNo.ToString();
            tbRegId.Text = t.Phone;
            tbRegNo.Text = t.TerminalNo.ToString();

            cbNetType.SelectedIndex = t.NetType;
            maskedTextBox1.Text = t.Version;


            t_ConnectState(t.ConState);
            t.ConnectState += new Terminal._ConnectState(t_ConnectState);
            t.Message += new Terminal._Message(t_Message);
        }

        void t_Message(string sMessage)
        {
            this.Invoke((EventHandler)delegate {
                textBox1.AppendText(DateTime.Now.ToLongTimeString() + ":" + sMessage + "\r\n");
            });
            
        }

        void t_ConnectState(Terminal.State state)
        {
            try
            {
                this.Invoke((EventHandler)delegate
                {
                    switch (state)
                    {
                        case Terminal.State.Connect: tsslConnectState.Text = "连接状态"; tspbRun.Enabled = true; break;
                        case Terminal.State.disConnect: tsslConnectState.Text = "断开状态"; tspbRun.Enabled = false; break;
                        case Terminal.State.Send: tsslConnectState.Text = "发送状态"; tspbRun.Enabled = true; break;
                        case Terminal.State.Recv: tsslConnectState.Text = "接收状态"; tspbRun.Enabled = true; break;
                    }
                });
            }
            catch { }
        }

        void d_Error(Exception e)
        {
            this.Invoke((EventHandler)delegate
            {
                tsslDatabase.Text = e.Message;
            });
            //关闭连接，停止活动
            //add code
            
        }

        private void bReg_Click(object sender, EventArgs e)
        {
            
            if (t.ConState == Terminal.State.Connect)
            {
                
                t.Version = maskedTextBox1.Text;
                t.Phone = tbRegId.Text.Trim();

                if (!t.SendRegister(checkBox2.Checked))
                {
                    MessageBox.Show("发送不成功。");
                }
            }
            else
                MessageBox.Show("连接状态不适合发送，稍等 或 重启。");
        }
        private Timer tt = new Timer();
        private void bHand_Click(object sender, EventArgs e)
        {
            if (bHand.Text == "激活")
            {
                try
                {
                    
                    tt.Interval = int.Parse(tbPeriod.Text)*1000;
                    tt.Enabled = true;
                    tt.Start();
                    bHand.Text = "停止";
                }
                catch { MessageBox.Show("周期是否为数字？"); }
            }
            else
            {
                
                tt.Stop();
                tt.Enabled = false;
                bHand.Text = "激活";
            }
        }

        void tt_Tick(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (tabControl1.SelectedIndex == 0)
            {
                sendhand();
                return;
            }

        }
        private void sendhand()
        {
            if (t.ConState == Terminal.State.Connect)
            {
                if (checkBox1.Checked)
                    tbTerminalNo.Text = t.TerminalNo.ToString();
                else
                    t.TerminalNo = UInt32.Parse(tbTerminalNo.Text);
                //测试，只发送一个。。。以后变成循环模式。。
                if (!t.SendHand(checkBox1.Checked))
                {
                    textBox1.AppendText(DateTime.Now.ToLongTimeString() + ":发送不成功。" + Environment.NewLine);
                    //MessageBox.Show("发送不成功。");
                }
            }
            else
            {
                MessageBox.Show("连接状态不适合发送，稍等 或 重启。");
                bHand.Text = "激活";
            }
        }
        private void cbNetType_SelectedIndexChanged(object sender, EventArgs e)
        {
            t.NetType = (byte)cbNetType.SelectedIndex;
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                t.Close();
            }
            catch { }
        }
    }
}
