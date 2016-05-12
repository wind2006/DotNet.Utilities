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
    public partial class winProgressBarExample : Form
    {
        public winProgressBarExample()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            progressBar1.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            progressBar1.Stop(50);
        }
    }
}
