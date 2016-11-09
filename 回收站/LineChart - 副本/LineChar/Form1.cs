using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
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

        private void button1_Click(object sender, EventArgs e)
        {
            List<String> list = new List<string>();
            list.Add("1");
            list.Add("2");
            list.Add("3");
             list[1] = "456";
             DateTime dt = new DateTime();
             DateTime dt2 = new DateTime();
             dt = DateTime.Now;
             dt2 = dt.AddDays(1);
             dt2 = dt2.AddHours(+1);
            MessageBox.Show((dt2-dt).Days.ToString());
        }


    }
}
