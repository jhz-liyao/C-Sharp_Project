namespace ControlRobot
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
            this.up = new System.Windows.Forms.Button();
            this.down = new System.Windows.Forms.Button();
            this.left = new System.Windows.Forms.Button();
            this.right = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.stop = new System.Windows.Forms.Button();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.button8 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.top = new System.Windows.Forms.Button();
            this.down1 = new System.Windows.Forms.Button();
            this.BaseSpeed = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // up
            // 
            this.up.Location = new System.Drawing.Point(434, 67);
            this.up.Name = "up";
            this.up.Size = new System.Drawing.Size(75, 59);
            this.up.TabIndex = 10;
            this.up.Text = "前";
            this.up.UseVisualStyleBackColor = true;
            this.up.Click += new System.EventHandler(this.up_Click);
            this.up.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.up.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.up.MouseDown += new System.Windows.Forms.MouseEventHandler(this.up_MouseDown);
            this.up.MouseUp += new System.Windows.Forms.MouseEventHandler(this.stop_MouseUp);
            // 
            // down
            // 
            this.down.Location = new System.Drawing.Point(434, 261);
            this.down.Name = "down";
            this.down.Size = new System.Drawing.Size(75, 59);
            this.down.TabIndex = 1;
            this.down.Text = "后";
            this.down.UseVisualStyleBackColor = true;
            this.down.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.down.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.down.MouseDown += new System.Windows.Forms.MouseEventHandler(this.down_MouseDown);
            this.down.MouseUp += new System.Windows.Forms.MouseEventHandler(this.stop_MouseUp);
            // 
            // left
            // 
            this.left.Location = new System.Drawing.Point(325, 178);
            this.left.Name = "left";
            this.left.Size = new System.Drawing.Size(75, 59);
            this.left.TabIndex = 1;
            this.left.Text = "左";
            this.left.UseVisualStyleBackColor = true;
            this.left.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.left.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.left.MouseDown += new System.Windows.Forms.MouseEventHandler(this.left_MouseDown);
            this.left.MouseUp += new System.Windows.Forms.MouseEventHandler(this.stop_MouseUp);
            // 
            // right
            // 
            this.right.Location = new System.Drawing.Point(548, 178);
            this.right.Name = "right";
            this.right.Size = new System.Drawing.Size(75, 59);
            this.right.TabIndex = 1;
            this.right.Text = "右";
            this.right.UseVisualStyleBackColor = true;
            this.right.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.right.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.right.MouseDown += new System.Windows.Forms.MouseEventHandler(this.right_MouseDown);
            this.right.MouseUp += new System.Windows.Forms.MouseEventHandler(this.stop_MouseUp);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(29, 77);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(290, 329);
            this.textBox1.TabIndex = 1;
            this.textBox1.Text = " ";
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.textBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            // 
            // comboBox1
            // 
            this.comboBox1.CausesValidation = false;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "",
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8",
            "COM9",
            "COM10",
            "COM11",
            "COM12",
            "COM13",
            "COM14",
            "COM15",
            "COM16",
            "COM17",
            "COM18",
            "COM19",
            "COM20"});
            this.comboBox1.Location = new System.Drawing.Point(348, 12);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(122, 20);
            this.comboBox1.TabIndex = 13;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // stop
            // 
            this.stop.Location = new System.Drawing.Point(712, 325);
            this.stop.Name = "stop";
            this.stop.Size = new System.Drawing.Size(75, 59);
            this.stop.TabIndex = 0;
            this.stop.Text = "停";
            this.stop.UseVisualStyleBackColor = true;
            this.stop.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.stop.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.stop.MouseDown += new System.Windows.Forms.MouseEventHandler(this.stop_MouseDown);
            this.stop.MouseUp += new System.Windows.Forms.MouseEventHandler(this.stop_MouseUp);
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "微调模式",
            "移动模式"});
            this.comboBox2.Location = new System.Drawing.Point(501, 12);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(122, 20);
            this.comboBox2.TabIndex = 13;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // timer1
            // 
            this.timer1.Interval = 30;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(346, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "停止";
            // 
            // comboBox3
            // 
            this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Items.AddRange(new object[] {
            "10",
            "20",
            "30",
            "40",
            "50",
            "100",
            "150",
            "200",
            "239"});
            this.comboBox3.Location = new System.Drawing.Point(501, 38);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(122, 20);
            this.comboBox3.TabIndex = 13;
            this.comboBox3.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(348, 40);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(42, 23);
            this.button8.TabIndex = 19;
            this.button8.Text = "暂停";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(396, 40);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(42, 23);
            this.button10.TabIndex = 19;
            this.button10.Text = "清空";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // top
            // 
            this.top.Location = new System.Drawing.Point(434, 176);
            this.top.Name = "top";
            this.top.Size = new System.Drawing.Size(40, 41);
            this.top.TabIndex = 21;
            this.top.Text = "上";
            this.top.UseVisualStyleBackColor = true;
            this.top.Click += new System.EventHandler(this.top_Click);
            // 
            // down1
            // 
            this.down1.Location = new System.Drawing.Point(434, 176);
            this.down1.Name = "down1";
            this.down1.Size = new System.Drawing.Size(75, 63);
            this.down1.TabIndex = 22;
            this.down1.Text = "\r\n      下";
            this.down1.UseVisualStyleBackColor = true;
            this.down1.Click += new System.EventHandler(this.down1_Click);
            // 
            // BaseSpeed
            // 
            this.BaseSpeed.Location = new System.Drawing.Point(422, 149);
            this.BaseSpeed.Name = "BaseSpeed";
            this.BaseSpeed.Size = new System.Drawing.Size(100, 21);
            this.BaseSpeed.TabIndex = 23;
            this.BaseSpeed.Text = "0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(868, 409);
            this.Controls.Add(this.BaseSpeed);
            this.Controls.Add(this.top);
            this.Controls.Add(this.down1);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox3);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.right);
            this.Controls.Add(this.left);
            this.Controls.Add(this.stop);
            this.Controls.Add(this.down);
            this.Controls.Add(this.up);
            this.Name = "Form1";
            this.Text = "机器人控制器";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button up;
        private System.Windows.Forms.Button down;
        private System.Windows.Forms.Button left;
        private System.Windows.Forms.Button right;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button stop;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button top;
        private System.Windows.Forms.Button down1;
        private System.Windows.Forms.TextBox BaseSpeed;
    }
}

