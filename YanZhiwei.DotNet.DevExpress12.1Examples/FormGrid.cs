using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using YanZhiwei.DotNet.DevExpress12._1.Test.Model;
using YanZhiwei.DotNet.DevExpress12._1.Utilities;
using YanZhiwei.DotNet.DevExpress12._1.Utilities.Models;
using YanZhiwei.DotNet2.Utilities.Common;

namespace YanZhiwei.DotNet.DevExpress12._1.Test
{
    public partial class FormGrid : Form
    {
        public FormGrid()
        {
            InitializeComponent();
        }

        private void FormGrid_Load(object sender, EventArgs e)
        {
        }

        public List<Person> PersonList
        {
            get
            {
                List<Person> _personList = new List<Person>();
                for (short i = 0; i < 10; i++)
                {
                    _personList.Add(new Person()
                    {
                        Name = RandomHelper.NetxtString(8, false),
                        Age = (short)RandomHelper.NextNumber(1, 100),
                        BrithDate = RandomHelper.NextTime()
                    });
                }
                return _personList;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            gridView1.SetDataSource(PersonList, checkEdit1.Checked);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            gridView1.ClearDataSource(checkEdit1.Checked);
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            gridView1.ClearAllRows();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            gridControl1.CustomPrint(new PrintItem()
            {
                FooterColor = Color.Red,
                FooterText = string.Format("打印日期{0}", DateTime.Now.FormatDate(4)),
                PrintFooter = true,
                HeaderText = string.Format("打印测试{0}", DateTime.Now.FormatDate(2)),
                PrintHeader = true
            }, true);
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            gridView1.ConditionRepositoryItemEdit<Person, string>("提示", "年龄能被二整除，所以不可编辑!", toolTipController1, c => c.Remark, c => c.Age % 2 == 0);
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            gridView1.ConditionRepositoryItemValidate<Person, string>("请输入偶数!", c => c.Remark, d => !string.IsNullOrEmpty(d.ToString()));
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            gridView1.DrawHeaderCheckBox<Person, bool>(ckItemAdult, c => c.Adult);
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            gridView1.DrawNoRowCountMessage("暂无筛选数据。");
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            gridView1.DrawSequenceNumber();
        }

        private void simpleButton10_Click(object sender, EventArgs e)
        {
            gridView1.SetSummaryItem<Person, short>(DevExpress.Data.SummaryItemType.Sum, "合计={0:n2}", c => c.Age);
        }

        private void simpleButton11_Click(object sender, EventArgs e)
        {
            gridView1.SetRepositoryItemButtonEdit(btnItemEdit, "编辑");
        }

        private void btnItemEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            MessageBox.Show("ButtonClick");
        }

        private void simpleButton12_Click(object sender, EventArgs e)
        {
            gridControl1.ToXls("ceshi.xls");
        }
    }
}