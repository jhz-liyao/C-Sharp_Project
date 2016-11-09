using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;

namespace AnalyzeVoltageExcel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        OperationParaVO operationPataVO = new OperationParaVO();
        OperationExcel oe = new OperationExcel();
        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 50; i++) {
                topline.Items.Add(i);
                endline.Items.Add(i);
            }
            //oe.buildFile(operationPataVO);
            //MessageBox.Show(sheet.UsedRange.CurrentRegion.Rows.Count.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(topline.Text.Length == 0 ||
               endline.Text.Length == 0 ||
               avgline.Text.Length == 0
                ){
                MessageBox.Show("不能为空");
                return;
            }
            operationPataVO = new OperationParaVO();
            try
            {
                operationPataVO.topline = float.Parse(topline.Text);
                operationPataVO.endline = float.Parse(endline.Text);
                operationPataVO.avgline = int.Parse(avgline.Text);
                operationPataVO.selectColumn = int.Parse(selectColumn.Text);
                operationPataVO.filePath = filepath.Text;
                operationPataVO.sheetIndex = 1;
            }
            catch {
                MessageBox.Show("参数错误");
                return;
            }

            oe.openExcel(operationPataVO);
            oe.analyzeExcel(operationPataVO);
            oe.buildFile(operationPataVO);
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            filepath.Text = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;
            else
                e.Effect = DragDropEffects.None;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
