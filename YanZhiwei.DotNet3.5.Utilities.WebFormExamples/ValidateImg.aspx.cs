using System;
using System.Collections.Generic;
using YanZhiwei.DotNet3._5.Utilities.WebForm.Core;

namespace YanZhiwei.DotNet3._5.Utilities.WebFormExamples
{
    public partial class ValidateImg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Create();
        }

        private void Create()
        {
            VerificationCode v = new VerificationCode();

            //是否随机字体颜色
            v.SetIsRandomColor = true;
            //随机码的旋转角度
            v.SetRandomAngle = 4;
            //文字大小
            v.SetFontSize = 15;
            //背景色
            //v.SetBackgroundColor
            //前景噪点数量
            //v.SetForeNoisePointCount = 3;
            //v.SetFontColor =Color.Red;
            //...还有更多设置不介绍了

            var questionList = new Dictionary<string, string>()
           {
               {"1+1=？","2"},
               {"喜羊羊主角叫什么名字？","喜羊羊" },
               {"【我爱你】中间的那个字？","爱" },
               {"谁是胖胖？今天生日的那个就是胖胖哈！","胡晓霞" },
           };

            var questionItem = v.GetQuestion(questionList);//不赋值为随机验证码 例如： 1*2=? 这种

            //指定验证文本
            //v.SetVerifyCodeText

            v.SetVerifyCodeText = questionItem.Key;

            Session["VerifyCode"] = questionItem.Value;

            //输出图片
            v.OutputImage(System.Web.HttpContext.Current.Response);
        }
    }
}