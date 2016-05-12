using System;
using System.Text;
using System.Windows.Forms;
using YanZhiwei.DotNet2.Utilities.Common;
using YanZhiwei.DotNet2.Utilities.WinForm;

namespace YanZhiwei.DotNet.Toolbox
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        #region 获取DLL，EXE编译日期

        private void btnGetFilePath_Click(object sender, EventArgs e)
        {
            this.txtFilePath.Text = OpenFileDialogHelper.GetFileName("选择DLL或EXE文件.", "EXE文件(*.exe)|*exe|DLL文件(*.dll)|*.dll");
        }

        private void btnGetBuilderDateByFile_Click(object sender, EventArgs e)
        {
            string _filePath = txtFilePath.Text.Trim();
            if (CheckedFilePath(_filePath))
            {
                AssemblyHelper _assHelper = new AssemblyHelper(_filePath);
                string _fileName = FileHelper.GetFileName(_filePath);
                string _builderDate = string.Format("{0}-->{1}", _fileName, _assHelper.GetBuildDateTime().FormatDate(1));
                AddOutputMessage(_builderDate);
            }
        }

        private bool CheckedFilePath(string filePath)
        {
            bool _result = true;
            if (string.IsNullOrEmpty(filePath))
            {
                txtFilePath.ShowWarningToolTip<TextBox>(toolTip, "请选择文件!", "eg:C:\\demo.dll");
                _result = false;
            }
            return _result;
        }

        private void btnGetFolderPath_Click(object sender, EventArgs e)
        {
            this.txtFolderpath.Text = OpenFolderDialogHelper.GetFolderName();
        }

        private void btnGetBuilderDateByFolder_Click(object sender, EventArgs e)
        {
            string _folder = this.txtFolderpath.Text.Trim();
            if (CheckedFoloderPath(_folder))
            {
                StringBuilder _builder = new StringBuilder();
                _builder.AppendFormat("编译日期如下：{0}", Environment.NewLine);
                FileHelper.RecursiveFolder(_folder, p =>
                {
                    string _fullPath = p.FullName,
                           _ext = FileHelper.GetFileEx(_fullPath);
                    if (string.Compare(_ext, ".exe", true) == 0 || string.Compare(_ext, ".dll", true) == 0)
                    {
                        AssemblyHelper _assHelper = new AssemblyHelper(p.FullName);
                        _builder.AppendFormat("{0}-->{1}{2}", FileHelper.GetFileName(p.FullName), _assHelper.GetBuildDateTime(), Environment.NewLine);
                    }
                });
                string _message = _builder.ToString();
                AddOutputMessage(_message);
            }
        }

        private bool CheckedFoloderPath(string folder)
        {
            bool _result = true;
            if (string.IsNullOrEmpty(folder))
            {
                txtFolderpath.ShowWarningToolTip<TextBox>(toolTip, "请选择文件夹!", "eg:C:\\demo");
                _result = false;
            }
            return _result;
        }

        #endregion 获取DLL，EXE编译日期

        private void AddOutputMessage(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                StringBuilder _builder = new StringBuilder();
                _builder.AppendFormat("{0} {1}{2}", DateTime.Now.FormatDate(10), message, Environment.NewLine);
                txtOutput.Text = _builder.ToString();
            }
        }

        private void btnGetMac_Click(object sender, EventArgs e)
        {

        }
    }
}