using System;
using YanZhiwei.DotNet3._5.Utilities.Model;
using YanZhiwei.DotNet3._5.Utilities.WebForm.Core;

namespace YanZhiwei.DotNet3._5.Utilities.WebFormExamples
{
    public partial class WebUploadFileDemo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                if (Request.Files.Count > 0)
                {
                    WebUploadFile _uploadFile = new WebUploadFile();
                    _uploadFile.SetFileDirectory("/上传");
                    UploadFileMessage _message = _uploadFile.Save(Request.Files["File1"]);
                    Label1.Text = _message.HasError == false ? "上传成功" : "上传失败" + _message.Message;
                }
            }
        }
    }
}