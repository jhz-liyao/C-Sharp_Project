using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace CameraView
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SetPicCallback picCallback = null;
        private void Form1_Load(object sender, EventArgs e)
        {
            picCallback = new SetPicCallback(flushPicImage);
        }

        Socket serverSocket = null; 
        private static byte[] result = new byte[10240];
        Thread serverStartThread = null;

        String IP = "";
        int PORT = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            
            if(button1.Text.Equals("预览"))
            {
                button1.Text = "停止预览";
                IP = tb_localip.Text;
                PORT = Int32.Parse(tb_port.Text);

               
                serverStartThread = new Thread(ListenClientConnect);
                serverStartThread.Start();
                cts = new CancellationTokenSource();
            }
            else if (button1.Text.Equals("停止预览"))
            {
                button1.Text = "预览";
                cts.Cancel(); 
                serverSocket.Close();
            }

        }

        /// <summary>
        /// 监听客户端连接
        /// </summary>
        CancellationTokenSource cts =new CancellationTokenSource();
        private void ListenClientConnect()
        {
            int count = 0;
            while (true)
            {
                if (cts.Token.IsCancellationRequested)
                { 
                    Console.WriteLine("线程被终止！");
                    break;
                }
                try
                {
                    //服务器IP地址
                    IPAddress ip = IPAddress.Parse(IP);
                    serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    //serverSocket.Bind(new IPEndPoint(ip, Int32.Parse(tb_port.Text)));  //绑定IP地址：端口
                    //serverSocket.Listen(1);    //设定最多10个排队连接请求
                    serverSocket.Connect(new IPEndPoint(ip, PORT));
                    ClientHandler(serverSocket);
                }
                catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                } 
            }
        }


        private void ClientHandler(object obj)
        {
            int len = -1;
            int image_size = 0;
            Socket clientSocket = (Socket)obj;
            byte[] tmpBuff = new byte[4];

            len = clientSocket.Receive(tmpBuff, 4,SocketFlags.None);
            if (len < 4)
                MessageBox.Show("图片头大小信息接收失败");
            for (int i = 3; i >= 0; i--)
                image_size |= (tmpBuff[3-i] << (i * 8));
            //MessageBox.Show("图片大小为：" + image_size);
            MemoryStream memoryStream = new MemoryStream();
            tmpBuff = new byte[image_size];
            int count = 0, offset = 0 ;
            try
            {
                count = image_size;
                while (true)
                {
                    len = clientSocket.Receive(tmpBuff, count, SocketFlags.None);
                    if (len > 0)
                    {

                        count -= len;
                        offset += len; 
                        memoryStream.Write(tmpBuff, 0, len); 
                    }
                    if (count == 0)
                    {
                        count = image_size;
                        len = 0;
                        this.pictureBox1.Invoke(picCallback, memoryStream);
                        memoryStream = new MemoryStream();
                    }
                } 
                
                

            }
            catch (Exception ex) {
                //MessageBox.Show(ex.Message);
            }finally
            {
                clientSocket.Close();
            }
        }


        /*/// <summary>
        /// 执行socket通信
        /// </summary>
        private void ClientHandler(object obj)
        {
            int len = -1;
            Socket clientSocket = (Socket)obj;
            MemoryStream memoryStream = new MemoryStream();
            try
            {
                while (len != 0)
                {
                    len = clientSocket.Receive(result);
                    memoryStream.Write(result, 0, len);
                }
                this.pictureBox1.Invoke(picCallback, memoryStream);

            }
            catch (Exception ex) { }
            finally
            {
                clientSocket.Close();
            }
        }
        */

        delegate void SetPicCallback(MemoryStream memoryStream);
        void flushPicImage(MemoryStream memoryStream)
        {
            Image image = null; 
            try
            {
                long x = memoryStream.Length;
                image = System.Drawing.Image.FromStream(memoryStream);
                memoryStream.Close();
                //image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                pictureBox1.Image = image; 
                pictureBox1.Refresh(); 
            }catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
            
        }

       
    }
}
