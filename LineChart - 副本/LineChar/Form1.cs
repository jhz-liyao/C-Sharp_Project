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
        bool isInit = true;
        bool isPause = false;

        void flush()
        {
            
            lc.grap_update(int.Parse(textBox1.Text), int.Parse(textBox2.Text), int.Parse(textBox5.Text), float.Parse(textBox7.Text), int.Parse(textBox6.Text));
            pictureBox1.Image = lc.flush(); 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            isInit = true;
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
            isInit = false;
           
        }

        Random ran = new Random();
       // byte[] dataBuffer = new byte[1024];
        string[] datas;
        string shengyu = "";
        private void timer1_Tick(object sender, EventArgs e)
        {
                try
                {
                    //int data = sp.ReadByte();
                    //string dataStr = sp.ReadLine(); 

                    int channel = 0;
                    string dataStr = sp.ReadExisting();
                    //sp.Read(dataBuffer, 0, 100);
                   // string[] datas = dataBuffer.ToString().Split(new char[] { '\n' });
                    
                   //将\r\n转换为\n
                    if (dataStr.IndexOf("\r\n", 0, dataStr.Length) > -1){
                        dataStr = dataStr.Replace("\r\n","\n");
                    }

                    //连接上次截取的数据
                    dataStr = shengyu + dataStr;

                    //将最后\n后的数据截取保存
                    int begin = 0;
                    int last = dataStr.LastIndexOf('\n');
                    if (dataStr.LastIndexOf('\n') != dataStr.Length)
                    {
                        if (last == -1)
                        {
                            shengyu = dataStr;
                            return;
                        }
                        shengyu = dataStr.Substring(last, dataStr.Length - last);
                    }

                    //将首个\n到末个\n的数据截取
                    begin = dataStr.IndexOf('\n', 0);
                    last = dataStr.LastIndexOf('\n');

                    if (begin == last)
                        return;
                    begin = begin + 1;//去掉最前面的\n
                    dataStr = dataStr.Substring(begin, last-begin);

                    datas = dataStr.Split(new char[] { '\n' });
                    channel = datas[0].Split(new char[] { '\t' }).Length;
                    textBox6.Text = channel.ToString();
                    string[,] channelData = new string[channel, datas.Length];
                    float minValue = 999999;
                    for (int i = 0; i < datas.Length; i++)
                    {
                        string[] value = datas[i].Split(new char[] { '\t' });
                        for (int j = 0; j < channel; j++)
                        {
                            if (float.Parse(value[j]) < minValue)
                                minValue = float.Parse(value[j]);
                            lc.put_data(float.Parse(value[j]),j);//, j
                            textBox4.Text = value[j];//datas[i];
                            textBox8.Text = (lc.hisMaxValue - lc.hisMinValue).ToString();
                        }
                    }
                    /*
                    datas = dataStr.Split(new char[] { '\n' });
                    for (int i = 0; i < datas.Length; i++)
                    {
                        // int RandKey = ran.Next(-100,100);
                        lc.put_data(float.Parse(datas[i]));
                        textBox4.Text = datas[i];
                        
                    }*/
                    //初始化时自动查找合适的y轴初始坐标
                    if (textBox5.Text == "=")
                    {
                        textBox5.Text = (minValue - float.Parse(textBox7.Text)).ToString();
                    }
                    pictureBox1.Image = lc.flush();

              }
              catch (Exception ex)
              {
                  //MessageBox.Show(ex.Message);
              }
           // }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (Form1.ActiveForm.Size.Width < 200 || Form1.ActiveForm.Size.Height < 200)
                return;
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
                if(float.Parse(textBox7.Text)>0)
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

        delegate void Flush_textbox(string text);  //委托
        Flush_textbox updateText;

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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                sp = new SerialPort(comboBox1.Text, 115200, Parity.None, 8, StopBits.One);
                sp.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(readSerialPort);
                sp.Open();
                //sp.ReadTimeout = 1;
                com = 0;
            }
            catch (System.Exception ex)
            {
                if(!isInit)
                    MessageBox.Show("当前串口不可用");
            }
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
                if(float.Parse(textBox6.Text)<1)
                    throw new Exception();
                flush();
            }
            catch (Exception ex)
            {
                ex = null;
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
