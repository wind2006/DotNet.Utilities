using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YanZhiwei.DotNet2.Utilities.WinForm;
namespace YanZhiwei.DotNet2.Utilities.WinFromExamples
{
    public partial class winTreeView : Form
    {
        public winTreeView()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            treeView1.ApplyNodeHighLight(Brushes.Blue);
        }

        private void winTreeView_Load(object sender, EventArgs e)
        {
            //treeView1.ApplyNodeHighLight(Brushes.Blue);
        }
    }
}
