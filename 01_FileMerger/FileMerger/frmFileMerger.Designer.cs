namespace FileMerger
{
    partial class frmFileMerger
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
            this.btnChooseDir = new System.Windows.Forms.Button();
            this.lblFileNamePtn = new System.Windows.Forms.Label();
            this.txtFileNamePtn = new System.Windows.Forms.TextBox();
            this.lblCurrentDir = new System.Windows.Forms.Label();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lsbfiles = new System.Windows.Forms.ListBox();
            this.lsbSelectedFiles = new System.Windows.Forms.ListBox();
            this.btnAddToSet = new System.Windows.Forms.Button();
            this.btnClearSet = new System.Windows.Forms.Button();
            this.btnRemoveFromSet = new System.Windows.Forms.Button();
            this.btnAddAdditionalFile = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnMoveUpward = new System.Windows.Forms.Button();
            this.btnMoveDownward = new System.Windows.Forms.Button();
            this.btnMerge = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnChooseDir
            // 
            this.btnChooseDir.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnChooseDir.Location = new System.Drawing.Point(35, 25);
            this.btnChooseDir.Name = "btnChooseDir";
            this.btnChooseDir.Size = new System.Drawing.Size(154, 23);
            this.btnChooseDir.TabIndex = 0;
            this.btnChooseDir.Text = "Select Directory";
            this.btnChooseDir.UseVisualStyleBackColor = true;
            this.btnChooseDir.Click += new System.EventHandler(this.btnChooseDir_Click);
            // 
            // lblFileNamePtn
            // 
            this.lblFileNamePtn.AutoSize = true;
            this.lblFileNamePtn.Location = new System.Drawing.Point(32, 77);
            this.lblFileNamePtn.Name = "lblFileNamePtn";
            this.lblFileNamePtn.Size = new System.Drawing.Size(143, 15);
            this.lblFileNamePtn.TabIndex = 1;
            this.lblFileNamePtn.Text = "File Name Pattern";
            // 
            // txtFileNamePtn
            // 
            this.txtFileNamePtn.Location = new System.Drawing.Point(236, 67);
            this.txtFileNamePtn.Name = "txtFileNamePtn";
            this.txtFileNamePtn.Size = new System.Drawing.Size(148, 25);
            this.txtFileNamePtn.TabIndex = 2;
            // 
            // lblCurrentDir
            // 
            this.lblCurrentDir.AutoSize = true;
            this.lblCurrentDir.Location = new System.Drawing.Point(242, 29);
            this.lblCurrentDir.Name = "lblCurrentDir";
            this.lblCurrentDir.Size = new System.Drawing.Size(0, 15);
            this.lblCurrentDir.TabIndex = 3;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(438, 70);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lsbfiles
            // 
            this.lsbfiles.FormattingEnabled = true;
            this.lsbfiles.ItemHeight = 15;
            this.lsbfiles.Location = new System.Drawing.Point(35, 117);
            this.lsbfiles.Name = "lsbfiles";
            this.lsbfiles.Size = new System.Drawing.Size(583, 124);
            this.lsbfiles.TabIndex = 5;
            // 
            // lsbSelectedFiles
            // 
            this.lsbSelectedFiles.FormattingEnabled = true;
            this.lsbSelectedFiles.ItemHeight = 15;
            this.lsbSelectedFiles.Location = new System.Drawing.Point(35, 301);
            this.lsbSelectedFiles.Name = "lsbSelectedFiles";
            this.lsbSelectedFiles.Size = new System.Drawing.Size(583, 124);
            this.lsbSelectedFiles.TabIndex = 6;
            // 
            // btnAddToSet
            // 
            this.btnAddToSet.Location = new System.Drawing.Point(35, 263);
            this.btnAddToSet.Name = "btnAddToSet";
            this.btnAddToSet.Size = new System.Drawing.Size(102, 23);
            this.btnAddToSet.TabIndex = 7;
            this.btnAddToSet.Text = "Add to Set";
            this.btnAddToSet.UseVisualStyleBackColor = true;
            this.btnAddToSet.Click += new System.EventHandler(this.btnAddtoSet_Click);
            // 
            // btnClearSet
            // 
            this.btnClearSet.Location = new System.Drawing.Point(157, 263);
            this.btnClearSet.Name = "btnClearSet";
            this.btnClearSet.Size = new System.Drawing.Size(102, 23);
            this.btnClearSet.TabIndex = 8;
            this.btnClearSet.Text = "Clear Set";
            this.btnClearSet.UseVisualStyleBackColor = true;
            this.btnClearSet.Click += new System.EventHandler(this.btnClearSet_Click);
            // 
            // btnRemoveFromSet
            // 
            this.btnRemoveFromSet.Location = new System.Drawing.Point(274, 263);
            this.btnRemoveFromSet.Name = "btnRemoveFromSet";
            this.btnRemoveFromSet.Size = new System.Drawing.Size(149, 23);
            this.btnRemoveFromSet.TabIndex = 9;
            this.btnRemoveFromSet.Text = "Remove from Set";
            this.btnRemoveFromSet.UseVisualStyleBackColor = true;
            this.btnRemoveFromSet.Click += new System.EventHandler(this.btnRemoveFromSet_Click);
            // 
            // btnAddAdditionalFile
            // 
            this.btnAddAdditionalFile.Location = new System.Drawing.Point(438, 263);
            this.btnAddAdditionalFile.Name = "btnAddAdditionalFile";
            this.btnAddAdditionalFile.Size = new System.Drawing.Size(180, 23);
            this.btnAddAdditionalFile.TabIndex = 10;
            this.btnAddAdditionalFile.Text = "Add Additional File";
            this.btnAddAdditionalFile.UseVisualStyleBackColor = true;
            this.btnAddAdditionalFile.Click += new System.EventHandler(this.btnAddSingleFile_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // btnMoveUpward
            // 
            this.btnMoveUpward.Location = new System.Drawing.Point(624, 310);
            this.btnMoveUpward.Name = "btnMoveUpward";
            this.btnMoveUpward.Size = new System.Drawing.Size(127, 23);
            this.btnMoveUpward.TabIndex = 11;
            this.btnMoveUpward.Text = "Move Upward";
            this.btnMoveUpward.UseVisualStyleBackColor = true;
            this.btnMoveUpward.Click += new System.EventHandler(this.btnMoveUpward_Click);
            // 
            // btnMoveDownward
            // 
            this.btnMoveDownward.Location = new System.Drawing.Point(624, 348);
            this.btnMoveDownward.Name = "btnMoveDownward";
            this.btnMoveDownward.Size = new System.Drawing.Size(127, 23);
            this.btnMoveDownward.TabIndex = 12;
            this.btnMoveDownward.Text = "Move Downward";
            this.btnMoveDownward.UseVisualStyleBackColor = true;
            this.btnMoveDownward.Click += new System.EventHandler(this.btnMoveDownward_Click);
            // 
            // btnMerge
            // 
            this.btnMerge.Location = new System.Drawing.Point(35, 443);
            this.btnMerge.Name = "btnMerge";
            this.btnMerge.Size = new System.Drawing.Size(102, 23);
            this.btnMerge.TabIndex = 13;
            this.btnMerge.Text = "Merge File";
            this.btnMerge.UseVisualStyleBackColor = true;
            this.btnMerge.Click += new System.EventHandler(this.btnMerge_Click);
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(624, 117);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(127, 23);
            this.btnOpenFile.TabIndex = 15;
            this.btnOpenFile.Text = "Open File";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // frmFileMerger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(763, 502);
            this.Controls.Add(this.btnOpenFile);
            this.Controls.Add(this.btnMerge);
            this.Controls.Add(this.btnMoveDownward);
            this.Controls.Add(this.btnMoveUpward);
            this.Controls.Add(this.btnAddAdditionalFile);
            this.Controls.Add(this.btnRemoveFromSet);
            this.Controls.Add(this.btnClearSet);
            this.Controls.Add(this.btnAddToSet);
            this.Controls.Add(this.lsbSelectedFiles);
            this.Controls.Add(this.lsbfiles);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.lblCurrentDir);
            this.Controls.Add(this.txtFileNamePtn);
            this.Controls.Add(this.lblFileNamePtn);
            this.Controls.Add(this.btnChooseDir);
            this.Name = "frmFileMerger";
            this.Text = "FileMerger";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnChooseDir;
        private System.Windows.Forms.Label lblFileNamePtn;
        private System.Windows.Forms.TextBox txtFileNamePtn;
        private System.Windows.Forms.Label lblCurrentDir;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ListBox lsbfiles;
        private System.Windows.Forms.ListBox lsbSelectedFiles;
        private System.Windows.Forms.Button btnAddToSet;
        private System.Windows.Forms.Button btnClearSet;
        private System.Windows.Forms.Button btnRemoveFromSet;
        private System.Windows.Forms.Button btnAddAdditionalFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button btnMoveUpward;
        private System.Windows.Forms.Button btnMoveDownward;
        private System.Windows.Forms.Button btnMerge;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Button btnOpenFile;
    }
}

