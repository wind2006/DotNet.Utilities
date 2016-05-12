using System;
using System.Windows.Forms;
using YanZhiwei.DotNet.DevExpress12._1.Utilities;

namespace YanZhiwei.DotNet.DevExpress12._1.Test
{
    public partial class FormComboBoxEdit : Form
    {
        public FormComboBoxEdit()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            int[] _dataSource = new int[10];
            for (int i = 1; i <= 10; i++)
            {
                _dataSource[i - 1] = i;
            }
            comboBoxEdit1.SetDataSource(_dataSource, "--请选择--");
        }
    }
}