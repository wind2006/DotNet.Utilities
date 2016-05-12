using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YanZhiwei.DotNet2.Utilities.Enums;
using YanZhiwei.DotNet3._5.Utilities.WebForm.Core;

namespace YanZhiwei.DotNet3._5.Utilities.WebFormExamples
{
    public partial class WebUploadImageDemo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.IsAuthenticated)
            {
                if (Request.Files.Count > 0)
                {
                    WebUploadImage _uploadImage = new WebUploadImage();
                    _uploadImage.SetWordWater = "哈哈";//文字水印
                    _uploadImage.SetPicWater = Server.MapPath("2.png");//图片水印(图片和文字都赋值图片有效)
                    _uploadImage.SetPositionWater = SetWaterPosition.bottomLeft;//水印图片的位置 0居中、1左上角、2右上角、3左下角、4右下角

                    _uploadImage.SetSmallImgHeight = "110,40,20";//设置缩略图可以多个
                    _uploadImage.SetSmallImgWidth = "100,40,20";

                    //保存图片生成缩略图
                    //  var _message = _uploadImage.FileSaveAs(Request.Files[0], Server.MapPath("~/file/temp"));
                    var _message = _uploadImage.FileCutSaveAs(Request.Files[0], Server.MapPath("~/file/temp2"), 400, 300, ImageCutMode.CutWH);
                    Label1.Text = _message.IsError == false ? "上传成功" : "上传失败" + _message.Message;
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //WebUploadImage _uploadImage = new WebUploadImage();
            //_uploadImage.FileCutSaveAs(Server.MapPath("2.png"), Server.MapPath("Img.jpg"));
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            //WebUploadImage _uploadImage = new WebUploadImage();
            //_uploadImage.AttachText("言志伟", Server.MapPath("005.jpg"));
        }
    }
}