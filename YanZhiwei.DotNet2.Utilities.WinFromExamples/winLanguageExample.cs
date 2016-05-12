using System;
using System.Windows.Forms;
using YanZhiwei.DotNet2.Utilities.WinForm;

namespace YanZhiwei.DotNet2.Utilities.WinFromExamples
{
    public partial class winLanguageExample : Form
    {
        public winLanguageExample()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() == "中文")
            {
                this.SetGlobalization("zh-Cn");
            }
            else if (comboBox1.SelectedItem.ToString() == "英文")
            {
                this.SetGlobalization("en-Us");
            }
        }
    }
}