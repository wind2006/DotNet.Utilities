using System;
using System.Windows.Forms;
using YanZhiwei.DotNet.DevExpress12._1.Utilities;

namespace YanZhiwei.DotNet.DevExpress12._1.Test
{
    public partial class FormBaseEdit : Form
    {
        public FormBaseEdit()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            textEdit1.PromptTimelyMessage(toolTipController1, "您好，PromptTimelyMessage.");
        }
    }
}