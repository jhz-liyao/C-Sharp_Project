using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

namespace PicView
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Bitmap bmp = new Bitmap(240,320);
        SerialPort sp;
        delegate void Flush_textbox(byte[] databuff, int rectLen);  //委托
        Flush_textbox updateText;

        public void flush_textbox(byte[] databuff, int rectLen)
        {
            
        }

        private void readSerialPort(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            byte[] databuff = new byte[1000000];
            //byte[] str = Encoding.ASCII.GetBytes(sp.ReadExisting());
            // str = Encoding.Convert(Encoding.ASCII, Encoding.UTF8, str);
            //flush_textbox(Encoding.UTF8.GetString());

            //byte[] byteArray = Encoding.GetEncoding("UTF-8").GetBytes(sp.ReadExisting()); 
            int rectLen = sp.BytesToRead;
            sp.Read(databuff, 0, rectLen);
            this.Invoke(updateText, databuff, rectLen);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sp = new SerialPort("COM8", 460800, Parity.None, 8, StopBits.One);
            sp.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(readSerialPort);
            sp.ReceivedBytesThreshold = 4;//设置 DataReceived 事件发生前内部输入缓冲区中的字节数
            sp.Open();
            sp.ReadTimeout = 10;




            for(int x=0;x<240;x++)
                for(int y=0;y<320;y++)
                    bmp.SetPixel(x,y,Color.FromArgb(10,80,200));
            pictureBox1.Image = bmp;
        }
    }
}
