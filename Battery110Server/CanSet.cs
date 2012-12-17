using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Battery110Server
{
    public partial class CanSet : Form
    {
        private DXBStudio.Terminal terminal;

        public CanSet(DXBStudio.Terminal terminal)
        {
            InitializeComponent();
            // TODO: Complete member initialization
            setTerminal(terminal);
        }

        private void CanSet_Load(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add();
            int i = 0;
            //for (int i = 0; i < 28; i++)
            {
                dataGridView1.Rows[0].Cells[i++].Value = string.Format("0X{0X:2}",DXBStudio.Packet.bBegin[0]);//包头 78
                dataGridView1.Rows[0].Cells[i++].Value = string.Format("0X{0X:2}", DXBStudio.Packet.bBegin[1]);//78
                dataGridView1.Rows[0].Cells[i++].Value = "0X01";//序号
                //终端号
                dataGridView1.Rows[0].Cells[i++].Value = string.Format("0X{0X:2}",BitConverter.GetBytes(terminal.Id)[0]);
                dataGridView1.Rows[0].Cells[i++].Value = string.Format("0X{0X:2}", BitConverter.GetBytes(terminal.Id)[0]);
                dataGridView1.Rows[0].Cells[i++].Value = string.Format("0X{0X:2}", BitConverter.GetBytes(terminal.Id)[0]);
                dataGridView1.Rows[0].Cells[i++].Value = string.Format("0X{0X:2}", BitConverter.GetBytes(terminal.Id)[0]);
                //协议号
                dataGridView1.Rows[0].Cells[i++].Value = string.Format("0X{0X:2}", BitConverter.GetBytes((ushort)DXBStudio.Packet.Commands.CanDownInfo)[0]);
                dataGridView1.Rows[0].Cells[i++].Value = string.Format("0X{0X:2}", BitConverter.GetBytes((ushort)DXBStudio.Packet.Commands.CanDownInfo)[1]);
                //信息长度  CANSet定长 ……
                dataGridView1.Rows[0].Cells[i++].Value = "0X00";//, BitConverter.GetBytes((ushort)DXBStudio.Packet.Commands.CanDownInfo)[0]);
                dataGridView1.Rows[0].Cells[i++].Value = "0X0D";//13 4 id 1 len 8 info;// string.Format("0X{0X:2}", BitConverter.GetBytes((ushort)DXBStudio.Packet.Commands.CanDownInfo)[1]);
                //CAN ID 4byte 可以设置
                dataGridView1.Rows[0].Cells[i++].Value = "0X00";
                dataGridView1.Rows[0].Cells[i++].Value = "0X00";
                dataGridView1.Rows[0].Cells[i++].Value = "0X00";
                dataGridView1.Rows[0].Cells[i++].Value = "0X00";
                //长度 自动计算后得出
                dataGridView1.Rows[0].Cells[i++].Value = "0X00";
                //8byte 信息
                dataGridView1.Rows[0].Cells[i++].Value = "0X00"; dataGridView1.Rows[0].Cells[i++].Value = "0X00"; dataGridView1.Rows[0].Cells[i++].Value = "0X00"; dataGridView1.Rows[0].Cells[i++].Value = "0X00"; dataGridView1.Rows[0].Cells[i++].Value = "0X00"; dataGridView1.Rows[0].Cells[i++].Value = "0X00"; dataGridView1.Rows[0].Cells[i++].Value = "0X00"; dataGridView1.Rows[0].Cells[i++].Value = "0X00";
                //crc 计算获的
                dataGridView1.Rows[0].Cells[i++].Value = "0X00";
                dataGridView1.Rows[0].Cells[i++].Value = "0X00";
                //end
                dataGridView1.Rows[0].Cells[i++].Value = string.Format("0X{0X:2}", DXBStudio.Packet.bEnd[0]);//包尾 0D
                dataGridView1.Rows[0].Cells[i++].Value = string.Format("0X{0X:2}", DXBStudio.Packet.bEnd[0]);//包 0A
            }
        }

        public void setTerminal(DXBStudio.Terminal terminal)
        {
            this.terminal = terminal;

            Text = "Can 设置 ， 终端号：" + terminal.Id.ToString();
        }
    }
}
