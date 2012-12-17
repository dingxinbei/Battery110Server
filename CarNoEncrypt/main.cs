using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CarNoEncrypt
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = DxbEncrypt.EncryptString(textBox1.Text.Trim());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = DxbEncrypt.DecryptString(textBox2.Text.Trim());
        }
    }
}
