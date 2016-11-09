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

        int x_max = 320, y_max = 240;
        Bitmap bmp = new Bitmap(320,240);
        SerialPort sp;
        delegate void Flush_textbox(byte[] databuff, int rectLen);  //委托
        Flush_textbox updateText;
        int fpss = 0;
        bool NewFps = false;
        byte[] tmp = new byte[2];
        int inc = 0;
        int r8 = 0, g8 = 0, b8 = 0;
        int r5 = 0, g6 = 0, b5 = 0;
 
        int x = 0, y = 0;
        
        public void flush_textbox(byte[] databuff, int rectLen)
        {
            int i = 0;
            for (i = 0; i < rectLen-1; i++)
            {
                if (databuff[i] == '\r' && databuff[i + 1] == '\n') {
                    x = 0;
                    y = 0;
                    inc = 0;
                    fpss++;
                    //MessageBox.Show("刷新帧");
                    textBox1.Text = fpss.ToString();
            
                    NewFps = true;
                    break;
                }
                NewFps = false;
            }
            if (NewFps == false) i = 0;

                for (; i < rectLen; i++)
                {
                    tmp[inc++] = databuff[i];
                    if (inc == 2)
                    {
                        inc = 0;
                        r5 = (tmp[0] & 0xF8) >> 3;
                        g6 = ((tmp[0] & 0x07) << 3 | (tmp[1] & 0xE0) >> 5);
                        b5 = (tmp[1] & 0x1F);


                        /*r8 = (r5 * 527 + 23) >> 6;
                        g8 = (g6 * 259 + 33) >> 6;
                        b8 = (b5 * 527 + 23) >> 6;*/
                        r8 = r5 * 255 / 31;
                        g8 = g6 * 255 / 63;
                        b8 = b5 * 255 / 31;
                        bmp.SetPixel(x, y, Color.FromArgb(r8, g8, b8));
                        x++;
                        if (x == x_max)
                        {
                            x = 0;
                            y++;
                        }
                        if (y == y_max)
                        {
                            y = 0;
                        }
                    }
                }
            //pictureBox1.Image = bmp;
            pictureBox1.BackgroundImage = bmp;
            pictureBox1.Refresh();
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

            updateText = flush_textbox;


            /* for(int x=0;x<240;x++)
                 for(int y=0;y<320;y++)
                     bmp.SetPixel(x,y,Color.FromArgb(10,80,200));
             pictureBox1.Image = bmp;*/

            //tmp[0] = ;
            //textBox1.Text = (unchecked((byte)(0x0f << 4))).ToString();
        }
    }
}
