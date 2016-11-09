using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LineChar;

namespace 盲区数据推导
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        int sam = 0;
        double zhouqi = 0;
        double pinlv = 0;
        double step = 0;
        string sinValStr = "";
        double resItem = 0;

        double max = 0;
        double min = 99999;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                step = Double.Parse(tb_step.Text);
                zhouqi = Double.Parse(tb_zhouqi.Text);
                pinlv = Double.Parse(tb_pinlv.Text);
                sam = Int32.Parse(tb_sam.Text);
                max = 0;
                min = 99999;
            }
            catch (Exception exc) {
                MessageBox.Show("参数异常");
            }
            sinValStr = "sin值：\r\n";
            for (int j = 0; j < zhouqi; j++)
            {
                resItem = 0;
                for (int i = 0; i < sam; i++)
                {
                    double val = Math.Sin(j / zhouqi * 2 * Math.PI + step * i % zhouqi / zhouqi * 2 * Math.PI);
                    double abs_value = Math.Abs(val); 
                    resItem += abs_value; 
                }
                if (resItem > max)
                    max = resItem;
                if (resItem < min)
                    min = resItem;
                sinValStr += ("组" + (j + 1) + ":\t" + Math.Round(resItem,2) + "\r\n");
            }

            sinValStr += "max:" + Math.Round(max,2) + "\r\n";
            sinValStr += "min:" + Math.Round(min, 2) + "\r\n";
            sinValStr += "scale:" + Math.Round((max - min) / min, 2) + "\r\n";
            textBox4.Text = sinValStr;

            pictureBox1.Image = new LineCharLib().flush(step, zhouqi, pinlv);
            pictureBox1.Refresh();

        }
    }
}
