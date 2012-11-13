using System;
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
                tbPort.Text = bt.Port.ToString();

                pbDatabase.Style = ProgressBarStyle.Continuous;
            }
            catch (Exception e){
                MessageBox.Show(e.Message);
            }
        }

        private void bDatabaseReset_Click(object sender, EventArgs e)
        {

        }

        private void bActive_Click(object sender, EventArgs e)
        {
            
        }

    }
}
