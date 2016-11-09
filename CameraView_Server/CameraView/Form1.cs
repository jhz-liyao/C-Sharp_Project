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
        private void button1_Click(object sender, EventArgs e)
        {
            
            if(button1.Text.Equals("预览"))
            {
                button1.Text = "停止预览";
                //服务器IP地址
                IPAddress ip = IPAddress.Parse(tb_localip.Text);
                serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                serverSocket.Bind(new IPEndPoint(ip, Int32.Parse(tb_port.Text)));  //绑定IP地址：端口
                serverSocket.Listen(1);    //设定最多10个排队连接请求
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
                    Socket clientSocket = serverSocket.Accept();
                    count++;
                    Console.WriteLine("收到socket数据:" + (count++));
                    //Thread receiveThread = new Thread(ClientHandler);
                    //receiveThread.Start(clientSocket);
                    ClientHandler(clientSocket);
                }
                catch (Exception ex) { } 
            }
        }

        /// <summary>
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


        delegate void SetPicCallback(MemoryStream memoryStream);
        void flushPicImage(MemoryStream memoryStream)
        {
            Image image = null; 
            try
            {
                image = System.Drawing.Image.FromStream(memoryStream);
                memoryStream.Dispose();
                image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                pictureBox1.Image = image;
                pictureBox1.Refresh();
            }catch (Exception ex) { }
            
        }

       
    }
}
