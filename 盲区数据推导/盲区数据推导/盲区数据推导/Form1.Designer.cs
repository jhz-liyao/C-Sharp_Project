namespace 盲区数据推导
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
            this.label1 = new System.Windows.Forms.Label();
            this.tb_step = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_zhouqi = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_pinlv = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_sam = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "步长";
            // 
            // tb_step
            // 
            this.tb_step.Location = new System.Drawing.Point(83, 22);
            this.tb_step.Name = "tb_step";
            this.tb_step.Size = new System.Drawing.Size(38, 21);
            this.tb_step.TabIndex = 1;
            this.tb_step.Text = "6.5";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(197, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "周期时间";
            // 
            // tb_zhouqi
            // 
            this.tb_zhouqi.Location = new System.Drawing.Point(256, 22);
            this.tb_zhouqi.Name = "tb_zhouqi";
            this.tb_zhouqi.Size = new System.Drawing.Size(31, 21);
            this.tb_zhouqi.TabIndex = 1;
            this.tb_zhouqi.Text = "25";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(383, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "频率";
            // 
            // tb_pinlv
            // 
            this.tb_pinlv.Location = new System.Drawing.Point(419, 22);
            this.tb_pinlv.Name = "tb_pinlv";
            this.tb_pinlv.Size = new System.Drawing.Size(46, 21);
            this.tb_pinlv.TabIndex = 4;
            this.tb_pinlv.Text = "40000";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(689, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "计算";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(38, 65);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(713, 491);
            this.textBox4.TabIndex = 6;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(38, 562);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(713, 200);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(525, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "采样点数";
            // 
            // tb_sam
            // 
            this.tb_sam.Location = new System.Drawing.Point(584, 22);
            this.tb_sam.Name = "tb_sam";
            this.tb_sam.Size = new System.Drawing.Size(38, 21);
            this.tb_sam.TabIndex = 8;
            this.tb_sam.Text = "38";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(810, 765);
            this.Controls.Add(this.tb_sam);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tb_pinlv);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tb_zhouqi);
            this.Controls.Add(this.tb_step);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "盲区数据推导";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_step;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_zhouqi;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_pinlv;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_sam;
    }
}

