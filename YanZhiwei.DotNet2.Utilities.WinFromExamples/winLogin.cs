using System;
using System.Windows.Forms;
using YanZhiwei.DotNet2.Utilities.WinForm;

namespace YanZhiwei.DotNet2.Utilities.WinFromExamples
{
    public partial class winLogin : Form
    {
        public winLogin()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("登陆成功");
            this.CloseLoginForm<winLogin>();
            //this.DialogResult = DialogResult.OK;
            //this.Close();
        }
    }
}