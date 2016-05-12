namespace YanZhiwei.DotNet.Toolbox
{
    partial class FormMain
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
            this.menuHeader = new System.Windows.Forms.MenuStrip();
            this.MenuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.gpOutput = new System.Windows.Forms.GroupBox();
            this.txtOutput = new System.Windows.Forms.RichTextBox();
            this.tbcMain = new System.Windows.Forms.TabControl();
            this.tpBuilderDate = new System.Windows.Forms.TabPage();
            this.tpMac = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnGetFilePath = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnGetBuilderDateByFile = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFolderpath = new System.Windows.Forms.TextBox();
            this.btnGetFolderPath = new System.Windows.Forms.Button();
            this.btnGetBuilderDateByFolder = new System.Windows.Forms.Button();
            this.btnGetMac = new System.Windows.Forms.Button();
            this.menuHeader.SuspendLayout();
            this.gpOutput.SuspendLayout();
            this.tbcMain.SuspendLayout();
            this.tpBuilderDate.SuspendLayout();
            this.tpMac.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuHeader
            // 
            this.menuHeader.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemAbout});
            this.menuHeader.Location = new System.Drawing.Point(0, 0);
            this.menuHeader.Name = "menuHeader";
            this.menuHeader.Size = new System.Drawing.Size(734, 25);
            this.menuHeader.TabIndex = 0;
            this.menuHeader.Text = "menuHeader";
            // 
            // MenuItemAbout
            // 
            this.MenuItemAbout.Name = "MenuItemAbout";
            this.MenuItemAbout.Size = new System.Drawing.Size(68, 21);
            this.MenuItemAbout.Text = "关于软件";
            // 
            // gpOutput
            // 
            this.gpOutput.Controls.Add(this.txtOutput);
            this.gpOutput.Location = new System.Drawing.Point(0, 217);
            this.gpOutput.Name = "gpOutput";
            this.gpOutput.Size = new System.Drawing.Size(734, 192);
            this.gpOutput.TabIndex = 1;
            this.gpOutput.TabStop = false;
            this.gpOutput.Text = "输出";
            // 
            // txtOutput
            // 
            this.txtOutput.BackColor = System.Drawing.SystemColors.MenuText;
            this.txtOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOutput.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtOutput.ForeColor = System.Drawing.Color.Green;
            this.txtOutput.Location = new System.Drawing.Point(3, 17);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.Size = new System.Drawing.Size(728, 172);
            this.txtOutput.TabIndex = 0;
            this.txtOutput.Text = "";
            // 
            // tbcMain
            // 
            this.tbcMain.Controls.Add(this.tpBuilderDate);
            this.tbcMain.Controls.Add(this.tpMac);
            this.tbcMain.Location = new System.Drawing.Point(3, 29);
            this.tbcMain.Name = "tbcMain";
            this.tbcMain.SelectedIndex = 0;
            this.tbcMain.Size = new System.Drawing.Size(731, 182);
            this.tbcMain.TabIndex = 2;
            // 
            // tpBuilderDate
            // 
            this.tpBuilderDate.Controls.Add(this.btnGetBuilderDateByFolder);
            this.tpBuilderDate.Controls.Add(this.btnGetFolderPath);
            this.tpBuilderDate.Controls.Add(this.txtFolderpath);
            this.tpBuilderDate.Controls.Add(this.label2);
            this.tpBuilderDate.Controls.Add(this.btnGetBuilderDateByFile);
            this.tpBuilderDate.Controls.Add(this.btnGetFilePath);
            this.tpBuilderDate.Controls.Add(this.txtFilePath);
            this.tpBuilderDate.Controls.Add(this.label1);
            this.tpBuilderDate.Location = new System.Drawing.Point(4, 22);
            this.tpBuilderDate.Name = "tpBuilderDate";
            this.tpBuilderDate.Padding = new System.Windows.Forms.Padding(3);
            this.tpBuilderDate.Size = new System.Drawing.Size(723, 156);
            this.tpBuilderDate.TabIndex = 0;
            this.tpBuilderDate.Text = "获取DLL，EXE编译日期";
            this.tpBuilderDate.UseVisualStyleBackColor = true;
            // 
            // tpMac
            // 
            this.tpMac.Controls.Add(this.btnGetMac);
            this.tpMac.Location = new System.Drawing.Point(4, 22);
            this.tpMac.Name = "tpMac";
            this.tpMac.Padding = new System.Windows.Forms.Padding(3);
            this.tpMac.Size = new System.Drawing.Size(723, 156);
            this.tpMac.TabIndex = 1;
            this.tpMac.Text = "获取MAC地址";
            this.tpMac.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(67, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "EXE，DLL文件路径:";
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(180, 44);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.ReadOnly = true;
            this.txtFilePath.Size = new System.Drawing.Size(326, 21);
            this.txtFilePath.TabIndex = 1;
            // 
            // btnGetFilePath
            // 
            this.btnGetFilePath.Location = new System.Drawing.Point(527, 47);
            this.btnGetFilePath.Name = "btnGetFilePath";
            this.btnGetFilePath.Size = new System.Drawing.Size(75, 23);
            this.btnGetFilePath.TabIndex = 2;
            this.btnGetFilePath.Text = "选择";
            this.btnGetFilePath.UseVisualStyleBackColor = true;
            this.btnGetFilePath.Click += new System.EventHandler(this.btnGetFilePath_Click);
            // 
            // btnGetBuilderDateByFile
            // 
            this.btnGetBuilderDateByFile.Location = new System.Drawing.Point(609, 46);
            this.btnGetBuilderDateByFile.Name = "btnGetBuilderDateByFile";
            this.btnGetBuilderDateByFile.Size = new System.Drawing.Size(75, 23);
            this.btnGetBuilderDateByFile.TabIndex = 3;
            this.btnGetBuilderDateByFile.Text = "获取";
            this.btnGetBuilderDateByFile.UseVisualStyleBackColor = true;
            this.btnGetBuilderDateByFile.Click += new System.EventHandler(this.btnGetBuilderDateByFile_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(55, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "EXE，DLL文件夹路径:";
            // 
            // txtFolderpath
            // 
            this.txtFolderpath.Location = new System.Drawing.Point(180, 85);
            this.txtFolderpath.Name = "txtFolderpath";
            this.txtFolderpath.ReadOnly = true;
            this.txtFolderpath.Size = new System.Drawing.Size(326, 21);
            this.txtFolderpath.TabIndex = 5;
            // 
            // btnGetFolderPath
            // 
            this.btnGetFolderPath.Location = new System.Drawing.Point(527, 88);
            this.btnGetFolderPath.Name = "btnGetFolderPath";
            this.btnGetFolderPath.Size = new System.Drawing.Size(75, 23);
            this.btnGetFolderPath.TabIndex = 6;
            this.btnGetFolderPath.Text = "选择";
            this.btnGetFolderPath.UseVisualStyleBackColor = true;
            this.btnGetFolderPath.Click += new System.EventHandler(this.btnGetFolderPath_Click);
            // 
            // btnGetBuilderDateByFolder
            // 
            this.btnGetBuilderDateByFolder.Location = new System.Drawing.Point(609, 88);
            this.btnGetBuilderDateByFolder.Name = "btnGetBuilderDateByFolder";
            this.btnGetBuilderDateByFolder.Size = new System.Drawing.Size(75, 23);
            this.btnGetBuilderDateByFolder.TabIndex = 7;
            this.btnGetBuilderDateByFolder.Text = "获取";
            this.btnGetBuilderDateByFolder.UseVisualStyleBackColor = true;
            this.btnGetBuilderDateByFolder.Click += new System.EventHandler(this.btnGetBuilderDateByFolder_Click);
            // 
            // btnGetMac
            // 
            this.btnGetMac.Location = new System.Drawing.Point(612, 61);
            this.btnGetMac.Name = "btnGetMac";
            this.btnGetMac.Size = new System.Drawing.Size(75, 23);
            this.btnGetMac.TabIndex = 0;
            this.btnGetMac.Text = "获取";
            this.btnGetMac.UseVisualStyleBackColor = true;
            this.btnGetMac.Click += new System.EventHandler(this.btnGetMac_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 412);
            this.Controls.Add(this.tbcMain);
            this.Controls.Add(this.gpOutput);
            this.Controls.Add(this.menuHeader);
            this.MainMenuStrip = this.menuHeader;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(750, 450);
            this.MinimumSize = new System.Drawing.Size(750, 450);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Toolbox";
            this.menuHeader.ResumeLayout(false);
            this.menuHeader.PerformLayout();
            this.gpOutput.ResumeLayout(false);
            this.tbcMain.ResumeLayout(false);
            this.tpBuilderDate.ResumeLayout(false);
            this.tpBuilderDate.PerformLayout();
            this.tpMac.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuHeader;
        private System.Windows.Forms.ToolStripMenuItem MenuItemAbout;
        private System.Windows.Forms.GroupBox gpOutput;
        private System.Windows.Forms.RichTextBox txtOutput;
        private System.Windows.Forms.TabControl tbcMain;
        private System.Windows.Forms.TabPage tpBuilderDate;
        private System.Windows.Forms.TabPage tpMac;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGetFilePath;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button btnGetBuilderDateByFile;
        private System.Windows.Forms.TextBox txtFolderpath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnGetFolderPath;
        private System.Windows.Forms.Button btnGetBuilderDateByFolder;
        private System.Windows.Forms.Button btnGetMac;
    }
}

