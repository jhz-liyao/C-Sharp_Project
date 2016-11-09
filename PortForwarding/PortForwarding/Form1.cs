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
using System.Threading;
using System.Globalization;
namespace PortForwarding
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

       


        StringBuilder messageBuffer = new StringBuilder();

        List<Socket> controlerList = new List<Socket>();
        List<Socket> deviceList = new List<Socket>();
        Socket controlSocket = null;
        Thread controlThread = null;

        Socket deviceSocket = null;
        Thread deviceThread = null;

        Thread deviceTransfer = null;
        Thread controlTransfer = null;
        private void button1_Click(object sender, EventArgs e)
        {
            if(comboBox1.Text.Length == 0){
                return;
            }
            IPAddress ip = IPAddress.Parse(comboBox1.Text);
            controlSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            controlSocket.Bind(new IPEndPoint(ip, Int32.Parse(textBox1.Text)));  //绑定IP地址：端口
            controlSocket.Listen(10);    //设定最多10个排队连接请求
            controlThread = new Thread(ListenControlConnect);
            controlThread.Start();

            deviceSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            deviceSocket.Bind(new IPEndPoint(ip, Int32.Parse(textBox2.Text)));  //绑定IP地址：端口
            deviceSocket.Listen(10);    //设定最多10个排队连接请求
            deviceThread = new Thread(ListenDeviceConnect);
            deviceThread.Start();

            button1.Enabled = false;
            button2.Enabled = true;
        }




        /// <summary>
        /// 监听客户端连接
        /// </summary> 
        private void ListenControlConnect()
        {
            int count = 0;
            while (true)
            { 
                try
                {
                    Socket cSocket = controlSocket.Accept();
                    controlerList.Add(cSocket);
                    count++;
                    controlTransfer = new Thread(ControlTransfer);
                    controlTransfer.Start(cSocket);
                    messageBuffer.Append("Controler连接\t" + System.DateTime.Now.ToString() + "\r\n"); 
                }
                catch (Exception ex) { }
            }
        }

        /// <summary>
        /// 监听客户端连接
        /// </summary> 
        private void ListenDeviceConnect()
        {
            int count = 0;
            while (true)
            { 
                try
                {
                    Socket dSocket = deviceSocket.Accept();
                    deviceList.Add(dSocket);
                    count++;
                    deviceTransfer = new Thread(DeviceTransfer);
                    deviceTransfer.Start(dSocket);
                    messageBuffer.Append("Device连接\t" + System.DateTime.Now.ToString() + "\r\n");  
                }
                catch (Exception ex) {
                    
                }
            }
        }
         
        /// <summary>
        /// 执行数据操作
        /// </summary>  
        private void ControlTransfer(object soc)
        {
            byte[] dataBuff = new byte[1024];
            Socket cSocket = (Socket)soc;
            Socket dSocket = null;
            int i;
            while (true)
            {
                try
                {
                    int cnt = cSocket.Receive(dataBuff);
                    if (cnt == 0)
                        throw new Exception();
                    messageBuffer.Append("收到控制端:" + cnt + "字节数据\t" + System.DateTime.Now.ToString() + "\r\n");
                    for (i = 0; i < cnt; i++)
                        messageBuffer.Append(String.Format("{0:X} ", dataBuff[i]));
                    messageBuffer.Append("\r\n");
                    for (i = 0; i < deviceList.Count; i++)
                    {
                        dSocket = deviceList[i];
                        if (dSocket.Connected)
                            dSocket.Send(dataBuff, cnt, SocketFlags.None);
                    }
                }
                catch (Exception ex)
                {
                    controlerList.Remove(dSocket);
                    try
                    {
                        messageBuffer.Append("Controler断开连接\t" + System.DateTime.Now.ToString() + "\r\n");
                        messageBuffer.Append(ex.Message + "\r\n");
                        dSocket.Close();
                    }
                    catch (Exception ex1)
                    {
                    }
                    break;
                } 
            }
        }

        /// <summary>
        /// 执行数据操作
        /// </summary>  
        private void DeviceTransfer(object soc)
        {
            byte[] dataBuff = new byte[1024];
            Socket dSocket = (Socket)soc;
            Socket cSocket = null;
            int i;
            while (true)
            {
                try
                {
                    int cnt = dSocket.Receive(dataBuff);
                    if (cnt == 0)
                        throw new Exception();
                    messageBuffer.Append("收到设备端:" + cnt + "字节数据\t" + System.DateTime.Now.ToString() + "\r\n");
                    for(i = 0; i < cnt; i ++)
                        messageBuffer.Append(String.Format("{0:X} ", dataBuff[i]));
                    messageBuffer.Append("\r\n");
                    for (i = 0; i < controlerList.Count; i++)
                    {
                        cSocket = controlerList[i];
                        if (cSocket.Connected)
                            cSocket.Send(dataBuff, cnt, SocketFlags.None);
                    }
                }
                catch (Exception ex)
                {
                    controlerList.Remove(cSocket);
                    try{

                        messageBuffer.Append("Device断开连接\t" + System.DateTime.Now.ToString() + "\r\n");
                        messageBuffer.Append(ex.Message + "\r\n");
                        cSocket.Close();
                    }
                    catch (Exception ex1)
                    {
                    } 
                    break;
                } 
            }
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            string hostName = Dns.GetHostName();//本机名   
            System.Net.IPAddress[] addressList = Dns.GetHostAddresses(hostName);//会返回所有地址，包括IPv4和IPv6   
            foreach (IPAddress ip in addressList)  
            {
                comboBox1.Items.Add(ip.ToString());
            }
            comboBox1.SelectedIndex = 0 ; 
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            textBox4.Text = messageBuffer.ToString();
            textBox4.Select(textBox4.TextLength, 0);
            textBox4.ScrollToCaret();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            button1.Enabled = true;
            foreach(Socket socket in controlerList){
                try
                {
                    socket.Close();
                }
                catch (Exception ex1)
                {

                } 
            }
            controlerList = new List<Socket>();

            foreach (Socket socket in deviceList)
            {
                try
                {
                    socket.Close();
                }
                catch (Exception ex1)
                {

                } 
            }
            deviceList = new List<Socket>();

            controlSocket.Close();
            deviceSocket.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            messageBuffer = new StringBuilder();
        }
    }
}
