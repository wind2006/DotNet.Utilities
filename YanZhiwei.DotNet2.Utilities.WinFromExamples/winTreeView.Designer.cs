namespace YanZhiwei.DotNet2.Utilities.WinFromExamples
{
    partial class winTreeView
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("节点8");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("节点9");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("节点10");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("节点11");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("节点0", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4});
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("节点5");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("节点6");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("节点7");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("节点1", new System.Windows.Forms.TreeNode[] {
            treeNode6,
            treeNode7,
            treeNode8});
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("节点12");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("节点13");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("节点14");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("节点2", new System.Windows.Forms.TreeNode[] {
            treeNode10,
            treeNode11,
            treeNode12});
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("节点15");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("节点16");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("节点17");
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("节点3", new System.Windows.Forms.TreeNode[] {
            treeNode14,
            treeNode15,
            treeNode16});
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("节点18");
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("节点19");
            System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("节点4", new System.Windows.Forms.TreeNode[] {
            treeNode18,
            treeNode19});
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(12, 12);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "节点8";
            treeNode1.Text = "节点8";
            treeNode2.Name = "节点9";
            treeNode2.Text = "节点9";
            treeNode3.Name = "节点10";
            treeNode3.Text = "节点10";
            treeNode4.Name = "节点11";
            treeNode4.Text = "节点11";
            treeNode5.Name = "节点0";
            treeNode5.Text = "节点0";
            treeNode6.Name = "节点5";
            treeNode6.Text = "节点5";
            treeNode7.Name = "节点6";
            treeNode7.Text = "节点6";
            treeNode8.Name = "节点7";
            treeNode8.Text = "节点7";
            treeNode9.Name = "节点1";
            treeNode9.Text = "节点1";
            treeNode10.Name = "节点12";
            treeNode10.Text = "节点12";
            treeNode11.Name = "节点13";
            treeNode11.Text = "节点13";
            treeNode12.Name = "节点14";
            treeNode12.Text = "节点14";
            treeNode13.Name = "节点2";
            treeNode13.Text = "节点2";
            treeNode14.Name = "节点15";
            treeNode14.Text = "节点15";
            treeNode15.Name = "节点16";
            treeNode15.Text = "节点16";
            treeNode16.Name = "节点17";
            treeNode16.Text = "节点17";
            treeNode17.Name = "节点3";
            treeNode17.Text = "节点3";
            treeNode18.Name = "节点18";
            treeNode18.Text = "节点18";
            treeNode19.Name = "节点19";
            treeNode19.Text = "节点19";
            treeNode20.Name = "节点4";
            treeNode20.Text = "节点4";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode9,
            treeNode13,
            treeNode17,
            treeNode20});
            this.treeView1.Size = new System.Drawing.Size(293, 357);
            this.treeView1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(312, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(210, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "ApplyNodeHighlight";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(312, 43);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(210, 21);
            this.textBox1.TabIndex = 2;
            // 
            // winTreeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 420);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.treeView1);
            this.Name = "winTreeView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "winTreeView";
            this.Load += new System.EventHandler(this.winTreeView_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
    }
}