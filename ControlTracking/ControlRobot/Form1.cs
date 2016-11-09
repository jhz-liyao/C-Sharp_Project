using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

namespace ControlRobot
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        bool isInit = true;
        int com = 1, isPause = 0;
        SerialPort sp;


        ControlCMD ccmd = new ControlCMD();
        delegate void Flush_textbox(string text);  //委托
        Flush_textbox updateText;


        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 2;
            isInit = true;
            for (; com < comboBox1.Items.Count; com++)
            {
                comboBox1.SelectedIndex = com;
                if (com == 0)
                {
                    break;
                }
            }
            if (com != 0)
            {
                comboBox1.SelectedIndex = 0;
                MessageBox.Show("没有找到可用的串口");
            }
            ccmd.run(ccmd.stopCMD);

            //timer1.Start();


            updateText = new Flush_textbox(flush_textbox);  //实例化委托对象
        }

        public void flush_textbox(string line_value)
        {
            textBox1.AppendText(line_value);
            //textBox1.Focus();
            textBox1.Select(textBox1.TextLength, 0);
            textBox1.ScrollToCaret();
        }
         private void readSerialPort(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            byte[] databuff = new byte[1024];
            //byte[] str = Encoding.ASCII.GetBytes(sp.ReadExisting());
           // str = Encoding.Convert(Encoding.ASCII, Encoding.UTF8, str);
            //flush_textbox(Encoding.UTF8.GetString());
           
            //byte[] byteArray = Encoding.GetEncoding("UTF-8").GetBytes(sp.ReadExisting()); 
            int rectLen = sp.BytesToRead;
            sp.Read(databuff, 0, rectLen);
            if (isPause == 0)
            {
                
                this.Invoke(updateText, new string[] { Encoding.GetEncoding("GBK").GetString(databuff, 0, rectLen) });
            }
        }


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.W)
            {
                label1.Text = "前进";
                ccmd.run(ccmd.upCMD);
            }
            else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.S)
            {
                label1.Text = "后退";
                ccmd.run(ccmd.downCMD);
            }
            else if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
            {
                label1.Text = "左转";
                ccmd.run(ccmd.leftCMD);
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
            {
                label1.Text = "右转";
                ccmd.run(ccmd.rightCMD);
            }else if (e.KeyCode == Keys.Space)
            {
                label1.Text = "停止";
                ccmd.run(ccmd.stopCMD);
            }
            //textBox1.Text = "按下"+e.KeyCode;
            ccmd.curCMD[4] = ccmd.curCMD[7] = (byte)int.Parse(comboBox3.Text);
            sp.Write(ccmd.curCMD, 0, 11);
           
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (comboBox2.Text.Equals("微调模式"))
            {
                label1.Text = "停止";
                ccmd.run(ccmd.stopCMD);
                sp.Write(ccmd.curCMD, 0, 11);
            }
        }


        private void Form1_KeyDown(object sender, KeyPressEventArgs e)
        {

        }

        private void hiddenBtn_Click(object sender, EventArgs e)
        {
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                sp = new SerialPort(comboBox1.Text, 115200, Parity.None, 8, StopBits.One); 
                sp.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(readSerialPort);
                sp.ReceivedBytesThreshold = 4;//设置 DataReceived 事件发生前内部输入缓冲区中的字节数
                sp.Open();
                sp.ReadTimeout = 10;
                com = 0;
            }
            catch (System.Exception ex)
            {
                if (!isInit)
                    MessageBox.Show("当前串口不可用");
            }
            stop.Focus();
        }


        private void stop_Leave(object sender, EventArgs e)
        {
            stop.Focus();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            stop.Focus();
        }

        private void up_MouseDown(object sender, MouseEventArgs e)
        {
            label1.Text = "前进";
            ccmd.run(ccmd.upCMD);
            ccmd.curCMD[4] = ccmd.curCMD[7] = (byte)int.Parse(comboBox3.Text);
            sp.Write(ccmd.curCMD, 0, 11);
        }

        private void down_MouseDown(object sender, MouseEventArgs e)
        {
            label1.Text = "后退";
            ccmd.run(ccmd.downCMD);
            ccmd.curCMD[4] = ccmd.curCMD[7] = (byte)int.Parse(comboBox3.Text);
            sp.Write(ccmd.curCMD, 0, 11);
        }

        private void left_MouseDown(object sender, MouseEventArgs e)
        {
            label1.Text = "左转";
            ccmd.run(ccmd.leftCMD);
            ccmd.curCMD[4] = ccmd.curCMD[7] = (byte)int.Parse(comboBox3.Text);
            sp.Write(ccmd.curCMD, 0, 11);
        }

        private void right_MouseDown(object sender, MouseEventArgs e)
        {
            label1.Text = "右转";
            ccmd.run(ccmd.rightCMD);
            ccmd.curCMD[4] = ccmd.curCMD[7] = (byte)int.Parse(comboBox3.Text);
            sp.Write(ccmd.curCMD, 0, 11);
        }

       
        private void stop_MouseDown(object sender, MouseEventArgs e)
        {
            label1.Text = "停止";
            ccmd.run(ccmd.stopCMD);
            sp.Write(ccmd.curCMD, 0, 11);
        }

        private void stop_MouseUp(object sender, MouseEventArgs e)
        {
            if (comboBox2.Text.Equals("微调模式"))
            {
                label1.Text = "停止";
                ccmd.run(ccmd.stopCMD);
                sp.Write(ccmd.curCMD, 0, 11);
            }
        }

       
        

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                /*ccmd.curCMD[4] = (byte)int.Parse(comboBox3.Text);
                ccmd.curCMD[7] = (byte)int.Parse(comboBox3.Text);
                sp.Write(ccmd.curCMD, 0, 11);*/
                //flush_textbox(sp.ReadExisting().ToString());
                
                

                //Encoding.Convert(Encoding.ASCII, Encoding.UTF8, Encoding.ASCII.GetBytes(sp.ReadExisting()));
            }
            catch (Exception exc) { }
        }

        /*private void button1_Click(object sender, EventArgs e)//头部向左
        {
            sp.Write(new byte[]{0xFD, 0x02, 0x00, 0x3C, 0x00, 0x01, 0x00, 0xF8 }, 0, 8);
        }

        private void button2_Click(object sender, EventArgs e)//头部向右
        {
            sp.Write(new byte[] { 0xFD, 0x02, 0xF0, 0x3C, 0x00, 0x01, 0x00, 0xF8 }, 0, 8);
        }*/

        private void button3_Click(object sender, EventArgs e)//头部运动到
        {
            byte data = (byte)int.Parse(textBox2.Text);
            byte[] bd = { 0xFD, 0x02, 0x00, 0x3C, 0x00, 0x01, 0x00, 0xF8 };
            bd[2] = data;
            bd[3] = (byte)int.Parse(textBox6.Text);
            sp.Write(bd, 0, 8);

        }

        private void button11_Click(object sender, EventArgs e)
        {
            byte data = (byte)int.Parse(textBox4.Text);
            byte[] bd = { 0xFD, 0x02, 0x00, 0x3C, 0x00, 0x01, 0x00, 0xF8 };
            bd[2] = data;
            bd[3] = (byte)int.Parse(textBox6.Text);
            sp.Write(bd, 0, 8);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //FD 02 77 00 52 01 CC F8 
            sp.Write(new byte[] { 0xFD, 0x02, 0x77, 0x00, 0x00, 0x01, 0x00, 0xF8 }, 0, 8);
        }

       /* private void button5_Click(object sender, EventArgs e)
        {
            //FD 03 00 1E 00 1E 53 01 93 F8 
            sp.Write(new byte[] { 0xFD, 0x03, 0x00, 0x1E, 0x00, 0x1E, 0x00, 0x01, 0x00, 0xF8 }, 0, 10);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //FD 03 3C 1E 3C 1E 54 01 0C F8 
            sp.Write(new byte[] { 0xFD, 0x03, 0x3C, 0x1E, 0x3C, 0x1E, 0x00, 0x01, 0x00, 0xF8 }, 0, 10);
        }*/

        private void button7_Click(object sender, EventArgs e)
        {
            byte data = (byte)int.Parse(textBox3.Text);
            byte[] bd = { 0xFD, 0x03, 0x3C, 0x1E, 0x3C, 0x1E, 0x00, 0x01, 0x00, 0xF8 };
            bd[2] = data;
            bd[3] = (byte)int.Parse(textBox7.Text);
            bd[4] = data;
            bd[5] = (byte)int.Parse(textBox7.Text);
            sp.Write(bd, 0, 10);
        }
        private void button12_Click(object sender, EventArgs e)
        {
            byte data = (byte)int.Parse(textBox5.Text);
            byte[] bd = { 0xFD, 0x03, 0x3C, 0x1E, 0x3C, 0x1E, 0x00, 0x01, 0x00, 0xF8 };
            bd[2] = data;
            bd[3] = (byte)int.Parse(textBox7.Text);
            bd[4] = data;
            bd[5] = (byte)int.Parse(textBox7.Text);
            sp.Write(bd, 0, 10);
        }
        private void button9_Click(object sender, EventArgs e)//双翅停止
        {
            //FD 03 00 00 00 00 55 01 59 F8 
            sp.Write(new byte[] { 0xFD, 0x03, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0xF8 }, 0, 10);
        }

        private void button10_Click(object sender, EventArgs e)//清空
        {
            textBox1.Text = "";
        }

        private void button8_Click(object sender, EventArgs e)//暂停
        {
            if (isPause == 1)
                isPause = 0;
            else
                isPause = 1;

        }

        

       




       

       

       


    }
}
