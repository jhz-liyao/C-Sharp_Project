using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Threading;
using System.Reflection;

namespace FirmwareUpdate
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        static SerialPort sp;
        string serialPortCur = "";
        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 30; i++) {
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

            
        }

        FileStream bootloaderFS ;
        FileStream programFS;
        FileStream bondFS;
        static Queue<byte> sendBuff = new Queue<byte>();
        static FileStream programEXEFS;
        static int fileSize = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            string newFile = textBox1.Text.Substring(0, textBox1.Text.LastIndexOf('\\')) + "\\Image.bin";
            bootloaderFS = File.Open(textBox1.Text, FileMode.Open);
            programFS = File.Open(textBox2.Text, FileMode.Open);
            bondFS = File.Open(newFile, FileMode.Create);
           
            long bootAddr = Convert.ToInt32(bootaddr.Text, 16);
            long programAddr = Convert.ToInt32(proaddr.Text, 16);

            long offset = (programAddr - bootAddr -bootloaderFS.Length);

            for (int i = 0; i < bootloaderFS.Length;i++ )
            {
                bondFS.WriteByte((byte)bootloaderFS.ReadByte());
            }
            for(int i = 0;i<offset;i++){
                bondFS.WriteByte(0xff);
            }
            for (int i = 0; i < programFS.Length; i++)
            {
                bondFS.WriteByte((byte)programFS.ReadByte());
            }
            bootloaderFS.Close();
            programFS.Close();
            bondFS.Close();
            MessageBox.Show("合并成功！");
        }

        private void tabPage1_DragDrop(object sender, DragEventArgs e)
        {
            if ("".Equals(textBox1.Text))
            {
                textBox1.Text = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            }
            else if ("".Equals(textBox2.Text))
            {
                textBox2.Text = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            }
        }

        private void tabPage1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;
            else
                e.Effect = DragDropEffects.None;
        }

        private void tabPage2_DragDrop(object sender, DragEventArgs e)
        {
            textBox3.Text = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
        }

        private void tabPage2_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;
            else
                e.Effect = DragDropEffects.None;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            try {
                programEXEFS.Close();
            }catch(Exception ex){
                
            }
            try
            {
                programEXEFS = File.Open(textBox3.Text, FileMode.Open);
                textBox4.Text = programEXEFS.Length.ToString();
                programEXEFS.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button3.Text = "打开串口";
            serialPortCur = "";
            try
            {
                sp.Close();
            }
            catch (Exception ex) { }
            
        }
        delegate void Flush_TextBox(string text);  //委托
        Flush_TextBox updateText;  //实例化委托对象
        SerialDataReceivedEventHandler spHandler;
        private bool m_IsTryToClosePort = false;
        private bool m_IsReceiving = false;


        public void flush_textbox(string line_value)
        {
            textBox5.AppendText(line_value);
            textBox5.Select(textBox5.TextLength, 0);
            textBox5.ScrollToCaret();
            m_IsReceiving = false; // 关键!!!
        }

        void readSerialPort(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            if (m_IsTryToClosePort) // 关键！！！
            {
                return;
            }
            m_IsReceiving = true; // 关键!!!
            byte[] databuff = new byte[10240];
            int rectLen = sp.BytesToRead;
            sp.Read(databuff, 0, rectLen);
            this.Invoke(updateText, new string[] { Encoding.GetEncoding("GBK").GetString(databuff, 0, rectLen) });
            
        }


        private void button3_Click(object sender, EventArgs e)
        {
            if ("".Equals(serialPortCur))
            {
               
                try
                {
                    if (sp.IsOpen)
                        sp.Close();
                    m_IsTryToClosePort = false;
                    sp = new SerialPort(comboBox1.Text, 115200, 0, 8, StopBits.One);
                    sp.Open();
                    spHandler = new SerialDataReceivedEventHandler(readSerialPort);
                    sp.DataReceived += spHandler;
                    updateText = new Flush_TextBox(flush_textbox);  //实例化委托对象
                    button3.Text = "关闭串口";
                    serialPortCur = comboBox1.Text;
                }
                catch (Exception ex) {
                    MessageBox.Show("打开串口失败！");
                } 
               
            }
            else {
                button3.Text = "打开串口";
                serialPortCur = "";
                try {
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
        public static void UpdateData()
        {
            try
            {
                
                /*sp.Write(new byte[] { 0xfd }, 0, 1);
                Thread.Sleep(20);*/
                sp.Write(new byte[] { (byte)(fileSize >> 8) }, 0, 1);//文件长度
                Thread.Sleep(20);
                sp.Write(new byte[] { (byte)(fileSize) }, 0, 1);//文件长度
                Thread.Sleep(20);

                sp.Write(new byte[] { 0x00 }, 0, 1);
                /*Thread.Sleep(20);
                sp.Write(new byte[] { 0xf8 }, 0, 1);*/
                Thread.Sleep(1000);
                byte[] byteTmp = new byte[1024];
                int offset = 0, res = -1;
                
                while (res != 0)
                {
                    res = programEXEFS.Read(byteTmp, 0, 1024);
                    sp.Write(byteTmp, 0, res);
                    //fs.Write(byteTmp, 0, res);
                    offset += res;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("升级失败！");
            }
            finally
            {
                programEXEFS.Close();
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            
            if ("".Equals(serialPortCur)) { MessageBox.Show("串口未打开！"); return; }
            if ("".Equals(textBox3.Text)) { MessageBox.Show("文件不存在！"); return; }
            fileSize = int.Parse(textBox4.Text);
            programEXEFS = File.Open(textBox3.Text, FileMode.Open);
            textBox4.Text = programEXEFS.Length.ToString();

            Thread T_UpdateData = new Thread(new ThreadStart(UpdateData));
            T_UpdateData.Start();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox5.Text = "";
        }
    }
}
