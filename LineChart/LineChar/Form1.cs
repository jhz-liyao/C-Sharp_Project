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
        bool refreshRequestFlag = false;
        Queue serialData = new Queue();
        byte[] lineData = new byte[1024];

        byte tmpByte = 0, lastTmpByte = 0;
        int ind = 0;


        void flush()
        {
            ConfigVO configVO = new ConfigVO();
            configVO.grap_width = int.Parse(grap_widthBox.Text);
            configVO.grap_height = int.Parse(grap_heightBox.Text);
            configVO.minY = int.Parse(minYBox.Text);
            configVO.scaleX = float.Parse(scaleXBox.Text);
            configVO.scaleY = float.Parse(scaleYBox.Text);
            configVO.channelNum = int.Parse(channelNumBox.Text);
            configVO.datasize = int.Parse(datasizeBox.Text);
            configVO.max_min_flag = CB_max_min_flag.Checked;
            lc.grap_update(configVO);
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
                    channelNumBox.Text = channel.ToString();
                    for (int j = 0; j < channel; j++)
                    {
                        if (float.Parse(value[j]) < minValue)
                            minValue = float.Parse(value[j]);
                        lc.put_data(float.Parse(value[j]), j);//, j
                        //tb_console.Text = value[j];//
                        textBox8.Text = (lc.hisMaxValue - lc.hisMinValue).ToString();
                    }
                    //初始化时自动查找合适的y轴初始坐标
                    if (minYBox.Text == "=")
                    {
                        minYBox.Text = (minValue - float.Parse(scaleYBox.Text)).ToString();
                    }
                    refreshRequestFlag = true;
                    //FPS++;
                    //pictureBox1.Image = lc.flush();

                }
                catch (Exception ex)
                {
                    tb_console.AppendText(Encoding.GetEncoding("GBK").GetString(lineData, 0, ind)); 
                    
                    //MessageBox.Show(ex.Message);
                }
                ind = 0;
                lineData = new byte[1024];
            }
            m_IsReceiving = false; // 关键!!!
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

            btn_open.TabIndex = 0;
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

            minYBox.Text = ConfigUtil.GetValue("minY") == null ? "0" : ConfigUtil.GetValue("minY");
            scaleXBox.Text = ConfigUtil.GetValue("scaleX") == null ? "1" : ConfigUtil.GetValue("scaleX");
            scaleYBox.Text = ConfigUtil.GetValue("scaleY") == null ? "1" : ConfigUtil.GetValue("scaleY");
            datasizeBox.Text = ConfigUtil.GetValue("datasize") == null ? "1024" : ConfigUtil.GetValue("datasize");
             
            grap_widthBox.Text = pictureBox1.Width.ToString();
            grap_heightBox.Text = pictureBox1.Height.ToString();  
            flush();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                if (Form1.ActiveForm.Size.Width < 200 || Form1.ActiveForm.Size.Height < 200)
                    return;
                pictureBox1.Width = Form1.ActiveForm.Size.Width - 20;
                pictureBox1.Height = Form1.ActiveForm.Size.Height - 130;
                grap_widthBox.Text = pictureBox1.Width.ToString();
                grap_heightBox.Text = pictureBox1.Height.ToString();
                //textBox3.Text = (Form1.ActiveForm.Size.Width / 10).ToString();
                flush();
            }
            catch (Exception ex)
            {
            }
        }

        private void minYBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                float.Parse(minYBox.Text);
                flush();
            }
            catch (Exception ex)
            {
                ex = null;
            }

        }

        private void scaleYBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (float.Parse(scaleYBox.Text) > 0)
                    flush();
            }
            catch (Exception ex) { }
        }
        private void scaleXBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (float.Parse(scaleXBox.Text) > 0)
                    flush();
            }
            catch (Exception ex) { }
        }

        private void btn_clean_Click(object sender, EventArgs e)
        {
            lc.dataArrayIndex = 0;
            tb_console.Clear();
            flush();
        }

        private void channelNumBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (float.Parse(channelNumBox.Text) < 1)
                    throw new Exception();
                flush();
            }
            catch (Exception ex) { }
        }

        private void btn_open_Click(object sender, EventArgs e)
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
                    btn_open.Text = "关闭串口";
                    serialPortCur = comboBox1.Text;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("打开串口失败！");
                }

            }
            else
            {
                btn_open.Text = "打开串口";
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            FPS_Box.Text = FPS.ToString();
            FPS = 0;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (refreshRequestFlag)
            {
                FPS++;
                pictureBox1.Image = lc.flush();
                refreshRequestFlag = false;
            }
        }

        private void CB_max_min_flag_CheckedChanged(object sender, EventArgs e)
        {
            flush();
        }

        private void lb_help_Click(object sender, EventArgs e)
        {
            MessageBox.Show("数据格式：\\t分割通道\n\t \\r\\n一条数据结尾\n\t \\v清屏");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ConfigUtil.SetValue("minY",minYBox.Text);
            ConfigUtil.SetValue("scaleX",scaleXBox.Text);
            ConfigUtil.SetValue("scaleY",scaleYBox.Text);
            ConfigUtil.SetValue("datasize", datasizeBox.Text);
            ConfigUtil.Save();
        }

    }
}
