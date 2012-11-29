using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Battery110Server
{
    public partial class DataShow : Form
    {
        private DXBStudio.Terminal terminal;
        public DataShow(DXBStudio.Terminal terminal)
        {
            InitializeComponent();
            // TODO: Complete member initialization
            setTerminal(terminal);
        }
        private void ThGetData()
        {
            toolStripProgressBar1.Visible = true;
            System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ThreadStart(GetData));
            th.Start();
        }
        public void setTerminal(DXBStudio.Terminal terminal)
        {
            this.terminal = terminal;
            byte[] bb = BitConverter.GetBytes(terminal.Id);
            Text = "数据展示——"+ string.Format("终端号：{0:X2}{1:X2}{2:X2}{3:X2}",bb[0],bb[1],bb[2],bb[3]);
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
        }

        private void GetData()
        {
            try
            {
                DataTable dt = DXBStudio.DBHelp.GetTerminalData(terminal.Id, dateTimePicker1.Value, dateTimePicker2.Value);
                this.Invoke((EventHandler)delegate
                {                    
                    if (dt != null)
                        dataGridView1.DataSource = dt;
                    toolStripProgressBar1.Visible = false;
                }
                );
            }
            catch { }
        }

        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value <= dateTimePicker2.Value)
            {
                ThGetData();
            }
            else
            {
                MessageBox.Show("日期不正确！");
            }
        }

        private void DataShow_Load(object sender, EventArgs e)
        {
            
        }

        private void DataShow_Shown(object sender, EventArgs e)
        {
            ThGetData();
        }

        private void DataShow_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

    }
}
