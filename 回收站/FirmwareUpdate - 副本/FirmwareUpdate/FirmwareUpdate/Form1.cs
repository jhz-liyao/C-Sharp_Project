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

namespace FirmwareUpdate
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SerialPort sp;
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            textBox1.Text = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;
            else
                e.Effect = DragDropEffects.None;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] data = new byte[1];
            int count = 0;
            sp = new SerialPort("COM8", 115200, Parity.None, 8, StopBits.One);
            sp.Open();
            FileStream sr = File.Open(textBox1.Text,FileMode.Open);
            while (count <= sr.Length)
            {

                data[0] = (byte)sr.ReadByte();
                //Thread.Sleep(1);
                //textBox2.AppendText(((char)data[0]).ToString());
                 sp.Write(data,0, 1);
                 count++;
                 label1.Text = count.ToString();

            }

        }
    }
}
