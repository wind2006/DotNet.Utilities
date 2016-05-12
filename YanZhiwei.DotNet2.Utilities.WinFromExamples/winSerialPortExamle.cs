using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using YanZhiwei.DotNet2.Utilities.Common;
using YanZhiwei.DotNet2.Utilities.Enums;
using YanZhiwei.DotNet2.Utilities.WinForm;
namespace YanZhiwei.DotNet2.Utilities.WinFromExamples
{
    public partial class winSerialPortExamle : Form
    {
        public winSerialPortExamle()
        {
            InitializeComponent();
        }

        private void SerialPortExamle_Load(object sender, EventArgs e)
        {


            comboBox1.DataSource = EnumHelper.GetValues<int>(typeof(SerialPortBaudRates));

        }
    }
}
