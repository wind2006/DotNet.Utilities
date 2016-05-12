using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using YanZhiwei.DotNet2.Utilities.WinForm;

namespace YanZhiwei.DotNet4.Utilities.WinForm.Examples
{
    public partial class FormBW : Form
    {
        private BWHelper backWorkerHelper;

        public FormBW()
        {
            InitializeComponent();
        }
        private void backgroundWorkerExample_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

            progressBarBW.UIThread<ProgressBar>(pr => pr.Value = e.ProgressPercentage);
        }

        private void backgroundWorkerExample_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            labelTimeLeft.UIThread<Label>(lbl => lbl.Text = "Completed");
            progressBarBW.UIThread<ProgressBar>(pr => pr.Value = 100);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            List<Action> actions = new List<Action>();
            for (int i = 0; i < 100; i++)
                actions.Add(() => Thread.Sleep(200));
            backWorkerHelper.SetActionsTodo(actions);
            backWorkerHelper.IsParallel = checkBoxUseParallel.Checked;
    
            backgroundWorker1.RunWorkerAsync();
        }

        private void FormBW_Load(object sender, EventArgs e)
        {
            backWorkerHelper = new BWHelper(backgroundWorker1);
            buttonStart.Click += new EventHandler(buttonStart_Click);
            buttonCancel.Click += new EventHandler(buttonCancel_Click);
            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorkerExample_ProgressChanged);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorkerExample_RunWorkerCompleted);
      
            backWorkerHelper.TimeLeft.ValueChanged += TimeLeft_ValueChanged;
        }
        private void TimeLeft_ValueChanged(TimeSpan oldValue, TimeSpan newValue)
        {
            labelTimeLeft.UIThread<Label>(lbl => lbl.Text = string.Format("Time left: {0} seconds", newValue.TotalSeconds));
        }

        private void buttonStart_Click_1(object sender, EventArgs e)
        {

        }
    }
}