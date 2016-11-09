using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
/*using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop;
using Microsoft.Office.Interop.Owc11;*/


namespace LineChar
{

    public partial class Form1 : Form
    {

        int com = 1;

        public Form1()
        {
            InitializeComponent();
        }

        SerialPort sp;
        LineCharLib lc = new LineCharLib();
        bool isPause = false;

        void flush(){
             lc.grap_init(int.Parse(textBox1.Text), int.Parse(textBox2.Text), int.Parse(textBox5.Text), float.Parse(textBox7.Text));
            pictureBox1.Image = lc.flush();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button2.TabIndex = 0;
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
            textBox1.Text = pictureBox1.Width.ToString();
            textBox2.Text = pictureBox1.Height.ToString();
            flush();
            timer1.Interval = int.Parse(textBox3.Text);
           
        }

        Random ran = new Random();
        //byte[] dataBuffer = new byte[100];
        string[] datas;
        private void timer1_Tick(object sender, EventArgs e)
        {
                try
                {
                    //int data = sp.ReadByte();
                    //string dataStr = sp.ReadLine();
                    string dataStr = sp.ReadExisting();
                    //sp.Read(dataBuffer, 0, 100);
                   // string[] datas = dataBuffer.ToString().Split(new char[] { '\n' });
                    if (dataStr.IndexOf("\r\n", 0, dataStr.Length) > -1){
                        dataStr = dataStr.Replace("\r\n","\n");
                    }
                    datas = dataStr.Split(new char[] { '\n' });
                    for (int i = 0; i < datas.Length-1; i++)
                    {
                        // int RandKey = ran.Next(-100,100);
                        lc.put_data(float.Parse(datas[i]));

                        
                        
                    }
                    pictureBox1.Image = lc.flush();
                    
                }
                catch (Exception ex)
                {
                    ex = null;
                }
           // }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            pictureBox1.Width = Form1.ActiveForm.Size.Width - 20;
            pictureBox1.Height = Form1.ActiveForm.Size.Height - 130;
            textBox1.Text = pictureBox1.Width.ToString() ;
            textBox2.Text = pictureBox1.Height.ToString();
            flush();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            try {
                float.Parse(textBox5.Text);
                flush();
            }catch(Exception ex){
                ex = null;
            }
            
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            try
            {
                float.Parse(textBox7.Text);
                flush();
            }
            catch (Exception ex)
            {
                ex = null;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (isPause)
            {
                isPause = false;
                button2.Text = "开始";
                timer1.Stop();
                
            }
            else {
                sp.DiscardInBuffer();
                isPause = true;
                button2.Text = "暂停";
                timer1.Start();
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                timer1.Interval = int.Parse(textBox3.Text);
            }
            catch (Exception ex)
            {
                ex = null;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                sp = new SerialPort(comboBox1.Text, 115200, Parity.None, 8, StopBits.One);

                sp.Open();
                sp.ReadTimeout = 1;
                com = 0;
            }
            catch (System.Exception ex)
            {
                ex = null;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lc.dataArrayIndex = 0;
            flush();
        }


    }
}
