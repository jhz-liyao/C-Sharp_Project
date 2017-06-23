using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Collections;
/*using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop;
using Microsoft.Office.Interop.Owc11;*/


namespace LineChar
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Random ran = new Random();
        SerialPort sp;
        LineCharLib lc = new LineCharLib();

        delegate void Flush_LinePic();  //委托
        Flush_LinePic flush_LinePic;
        SerialDataReceivedEventHandler spHandler;
        private bool m_IsTryToClosePort = false;
        private bool m_IsReceiving = false;
        string serialPortCur = "";
        int FPS = 0;
        Queue serialData = new Queue();
        byte[] lineData = new byte[1024];

        byte tmpByte = 0, lastTmpByte = 0;
        int ind = 0;


        void flush()
        {
            lc.grap_update(int.Parse(textBox1.Text), int.Parse(textBox2.Text), int.Parse(textBox5.Text), float.Parse(textBox7.Text), int.Parse(textBox6.Text), int.Parse(textBox3.Text));
            pictureBox1.Image = lc.flush();

        }


        public void flush_Line()
        {
            while (serialData.Count > 0)
            {
                //textBox9.AppendText("\r\n" + serialData.Count.ToString());
                while (true)
                {
                    tmpByte = (byte)serialData.Dequeue();
                    lineData[ind++] = tmpByte;
                    if (lastTmpByte == '\r' && tmpByte == '\n')//取出队列中完整的一条记录
                        break;
                    if (tmpByte == '\v')
                    {
                        lc.clean_data();
                    }

                    lastTmpByte = tmpByte;
                    if (serialData.Count == 0)//队列中未找到\r\n则退出
                    {
                        m_IsReceiving = false; // 关键!!!
                        return;
                    }
                }

                try
                {
                    int channel = 0;
                    string dataStr = System.Text.Encoding.Default.GetString(lineData);
                    dataStr = dataStr.Substring(0, dataStr.IndexOf("\r\n"));
                    float minValue = 999999;
                    string[] value = dataStr.Split(new char[] { '\t' });

                    channel = value.Length;
                    textBox6.Text = channel.ToString();
                    for (int j = 0; j < channel; j++)
                    {
                        if (float.Parse(value[j]) < minValue)
                            minValue = float.Parse(value[j]);
                        lc.put_data(float.Parse(value[j]), j);//, j
                        textBox4.Text = value[j];//
                        textBox8.Text = (lc.hisMaxValue - lc.hisMinValue).ToString();
                    }
                    //初始化时自动查找合适的y轴初始坐标
                    if (textBox5.Text == "=")
                    {
                        textBox5.Text = (minValue - float.Parse(textBox7.Text)).ToString();
                    }
                    FPS++;
                    pictureBox1.Image = lc.flush();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                ind = 0;
                lineData = new byte[1024];
                m_IsReceiving = false; // 关键!!!
            }
        }
        private void readSerialPort(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            if (m_IsTryToClosePort) // 关键！！！
            {
                return;
            }
            byte[] databuff = new byte[sp.BytesToRead];
            //String str = sp.ReadExisting();
            //databuff = System.Text.Encoding.Default.GetBytes(str);
            // textBox9.AppendText("\n" + databuff.Length.ToString());
            int res = sp.Read(databuff, 0, databuff.Length);
            for (int i = 0; i < res; i++)
            {
                serialData.Enqueue(databuff[i]);
            }
            //for (int i = 0; i < databuff.Length; i++)
            //{
            //    serialData.Enqueue(databuff[i]);
            this.Invoke(flush_LinePic);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            button2.TabIndex = 0;
            for (int i = 0; i < 30; i++)
            {
                try
                {
                    sp = new SerialPort("COM" + i, 115200, 0, 8, StopBits.One);
                    sp.Open();
                    comboBox1.Items.Add("COM" + i);
                    sp.Close();
                }
                catch (Exception ex) { }
            }
            comboBox1.SelectedIndex = 0;
            textBox1.Text = pictureBox1.Width.ToString();
            textBox2.Text = pictureBox1.Height.ToString();
            flush();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (Form1.ActiveForm.Size.Width < 200 || Form1.ActiveForm.Size.Height < 200)
                return;
            pictureBox1.Width = Form1.ActiveForm.Size.Width - 20;
            pictureBox1.Height = Form1.ActiveForm.Size.Height - 130;
            textBox1.Text = pictureBox1.Width.ToString();
            textBox2.Text = pictureBox1.Height.ToString();
            textBox3.Text = (Form1.ActiveForm.Size.Width / 10).ToString();
            flush();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            try
            {
                float.Parse(textBox5.Text);
                flush();
            }
            catch (Exception ex)
            {
                ex = null;
            }

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (float.Parse(textBox7.Text) > 0)
                    flush();
            }
            catch (Exception ex) { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lc.dataArrayIndex = 0;
            flush();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (float.Parse(textBox6.Text) < 1)
                    throw new Exception();
                flush();
            }
            catch (Exception ex) { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if ("".Equals(serialPortCur))
            {
                try
                {
                    if (sp.IsOpen)
                        sp.Close();
                    m_IsTryToClosePort = false;
                    sp = new SerialPort(comboBox1.Text, 115200, 0, 8, StopBits.One);
                    //sp.ReceivedBytesThreshold = 100;
                    sp.Open();
                    spHandler = new SerialDataReceivedEventHandler(readSerialPort);
                    sp.DataReceived += spHandler;
                    flush_LinePic = new Flush_LinePic(flush_Line);  //实例化委托对象
                    button2.Text = "关闭串口";
                    serialPortCur = comboBox1.Text;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("打开串口失败！");
                }

            }
            else
            {
                button2.Text = "打开串口";
                serialPortCur = "";
                try
                {
                    m_IsTryToClosePort = true;
                    while (m_IsReceiving)
                    {
                        System.Windows.Forms.Application.DoEvents();
                    }

                    sp.Close();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            textBox9.Text = FPS.ToString();
            FPS = 0;
        }
    }
}
