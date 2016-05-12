using System;
using System.Collections.Generic;
using System.Windows.Forms;
using YanZhiwei.DotNet2.Utilities.Common;
using YanZhiwei.DotNet2.Utilities.WinForm;
using YanZhiwei.DotNet2.Utilities.WinForm.Core;

namespace YanZhiwei.DotNet2.Utilities.WinFromExamples
{
    public partial class winGridExamle : Form
    {
        public winGridExamle()
        {
            InitializeComponent();
        }

        private void winGridExamle_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Person> personList = new List<Person>();
            for (byte i = 0; i < 200; i++)
            {
                Person _person = new Person();

                _person.Name = RandomHelper.NetxtString(4, false);
                _person.Age = (byte)RandomHelper.NextNumber(1, 99);
                _person.InsertTime = RandomHelper.NextDateTime();
                _person.LoginDate = RandomHelper.NextDateTime().FormatDate(0);
                _person.Gender = RandomHelper.NextBool();

                personList.Add(_person);
            }
            dataGridView1.DynamicBind<Person>(personList);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.AutoCellWidth();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.DrawSequenceNumber();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DateTimePicker _newDatePicker = new DateTimePicker();
            _newDatePicker.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            _newDatePicker.Format = DateTimePickerFormat.Custom;
            _newDatePicker.ShowUpDown = true;
            dataGridView1.ApplyDateTimePicker(_newDatePicker, 2);
            dataGridView1.ApplyDateTimePicker(_newDatePicker, 3);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //DatagridViewCheckBoxHeaderCell cbHeader = new DatagridViewCheckBoxHeaderCell();
            //dataGridView1.Columns[4].HeaderCell = cbHeader;
            //dataGridView1.Columns[4].HeaderText = "全选";
            //cbHeader.OnCheckBoxClicked += new CheckBoxClickedHandler(cbHeader_OnCheckBoxClicked);
            dataGridView1.ApplyHeaderCheckbox(4, "全选");
        }

        private void button6_Click(object sender, EventArgs e)
        {
          
        }

        //private void cbHeader_OnCheckBoxClicked(bool state)
        //{
        //    int count = dataGridView1.Rows.Count;
        //    for (int i = 0; i < count; i++)
        //    {
        //        DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)dataGridView1.Rows[i].Cells[4];
        //        checkCell.Value = state;
        //    }
        //}
    }

    internal class Person
    {
        public string Name { get; set; }

        public byte Age { get; set; }

        public DateTime InsertTime { get; set; }

        public string LoginDate { get; set; }
        public bool Gender { get; set; }
    }
}