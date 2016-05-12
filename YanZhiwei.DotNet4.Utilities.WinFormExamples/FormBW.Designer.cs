namespace YanZhiwei.DotNet4.Utilities.WinForm.Examples
{
    partial class FormBW
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.checkBoxUseParallel = new System.Windows.Forms.CheckBox();
            this.progressBarBW = new System.Windows.Forms.ProgressBar();
            this.labelTimeLeft = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonStart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // checkBoxUseParallel
            // 
            this.checkBoxUseParallel.AutoSize = true;
            this.checkBoxUseParallel.Location = new System.Drawing.Point(192, 43);
            this.checkBoxUseParallel.Name = "checkBoxUseParallel";
            this.checkBoxUseParallel.Size = new System.Drawing.Size(96, 16);
            this.checkBoxUseParallel.TabIndex = 9;
            this.checkBoxUseParallel.Text = "Use parallel";
            this.checkBoxUseParallel.UseVisualStyleBackColor = true;
            // 
            // progressBarBW
            // 
            this.progressBarBW.Location = new System.Drawing.Point(12, 12);
            this.progressBarBW.Name = "progressBarBW";
            this.progressBarBW.Size = new System.Drawing.Size(251, 21);
            this.progressBarBW.TabIndex = 8;
            // 
            // labelTimeLeft
            // 
            this.labelTimeLeft.AutoSize = true;
            this.labelTimeLeft.Location = new System.Drawing.Point(12, 63);
            this.labelTimeLeft.Name = "labelTimeLeft";
            this.labelTimeLeft.Size = new System.Drawing.Size(59, 12);
            this.labelTimeLeft.TabIndex = 7;
            this.labelTimeLeft.Text = "Time left";
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(93, 39);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 21);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(12, 39);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 21);
            this.buttonStart.TabIndex = 5;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click_1);
            // 
            // FormBW
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(297, 78);
            this.Controls.Add(this.checkBoxUseParallel);
            this.Controls.Add(this.progressBarBW);
            this.Controls.Add(this.labelTimeLeft);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonStart);
            this.Name = "FormBW";
            this.Text = "FormBWExpamle";
            this.Load += new System.EventHandler(this.FormBW_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.CheckBox checkBoxUseParallel;
        private System.Windows.Forms.ProgressBar progressBarBW;
        private System.Windows.Forms.Label labelTimeLeft;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonStart;
    }
}