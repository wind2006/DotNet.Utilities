using System;
using System.Windows.Forms;

namespace YanZhiwei.DotNet.DevExpress12._1.Test
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            FormBaseEdit _baseEdit = new FormBaseEdit();
            _baseEdit.ShowDialog();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            FormComboBoxEdit _comboBox = new FormComboBoxEdit();
            _comboBox.ShowDialog();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            FormGrid _grid = new FormGrid();
            _grid.ShowDialog();
        }
    }
}
