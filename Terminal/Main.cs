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

        private void Main_Load(object sender, EventArgs e)
        {
            initData();
        }
        private Database d;
        private Terminal t;
        private void initData()
        {
            d = new Database();
            d.Error += new Database.error(d_Error);

            Server s = new Server("127.0.0.1", 8888);

            t = new Terminal(s);

            tbPeriod.Text = "5";//默认5秒发送一次
            tbTerminalID.Text = t.Phone.ToString();
            tbTerminalNo.Text = t.TerminalNo.ToString();
            tbRegId.Text = t.Phone;
            tbRegNo.Text = t.TerminalNo.ToString();

            cbNetType.SelectedIndex = t.NetType;
            maskedTextBox1.Text = t.Version;

            

            t.ConnectState += new Terminal._ConnectState(t_ConnectState);

            t.Message += new Terminal._Message(t_Message);
        }

        void t_Message(string sMessage)
        {
            textBox1.AppendText(DateTime.Now.ToLongTimeString()+":"+sMessage+"\r\n");
        }

        void t_ConnectState(Terminal.State state)
        {
            switch (state)
            {
                case Terminal.State.Connect: tsslConnectState.Text = "连接状态"; tspbRun.Enabled = true; break;
                case Terminal.State.disConnect: tsslConnectState.Text = "断开状态"; tspbRun.Enabled = false; break;
                case Terminal.State.Send: tsslConnectState.Text = "发送状态"; tspbRun.Enabled = true; break;
                case Terminal.State.Recv: tsslConnectState.Text = "接收状态"; tspbRun.Enabled = true; break;
            }
        }

        void d_Error(Exception e)
        {
            tsslDatabase.Text = e.Message;
            //关闭连接，停止活动
            //add code
            
        }

        private void bReg_Click(object sender, EventArgs e)
        {
            t.Version = maskedTextBox1.Text;
            if (t.ConState == Terminal.State.Connect)
            {
                tbRegId.Text = t.Phone;
                tbRegNo.Text = t.TerminalNo.ToString();

                if (!t.SendRegister(checkBox2.Checked))
                {
                    MessageBox.Show("发送不成功。");
                }
            }
            else
                MessageBox.Show("连接状态不适合发送，稍等 或 重启。");
        }

        private void bHand_Click(object sender, EventArgs e)
        {
            if (t.ConState == Terminal.State.Connect)
            {
                tbTerminalID.Text = t.Phone;
                if (!t.SendHand(checkBox1.Checked))
                {
                    MessageBox.Show("发送不成功。");
                }
            }
            else
                MessageBox.Show("连接状态不适合发送，稍等 或 重启。");
        }

        private void cbNetType_SelectedIndexChanged(object sender, EventArgs e)
        {
            t.NetType = (byte)cbNetType.SelectedIndex;
        }
    }
}
