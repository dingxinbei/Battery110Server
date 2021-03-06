﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Battery110Server
{
    public partial class Main : Form
    {
        private DXBStudio.BTServer bt;
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            InitData();
        }

        private void InitData()
        {
            //throw new NotImplementedException();
            try
            {
                bt = new DXBStudio.BTServer();

                tbDatabaseAddress.Text = DXBStudio.DBHelp.sDbAddress;
                tbDbPort.Text = DXBStudio.DBHelp.sDbPort;
                tbPass.Text = DXBStudio.DBHelp.sDbPass;
                tbDbUser.Text = DXBStudio.DBHelp.sDbUser;
                tbIpAddress.Text = bt.Ip;
                if (bt.Ip == "0.0.0.0")
                    checkBox2.Checked = true;
                tbPort.Text = bt.Port.ToString();

                pbDatabase.Style = ProgressBarStyle.Marquee;

                DXBStudio.Terminal.InitData(DXBStudio.BTServer.Mac);
                ////////////////////////////////////////////
                //int i =0;
                foreach (DXBStudio.Terminal t in DXBStudio.Terminal.lTerminals)
                {
                    AppRow(dataGridView1,t);
                }
                AsynOpenListening();
            }
            catch (Exception e){
                MessageBox.Show(e.Message);
            }
        }

        private void AsynOpenListening()
        {
            System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ThreadStart(bt.Open));
            th.Start();

            if (bt.State == 1)
                pbListen.Enabled = true;
            else
                pbListen.Enabled = false;
        }
        
        public void AppRow(DataGridView dataGridView1, DXBStudio.Terminal t)
        {
            
            dataGridView1.Rows.Add();
            int i = dataGridView1.Rows.Count;
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Yellow;
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Value = i ;
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1].Value = t.Id;
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Tag = t;
            t.RowIndex = dataGridView1.Rows[i - 1].Index;
            //初始化无间隔发送时间
            //dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[2].Value = 
            t.StateChange += new DXBStudio.Terminal._StateChange(t_StateChange);
            t.RecvData += new DXBStudio.Terminal._RecvData(t_RecvData);
        }

        void t_RecvData(DXBStudio.Terminal sender)
        {
            if (sender.State == DXBStudio.Terminal.ConnectState.Normal)
            {
                dataGridView1.Rows[sender.RowIndex].DefaultCellStyle.BackColor = Color.Green;
                dataGridView1.Rows[sender.RowIndex].Cells[2].Value = "连接";
            }
            if (sender.LastRecv != null)
            {
                dataGridView1.Rows[sender.RowIndex].Cells[3].Value = (sender.NowRecv - sender.LastRecv).TotalSeconds;
                sender.GPRSPeriod = (int)dataGridView1.Rows[sender.RowIndex].Cells[3].Value;
            }
        }

        void t_StateChange(DXBStudio.Terminal sender,DXBStudio.Terminal.ConnectState cs)
        {
            //throw new NotImplementedException();
            if (cs == DXBStudio.Terminal.ConnectState.Normal)
            {
                dataGridView1.Rows[sender.RowIndex].DefaultCellStyle.BackColor = Color.Green;
                dataGridView1.Rows[sender.RowIndex].Cells[2].Value = "连接";
                lConNums.Text = (int.Parse(lConNums.Text) + 1).ToString();
                return;
            }
            if (cs == DXBStudio.Terminal.ConnectState.Disconnect)
            {
                dataGridView1.Rows[sender.RowIndex].DefaultCellStyle.BackColor = Color.Yellow;
                dataGridView1.Rows[sender.RowIndex].Cells[2].Value = "未连接";
                lConNums.Text = (int.Parse(lConNums.Text) - 1).ToString();
                return;
            }

            dataGridView1.Rows[sender.RowIndex].Cells[2].Value = "严重问题";
            dataGridView1.Rows[sender.RowIndex].DefaultCellStyle.BackColor = Color.Red;
            lConNums.Text = (int.Parse(lConNums.Text) - 1).ToString();
            return;
        }

        private void bDatabaseReset_Click(object sender, EventArgs e)
        {
            MessageBox.Show("勿轻易修改此配置，请联系管理员！");
        }

        private void bActive_Click(object sender, EventArgs e)
        {
            try
            {
                System.Net.IPAddress ipa ;
                if (System.Net.IPAddress.TryParse(tbIpAddress.Text, out ipa))
                {
                    //判断ip port 是否一样。
                    if (bt.Ip != tbIpAddress.Text.Trim() || bt.Port != int.Parse(tbPort.Text))
                    {
                        //add code 
                        bt.Close();

                        bt = new DXBStudio.BTServer(tbIpAddress.Text.Trim(),int.Parse(tbPort.Text));
                        AsynOpenListening();
                    }
                    else
                        MessageBox.Show("没有变化不修改！");
                }
                else
                    MessageBox.Show("IP地址不标准");
                

            }catch{
                MessageBox.Show("请检测Port是否是数字,是否超出范围！");
            }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            propertyGrid1.SelectedObject = dataGridView1.Rows[e.RowIndex].Tag;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                tbIpAddress.Text = "0.0.0.0";
                tbIpAddress.Enabled = false;
            }
            else
            {
                tbIpAddress.Enabled = true;
                tbIpAddress.Focus();
            }
        }
        /// <summary>
        /// 5'' 检查一次队列，看是否有新增的 Terminal。。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (DXBStudio.Terminal t in DXBStudio.Terminal.lTerminals)
            {
                if (t.RowIndex == -1)
                {
                    AppRow(dataGridView1, t);
                }
            }
            lRegNums.Text = DXBStudio.Terminal.lTerminals.Count.ToString();

        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            DXBStudio.Terminal.ReleaseALL();
            bt.Close();
            Application.Exit();
        }
        private DataShow ds;
        private void bDataLog_Click(object sender, EventArgs e)
        {
            //展示数据记录
            if (dataGridView1.SelectedRows.Count == 1)
            {
                if (ds == null)
                {
                    ds = new DataShow((DXBStudio.Terminal)dataGridView1.SelectedRows[0].Tag);
                    ds.Show();
                }
                else
                {
                    ds.setTerminal((DXBStudio.Terminal)dataGridView1.SelectedRows[0].Tag);
                    ds.Show();
                }

            }
            else
            {
                MessageBox.Show("选中的行不正确！？");
            }
        }
        private CanSet dSet;
        private void bCanSet_Click(object sender, EventArgs e)
        {
            //每次只设置一个 can信息，协议号 21  
            // CAN 数据上报 00 20
            // CAN 数据下传 00 21
            if (dataGridView1.SelectedRows.Count == 1)
            {
                if (dSet == null)
                {
                    dSet = new CanSet((DXBStudio.Terminal)dataGridView1.SelectedRows[0].Tag);
                    dSet.Show();
                }
                else
                {
                    dSet.setTerminal((DXBStudio.Terminal)dataGridView1.SelectedRows[0].Tag);
                    dSet.Show();
                }

            }
            else
            {
                MessageBox.Show("选中的行不正确！？");
            }
        }

    }
}
