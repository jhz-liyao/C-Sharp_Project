using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Threading;

namespace ADC_READ
{
    public partial class Form1 : Form
    {

        FileStream fs;
        StreamWriter sw;
        SerialPort sp;
        int com = 1;  
        Thread t;

        string data = "";
        string line_data = "";
        string tem_data = "";
        bool line_ready = false;
        int delay = 1; 

        public Form1()
        {
            InitializeComponent();
        }
        public void flush_textbox(string line_value) {
            textBox1.AppendText(line_value);
            textBox1.Focus();
            textBox1.Select(textBox1.TextLength, 0);
            textBox1.ScrollToCaret();
        }

        string num_convert(int num){
            data = (num * 3300 / 255 / 1000.0).ToString();
            for (int i = 0; i < (6 - data.Length); i++) {
                data += "0";
            }
            data = data.Substring(0, 4);
            return data;
        }
        public void read_SerialPort()
        {
            Thread.Sleep(500);
            int[] line_byte = new int[9];
            int cur_byte;
            int cur_index = 0;
            int prev = 0;
            while (true)
            {
                //sw.Write(sp.ReadLine().ToString() + "\r\n");
                cur_byte = sp.ReadByte();
              //  sw.Write(cur_byte);
                prev = cur_index - 1;
                if(prev < 0)
                    prev = line_byte.Length -1;
                if (line_byte[prev] == '\r' && cur_byte == '\n' && cur_index != 8)
                {
                    line_byte = new int[9];
                    cur_index = 0;
                    continue;
                }

                line_byte[cur_index] = cur_byte;
                
                //flush_textbox(val);

                cur_index++;
                if (cur_index == 9)
                {
                    for (int i = 0; i < 7; i++) {
                        tem_data += (num_convert(line_byte[i]) + "\t");
                    }
                    tem_data += "\r\n";
                    sw.Write(tem_data);
                    line_data = tem_data;
                    tem_data = "";
                    line_ready = true;

                    line_byte = new int[9];

                   

                    cur_index = 0;
                   // while (line_ready) ;
                }

                if (delay - 1 > 0)
                    Thread.Sleep(delay);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            delay = Int32.Parse(textBox2.Text);
            //second = Int32.Parse(textBox3.Text); 
            fs = new FileStream("D:\\ADC_READ.txt", FileMode.Create);
            sw = new StreamWriter(fs, Encoding.Default);
            sw.Write("ADC1\tADC2\tADC3\tADC4\tADC5\tADC6\tADC7\r\n");
            t = new Thread(read_SerialPort);
            timer1.Start();
            //timer2.Start();
            t.Start();
           /* while (second > 0)
            if (line_ready)
            {
                line_ready = false;
                if (textBox1.TextLength > 100000)
                    textBox1.Text = "";
                textBox1.AppendText(line_data);
                line_data = "";
                if(delay-1 >0)
                    Thread.Sleep(delay);
            }*/
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           // second--;
            //MessageBox.Show("123");
            if (line_ready)
            {
                line_ready = false;
                if (textBox1.TextLength > 100000)
                    textBox1.Text = "";
                textBox1.AppendText(line_data);
                line_data = "";
                if (delay - 1 > 0)
                    Thread.Sleep(delay);
            }
           /* if (line_ready)
            {

                if (textBox1.TextLength > 100000)
                    textBox1.Text = "";
                textBox1.AppendText(line_data);
                line_data = "";
                //Thread.Sleep(delay);
                line_ready = false;
            }*/
            //textBox1.Text = data;
            //textBox1.Focus();
            //textBox1.Select(textBox1.TextLength, 0);
            //textBox1.ScrollToCaret();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                sp = new SerialPort(comboBox1.Text, 115200, Parity.None, 8, StopBits.One);

                sp.Open();
                //sp.ReadTimeout = 1000;
                com = 0;
            }
            catch (System.Exception ex)
            {
                ex = null;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
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
                MessageBox.Show("没有找到可用的串口");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        { 
            MessageBox.Show(((int)'\n').ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            t.Suspend(); ;
            timer1.Stop();
            sw.Close();
            fs.Close();
            //sp.Close();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            MessageBox.Show("123");
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            t.Abort();
            sp.Close();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            timer1.Interval = Int32.Parse(textBox3.Text);
        }
    }
}
