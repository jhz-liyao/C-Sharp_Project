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

        byte[] tmp = new byte[2];
        int inc = 0;
        int r = 0, g = 0, b = 0;
        int x = 0, y = 0;
        int x_max = 240,y_max = 320;
        int bmp32 = 0;
        int color = 0;
        
        public void flush_textbox(byte[] databuff, int rectLen)
        {
            for (int i = 0; i < rectLen; i++) {
                tmp[inc++] = databuff[i];
                if (inc == 2) {
                    inc = 0;
                    /*r = tmp[0] & 0xF8;
                    g = (tmp[0] & 0x07) | (tmp[0] & 0xE0);
                    b = tmp[1] & 0x1F;*/
                    bmp32 = tmp[0] & tmp[1];

                    color = tmp[0] & 0xF8;
                    color += ((tmp[0] & 0x07) | (tmp[0] & 0xE0)) & 0x07e0;
                    color += (tmp[1] & 0x1F) & 0XF800;

                    
                    /*color = (*bmpbuf++) >> 3;		   		 	//B
                    color += ((u16)(*bmpbuf++) << 3) & 0X07E0;	//G
                    color += (((u16) * bmpbuf++) << 8) & 0XF800;	//R*/
                    r = color & 0xff0000;
                    g = color & 0x00ff00;
                    b = color & 0x0000ff;

                    /*if (r > 255) r = 255;
                    if (g > 255) g = 255;
                    if (b > 255) b = 255;*/
                    bmp.SetPixel(x, y, Color.FromArgb(r, g, b));
                    x++;
                    if (x == x_max - 1)
                    {
                        x = 0;
                        y++;
                    }
                    if(y == y_max -1){
                        y = 0;
                    }
                    pictureBox1.Image = bmp;
                    pictureBox1.Refresh();


                }
                switch (inc) {
                    case 0:
                        r = databuff[i];
                        inc++;
                        break;
                    case 1:
                        g = databuff[i];
                        inc++;
                        break;
                    case 2:
                        b = databuff[i];
                        inc++;
                    //case 3:
                        inc = 0;
                        bmp.SetPixel(x, y, Color.FromArgb(r, g, b));
                        x++;
                        if (x == x_max - 1)
                        {
                            x = 0;
                            y++;
                        }
                        if(y == y_max -1){
                            y = 0;
                        }
                        pictureBox1.Image = bmp;
                        pictureBox1.Refresh();

                            break;

                }

                
            }
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
            textBox1.Text = (unchecked((byte)(0x0f<<4))).ToString();
        }
    }
}
