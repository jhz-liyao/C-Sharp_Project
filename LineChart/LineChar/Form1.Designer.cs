namespace LineChar
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.grap_widthBox = new System.Windows.Forms.TextBox();
            this.grap_heightBox = new System.Windows.Forms.TextBox();
            this.minYBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.scaleYBox = new System.Windows.Forms.TextBox();
            this.btn_open = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btn_clean = new System.Windows.Forms.Button();
            this.tb_console = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.channelNumBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.datasizeBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.FPS_Box = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.label11 = new System.Windows.Forms.Label();
            this.scaleXBox = new System.Windows.Forms.TextBox();
            this.CB_max_min_flag = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lb_help = new System.Windows.Forms.Label();
            this.cb_channel1 = new System.Windows.Forms.CheckBox();
            this.cb_channel3 = new System.Windows.Forms.CheckBox();
            this.cb_channel4 = new System.Windows.Forms.CheckBox();
            this.cb_channel2 = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_autopara = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.pictureBox1.Location = new System.Drawing.Point(0, 90);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(880, 530);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // grap_widthBox
            // 
            this.grap_widthBox.Location = new System.Drawing.Point(50, 12);
            this.grap_widthBox.Name = "grap_widthBox";
            this.grap_widthBox.ReadOnly = true;
            this.grap_widthBox.Size = new System.Drawing.Size(58, 21);
            this.grap_widthBox.TabIndex = 2;
            this.grap_widthBox.Text = "880";
            // 
            // grap_heightBox
            // 
            this.grap_heightBox.Location = new System.Drawing.Point(50, 39);
            this.grap_heightBox.Name = "grap_heightBox";
            this.grap_heightBox.ReadOnly = true;
            this.grap_heightBox.Size = new System.Drawing.Size(58, 21);
            this.grap_heightBox.TabIndex = 2;
            this.grap_heightBox.Text = "530";
            // 
            // minYBox
            // 
            this.minYBox.Location = new System.Drawing.Point(187, 12);
            this.minYBox.Name = "minYBox";
            this.minYBox.Size = new System.Drawing.Size(52, 21);
            this.minYBox.TabIndex = 3;
            this.minYBox.Text = "0";
            this.minYBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.enterFork_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(118, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "Y轴起始值";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "宽";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "高";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(356, 40);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 12);
            this.label7.TabIndex = 6;
            this.label7.Text = "Y刻度";
            // 
            // scaleYBox
            // 
            this.scaleYBox.Location = new System.Drawing.Point(391, 38);
            this.scaleYBox.Name = "scaleYBox";
            this.scaleYBox.Size = new System.Drawing.Size(27, 21);
            this.scaleYBox.TabIndex = 7;
            this.scaleYBox.Text = "1";
            this.scaleYBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.enterFork_KeyDown);
            // 
            // btn_open
            // 
            this.btn_open.Location = new System.Drawing.Point(749, 8);
            this.btn_open.Name = "btn_open";
            this.btn_open.Size = new System.Drawing.Size(75, 23);
            this.btn_open.TabIndex = 11;
            this.btn_open.Text = "打开串口";
            this.btn_open.UseVisualStyleBackColor = true;
            this.btn_open.Click += new System.EventHandler(this.btn_open_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(293, 12);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(52, 20);
            this.comboBox1.TabIndex = 12;
            // 
            // btn_clean
            // 
            this.btn_clean.Location = new System.Drawing.Point(749, 35);
            this.btn_clean.Name = "btn_clean";
            this.btn_clean.Size = new System.Drawing.Size(75, 23);
            this.btn_clean.TabIndex = 11;
            this.btn_clean.Text = "清空";
            this.btn_clean.UseVisualStyleBackColor = true;
            this.btn_clean.Click += new System.EventHandler(this.btn_clean_Click);
            // 
            // tb_console
            // 
            this.tb_console.Location = new System.Drawing.Point(516, 2);
            this.tb_console.Multiline = true;
            this.tb_console.Name = "tb_console";
            this.tb_console.ReadOnly = true;
            this.tb_console.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tb_console.Size = new System.Drawing.Size(227, 82);
            this.tb_console.TabIndex = 13;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(254, 15);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 14;
            this.label8.Text = "串口";
            // 
            // channelNumBox
            // 
            this.channelNumBox.Location = new System.Drawing.Point(466, 37);
            this.channelNumBox.Name = "channelNumBox";
            this.channelNumBox.ReadOnly = true;
            this.channelNumBox.Size = new System.Drawing.Size(27, 21);
            this.channelNumBox.TabIndex = 13;
            this.channelNumBox.Text = "1";
            this.channelNumBox.TextChanged += new System.EventHandler(this.para_Changed);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(425, 40);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 9;
            this.label9.Text = "通道";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(4, 66);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 15;
            this.label10.Text = "峰谷差";
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(50, 63);
            this.textBox8.Name = "textBox8";
            this.textBox8.ReadOnly = true;
            this.textBox8.Size = new System.Drawing.Size(58, 21);
            this.textBox8.TabIndex = 2;
            this.textBox8.Text = "0";
            // 
            // datasizeBox
            // 
            this.datasizeBox.Location = new System.Drawing.Point(187, 38);
            this.datasizeBox.Name = "datasizeBox";
            this.datasizeBox.Size = new System.Drawing.Size(52, 21);
            this.datasizeBox.TabIndex = 3;
            this.datasizeBox.Text = "1024";
            this.datasizeBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.enterFork_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(431, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "FPS";
            // 
            // FPS_Box
            // 
            this.FPS_Box.Location = new System.Drawing.Point(466, 11);
            this.FPS_Box.Name = "FPS_Box";
            this.FPS_Box.ReadOnly = true;
            this.FPS_Box.Size = new System.Drawing.Size(27, 21);
            this.FPS_Box.TabIndex = 7;
            this.FPS_Box.Text = "1";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Interval = 20;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(356, 13);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(35, 12);
            this.label11.TabIndex = 6;
            this.label11.Text = "X刻度";
            // 
            // scaleXBox
            // 
            this.scaleXBox.Location = new System.Drawing.Point(391, 11);
            this.scaleXBox.Name = "scaleXBox";
            this.scaleXBox.Size = new System.Drawing.Size(27, 21);
            this.scaleXBox.TabIndex = 7;
            this.scaleXBox.Text = "1";
            this.scaleXBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.enterFork_KeyDown);
            // 
            // CB_max_min_flag
            // 
            this.CB_max_min_flag.AutoSize = true;
            this.CB_max_min_flag.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.CB_max_min_flag.Checked = true;
            this.CB_max_min_flag.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CB_max_min_flag.Location = new System.Drawing.Point(120, 65);
            this.CB_max_min_flag.Name = "CB_max_min_flag";
            this.CB_max_min_flag.Size = new System.Drawing.Size(108, 16);
            this.CB_max_min_flag.TabIndex = 16;
            this.CB_max_min_flag.Text = "最大、小值统计";
            this.CB_max_min_flag.UseVisualStyleBackColor = true;
            this.CB_max_min_flag.CheckedChanged += new System.EventHandler(this.para_Changed);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(118, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "缓冲区大小";
            // 
            // lb_help
            // 
            this.lb_help.AutoSize = true;
            this.lb_help.Location = new System.Drawing.Point(841, 9);
            this.lb_help.Name = "lb_help";
            this.lb_help.Size = new System.Drawing.Size(41, 12);
            this.lb_help.TabIndex = 17;
            this.lb_help.Text = "用法？";
            this.lb_help.Click += new System.EventHandler(this.lb_help_Click);
            // 
            // cb_channel1
            // 
            this.cb_channel1.AutoSize = true;
            this.cb_channel1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cb_channel1.Checked = true;
            this.cb_channel1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_channel1.Location = new System.Drawing.Point(5, 3);
            this.cb_channel1.Name = "cb_channel1";
            this.cb_channel1.Size = new System.Drawing.Size(54, 16);
            this.cb_channel1.TabIndex = 18;
            this.cb_channel1.Tag = "1";
            this.cb_channel1.Text = "通道1";
            this.cb_channel1.UseVisualStyleBackColor = true;
            this.cb_channel1.Visible = false;
            this.cb_channel1.CheckedChanged += new System.EventHandler(this.para_Changed);
            // 
            // cb_channel3
            // 
            this.cb_channel3.AutoSize = true;
            this.cb_channel3.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cb_channel3.Checked = true;
            this.cb_channel3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_channel3.Location = new System.Drawing.Point(123, 3);
            this.cb_channel3.Name = "cb_channel3";
            this.cb_channel3.Size = new System.Drawing.Size(54, 16);
            this.cb_channel3.TabIndex = 19;
            this.cb_channel3.Tag = "3";
            this.cb_channel3.Text = "通道3";
            this.cb_channel3.UseVisualStyleBackColor = true;
            this.cb_channel3.Visible = false;
            this.cb_channel3.CheckedChanged += new System.EventHandler(this.para_Changed);
            // 
            // cb_channel4
            // 
            this.cb_channel4.AutoSize = true;
            this.cb_channel4.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cb_channel4.Checked = true;
            this.cb_channel4.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_channel4.Location = new System.Drawing.Point(183, 3);
            this.cb_channel4.Name = "cb_channel4";
            this.cb_channel4.Size = new System.Drawing.Size(54, 16);
            this.cb_channel4.TabIndex = 20;
            this.cb_channel4.Tag = "4";
            this.cb_channel4.Text = "通道4";
            this.cb_channel4.UseVisualStyleBackColor = true;
            this.cb_channel4.Visible = false;
            this.cb_channel4.CheckedChanged += new System.EventHandler(this.para_Changed);
            // 
            // cb_channel2
            // 
            this.cb_channel2.AutoSize = true;
            this.cb_channel2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cb_channel2.Checked = true;
            this.cb_channel2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_channel2.Location = new System.Drawing.Point(65, 3);
            this.cb_channel2.Name = "cb_channel2";
            this.cb_channel2.Size = new System.Drawing.Size(54, 16);
            this.cb_channel2.TabIndex = 21;
            this.cb_channel2.Tag = "2";
            this.cb_channel2.Text = "通道2";
            this.cb_channel2.UseVisualStyleBackColor = true;
            this.cb_channel2.Visible = false;
            this.cb_channel2.CheckedChanged += new System.EventHandler(this.para_Changed);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cb_channel1);
            this.panel1.Controls.Add(this.cb_channel2);
            this.panel1.Controls.Add(this.cb_channel3);
            this.panel1.Controls.Add(this.cb_channel4);
            this.panel1.Location = new System.Drawing.Point(254, 63);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(247, 23);
            this.panel1.TabIndex = 19;
            // 
            // btn_autopara
            // 
            this.btn_autopara.Location = new System.Drawing.Point(750, 62);
            this.btn_autopara.Name = "btn_autopara";
            this.btn_autopara.Size = new System.Drawing.Size(75, 23);
            this.btn_autopara.TabIndex = 20;
            this.btn_autopara.Text = "自动适配";
            this.btn_autopara.UseVisualStyleBackColor = true;
            this.btn_autopara.Click += new System.EventHandler(this.btn_autopara_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(880, 620);
            this.Controls.Add(this.btn_autopara);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lb_help);
            this.Controls.Add(this.CB_max_min_flag);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.channelNumBox);
            this.Controls.Add(this.tb_console);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btn_clean);
            this.Controls.Add(this.btn_open);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.FPS_Box);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.scaleXBox);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.scaleYBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.datasizeBox);
            this.Controls.Add(this.minYBox);
            this.Controls.Add(this.textBox8);
            this.Controls.Add(this.grap_heightBox);
            this.Controls.Add(this.grap_widthBox);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "数据图表";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox grap_widthBox;
        private System.Windows.Forms.TextBox grap_heightBox;
        private System.Windows.Forms.TextBox minYBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox scaleYBox;
        private System.Windows.Forms.Button btn_open;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button btn_clean;
        private System.Windows.Forms.TextBox tb_console;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox channelNumBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.TextBox datasizeBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox FPS_Box;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox scaleXBox;
        private System.Windows.Forms.CheckBox CB_max_min_flag;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lb_help;
        private System.Windows.Forms.CheckBox cb_channel1;
        private System.Windows.Forms.CheckBox cb_channel3;
        private System.Windows.Forms.CheckBox cb_channel4;
        private System.Windows.Forms.CheckBox cb_channel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_autopara;
    }
}

