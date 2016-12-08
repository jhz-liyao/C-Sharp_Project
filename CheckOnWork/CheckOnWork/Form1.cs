using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CheckOnWork
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        OperationExcel oe = new OperationExcel();

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            if ("".Equals(textBox1.Text))
                textBox1.Text = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            else if ("".Equals(textBox4.Text))
                textBox4.Text = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            else if ("".Equals(textBox5.Text))
                textBox5.Text = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            else if ("".Equals(textBox6.Text))
                textBox6.Text = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            else if ("".Equals(textBox7.Text))
                textBox7.Text = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            //oe.openExcel(textBox1.Text);
           // textBox2.Text = oe.analysisWork();
            　
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;
            else
                e.Effect = DragDropEffects.None;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /*DateTime dt = new DateTime(2015,10,22,16,00,00);
            DateTime dt2 = new DateTime(2015, 10, 23, 15, 35, 00);
            textBox2.Text = (dt2 - dt).Hours.ToString();*/
        }

        private void button1_Click(object sender, EventArgs e)
        {
          

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!"".Equals(textBox1.Text))
            {
                oe.openExcel(textBox1.Text);
                textBox3.Text = oe.ReadEmployeeInfo();
            }
            if (!"".Equals(textBox4.Text))
            {
                oe.openExcel(textBox4.Text);
                textBox3.Text = oe.ReadEmployeeInfo();
            } 
            if (!"".Equals(textBox5.Text))
            {
                oe.openExcel(textBox5.Text);
                textBox3.Text = oe.ReadEmployeeInfo();
            } 
            if (!"".Equals(textBox6.Text))
            {
                oe.openExcel(textBox6.Text);
                textBox3.Text = oe.ReadEmployeeInfo();
            } 
            if (!"".Equals(textBox7.Text))
            {
                oe.openExcel(textBox7.Text);
                textBox3.Text = oe.ReadEmployeeInfo();
            }
            textBox2.Text = oe.analysisWork();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox4.Text = textBox5.Text = textBox6.Text = textBox7.Text = "";
        }
    }
}
