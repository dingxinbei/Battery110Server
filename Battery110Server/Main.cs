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

                pbDatabase.Style = ProgressBarStyle.Continuous;

                DXBStudio.Ternimal.InitData(bt.Mac);
                ////////////////////////////////////////////
                int i =0;
                foreach (DXBStudio.Ternimal t in DXBStudio.Ternimal.lTernimals)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Yellow;
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Value = i + 1;
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1].Value = t.Id;
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Tag = t;
                    t.RowIndex = dataGridView1.Rows[dataGridView1.Rows.Count - 1].Index;
                    //初始化无间隔发送时间
                    //dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[2].Value = 
                    t.StateChange+=new DXBStudio.Ternimal._StateChange(t_StateChange);
                    t.RecvData += new DXBStudio.Ternimal._RecvData(t_RecvData);
                }

                bt.Open();
            }
            catch (Exception e){
                MessageBox.Show(e.Message);
            }
        }

        void t_RecvData(DXBStudio.Ternimal sender)
        {
            //throw new NotImplementedException();
            if (sender.LastRecv != null)
            {
                dataGridView1.Rows[sender.RowIndex].Cells[2].Value = (sender.NowRecv - sender.LastRecv).Milliseconds;
            }
        }

        void t_StateChange(DXBStudio.Ternimal sender,DXBStudio.Ternimal.ConnectState cs)
        {
            //throw new NotImplementedException();
            if (cs == DXBStudio.Ternimal.ConnectState.Normal)
            {
                dataGridView1.Rows[sender.RowIndex].DefaultCellStyle.BackColor = Color.Green;
                return;
            }
            if (cs == DXBStudio.Ternimal.ConnectState.Disconnect)
            {
                dataGridView1.Rows[sender.RowIndex].DefaultCellStyle.BackColor = Color.Yellow;
                return;
            }
            
            
                dataGridView1.Rows[sender.RowIndex].DefaultCellStyle.BackColor = Color.Red;
                return;
        }

        private void bDatabaseReset_Click(object sender, EventArgs e)
        {

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
                        bt.Open();
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

    }
}
