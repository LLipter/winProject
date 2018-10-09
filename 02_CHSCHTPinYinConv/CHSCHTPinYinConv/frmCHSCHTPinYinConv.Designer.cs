namespace CHSCHTPinYinConv
{
    partial class frmCHSCHTPinYinConv
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lblCHS = new System.Windows.Forms.Label();
            this.lblCHT = new System.Windows.Forms.Label();
            this.txtCHS = new System.Windows.Forms.TextBox();
            this.txtCHT = new System.Windows.Forms.TextBox();
            this.btnToCHT = new System.Windows.Forms.Button();
            this.btnToCHS = new System.Windows.Forms.Button();
            this.btnPinyin = new System.Windows.Forms.Button();
            this.lblPinyin = new System.Windows.Forms.Label();
            this.lblPY = new System.Windows.Forms.Label();
            this.btnVoice = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblCHS
            // 
            this.lblCHS.AutoSize = true;
            this.lblCHS.Location = new System.Drawing.Point(43, 60);
            this.lblCHS.Name = "lblCHS";
            this.lblCHS.Size = new System.Drawing.Size(31, 15);
            this.lblCHS.TabIndex = 0;
            this.lblCHS.Text = "CHS";
            // 
            // lblCHT
            // 
            this.lblCHT.AutoSize = true;
            this.lblCHT.Location = new System.Drawing.Point(43, 100);
            this.lblCHT.Name = "lblCHT";
            this.lblCHT.Size = new System.Drawing.Size(31, 15);
            this.lblCHT.TabIndex = 1;
            this.lblCHT.Text = "CHT";
            // 
            // txtCHS
            // 
            this.txtCHS.Location = new System.Drawing.Point(117, 57);
            this.txtCHS.Name = "txtCHS";
            this.txtCHS.Size = new System.Drawing.Size(100, 25);
            this.txtCHS.TabIndex = 2;
            // 
            // txtCHT
            // 
            this.txtCHT.Location = new System.Drawing.Point(117, 100);
            this.txtCHT.Name = "txtCHT";
            this.txtCHT.Size = new System.Drawing.Size(100, 25);
            this.txtCHT.TabIndex = 3;
            // 
            // btnToCHT
            // 
            this.btnToCHT.Location = new System.Drawing.Point(278, 57);
            this.btnToCHT.Name = "btnToCHT";
            this.btnToCHT.Size = new System.Drawing.Size(105, 23);
            this.btnToCHT.TabIndex = 4;
            this.btnToCHT.Text = "To CHT";
            this.btnToCHT.UseVisualStyleBackColor = true;
            this.btnToCHT.Click += new System.EventHandler(this.btnToCHT_Click);
            // 
            // btnToCHS
            // 
            this.btnToCHS.Location = new System.Drawing.Point(278, 102);
            this.btnToCHS.Name = "btnToCHS";
            this.btnToCHS.Size = new System.Drawing.Size(105, 23);
            this.btnToCHS.TabIndex = 5;
            this.btnToCHS.Text = "To CHS";
            this.btnToCHS.UseVisualStyleBackColor = true;
            this.btnToCHS.Click += new System.EventHandler(this.btnToCHS_Click);
            // 
            // btnPinyin
            // 
            this.btnPinyin.Location = new System.Drawing.Point(278, 147);
            this.btnPinyin.Name = "btnPinyin";
            this.btnPinyin.Size = new System.Drawing.Size(105, 23);
            this.btnPinyin.TabIndex = 6;
            this.btnPinyin.Text = "Get PinYin";
            this.btnPinyin.UseVisualStyleBackColor = true;
            this.btnPinyin.Click += new System.EventHandler(this.btnPinyin_Click);
            // 
            // lblPinyin
            // 
            this.lblPinyin.AutoSize = true;
            this.lblPinyin.Location = new System.Drawing.Point(114, 151);
            this.lblPinyin.Name = "lblPinyin";
            this.lblPinyin.Size = new System.Drawing.Size(0, 15);
            this.lblPinyin.TabIndex = 7;
            // 
            // lblPY
            // 
            this.lblPY.AutoSize = true;
            this.lblPY.Location = new System.Drawing.Point(43, 147);
            this.lblPY.Name = "lblPY";
            this.lblPY.Size = new System.Drawing.Size(55, 15);
            this.lblPY.TabIndex = 8;
            this.lblPY.Text = "PinYin";
            // 
            // btnVoice
            // 
            this.btnVoice.Location = new System.Drawing.Point(278, 188);
            this.btnVoice.Name = "btnVoice";
            this.btnVoice.Size = new System.Drawing.Size(105, 23);
            this.btnVoice.TabIndex = 9;
            this.btnVoice.Text = "Get Voice";
            this.btnVoice.UseVisualStyleBackColor = true;
            this.btnVoice.Click += new System.EventHandler(this.btnVoice_Click);
            // 
            // frmCHSCHTPinYinConv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(425, 335);
            this.Controls.Add(this.btnVoice);
            this.Controls.Add(this.lblPY);
            this.Controls.Add(this.lblPinyin);
            this.Controls.Add(this.btnPinyin);
            this.Controls.Add(this.btnToCHS);
            this.Controls.Add(this.btnToCHT);
            this.Controls.Add(this.txtCHT);
            this.Controls.Add(this.txtCHS);
            this.Controls.Add(this.lblCHT);
            this.Controls.Add(this.lblCHS);
            this.Name = "frmCHSCHTPinYinConv";
            this.Text = "CHSCHTPinYinConv";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCHS;
        private System.Windows.Forms.Label lblCHT;
        private System.Windows.Forms.TextBox txtCHS;
        private System.Windows.Forms.TextBox txtCHT;
        private System.Windows.Forms.Button btnToCHT;
        private System.Windows.Forms.Button btnToCHS;
        private System.Windows.Forms.Button btnPinyin;
        private System.Windows.Forms.Label lblPinyin;
        private System.Windows.Forms.Label lblPY;
        private System.Windows.Forms.Button btnVoice;
    }
}

