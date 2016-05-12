using System;
using System.Diagnostics;
using YanZhiwei.DotNet2.Utilities.WebForm;

namespace YanZhiwei.DotNet2.Utilities.WebFormExamples
{
    public partial class FileDownHelperDemo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Tuple<bool, Exception> _result = FileDownHelper.ResponseFile("动物世界", @"C:\Users\Public\Videos\Sample Videos\Wildlife.wmv");
            Debug.WriteLine(_result.Item1 == true ? "成功" : "失败");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Tuple<bool, Exception> _result = FileDownHelper.ResponseFile("动物世界", @"C:\Users\Public\Videos\Sample Videos\Wildlife.wmv", 102400);
            Debug.WriteLine(_result.Item1 == true ? "成功" : "失败");
        }
    }
}