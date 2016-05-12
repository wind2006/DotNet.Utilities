using System;
using System.Collections.Generic;
using System.Windows.Forms;
using YanZhiwei.DotNet2.Utilities.Models;
using YanZhiwei.DotNet2.Utilities.WinForm;

namespace YanZhiwei.DotNet2.Utilities.WinFromExamples
{
    public partial class winCheckedListBox : Form
    {
        public winCheckedListBox()
        {
            InitializeComponent();
        }

        private List<ListItem> ERCList = null;

        private void winCheckedListBox_Load(object sender, EventArgs e)
        {
            ERCList = new List<ListItem>();
            ERCList.Add(new ListItem() { Key = "ERC1", Value = "ERC1： RTU失电记录" });
            ERCList.Add(new ListItem() { Key = "ERC2", Value = "ERC2：正常开灯成功记录" });
            ERCList.Add(new ListItem() { Key = "ERC3", Value = "ERC3：正常开灯失败记录" });
            ERCList.Add(new ListItem() { Key = "ERC4", Value = "ERC4：正常关灯成功记录" });
            ERCList.Add(new ListItem() { Key = "ERC5", Value = "ERC5：正常关灯失败记录" });
            ERCList.Add(new ListItem() { Key = "ERC6", Value = "ERC6：异常开灯记录" });
            ERCList.Add(new ListItem() { Key = "ERC7", Value = "ERC7：异常关灯记录" });
            ERCList.Add(new ListItem() { Key = "ERC8", Value = "ERC8：模拟量越上限(关灯时间内)记录" });
            ERCList.Add(new ListItem() { Key = "ERC9", Value = "ERC9：模拟量越上限(开灯时间内)记录" });
            ERCList.Add(new ListItem() { Key = "ERC10", Value = "ERC10：模拟量越下限(开灯时间内)记录" });
            ERCList.Add(new ListItem() { Key = "ERC11", Value = "ERC11：终端通信故障记录" });
            ERCList.Add(new ListItem() { Key = "ERC12", Value = "ERC12：集中器路由板故障记录" });
            ERCList.Add(new ListItem() { Key = "ERC13", Value = "ERC13：节能接触器档位正常切换记录" });
            ERCList.Add(new ListItem() { Key = "ERC14", Value = "ERC14：节能接触器档位切换失败记录" });
            ERCList.Add(new ListItem() { Key = "ERC15", Value = "ERC15：单灯正常开灯记录" });
            ERCList.Add(new ListItem() { Key = "ERC16", Value = "ERC16：单灯正常关灯记录" });
            ERCList.Add(new ListItem() { Key = "ERC17", Value = "ERC17：单灯异常开灯记录" });
            ERCList.Add(new ListItem() { Key = "ERC18", Value = "ERC18：单灯异常关灯记录" });
            ERCList.Add(new ListItem() { Key = "ERC19", Value = "ERC19：单灯电流过大记录" });
            ERCList.Add(new ListItem() { Key = "ERC20", Value = "ERC20：单灯电流过小记录" });
            ERCList.Add(new ListItem() { Key = "ERC21", Value = "ERC21：单灯电容故障记录" });
            ERCList.Add(new ListItem() { Key = "ERC22", Value = "ERC22：单灯灯具故障记录" });
            ERCList.Add(new ListItem() { Key = "ERC23", Value = "ERC23：单灯熔丝故障记录" });
            ERCList.Add(new ListItem() { Key = "ERC24", Value = "ERC24：单灯通信故障记录" });
            ERCList.Add(new ListItem() { Key = "ERC25", Value = "ERC25： 设施物理状态报警记录" });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            checkedListBox1.SetDataSource(ERCList, "Key", "Value");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(checkedListBox1.GetCheckedItemList<ListItem>().Count.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            checkedListBox1.SetAllItemState(true);
        }
    }
}