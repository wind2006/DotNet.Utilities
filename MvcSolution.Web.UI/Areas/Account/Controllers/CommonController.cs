using MvcSolution.Web.UI.Common;
using System.Web.Mvc;
using YanZhiwei.DotNet2.Utilities.ValidateCode;
using YanZhiwei.Framework.Mvc;

namespace MvcSolution.Web.UI.Areas.Account.Controllers
{
    public class CommonController : AdminControllerBase
    {
        [AuthorizeIgnore]
        public virtual ActionResult VerifyImage()
        {
            var validateCodeType = new ValidateCode_Style10();
            string code = "6666";
            byte[] bytes = validateCodeType.CreateImage(out code);
            this.CookieContext.VerifyCodeGuid = VerifyCodeHelper.SaveVerifyCode(code);

            return File(bytes, @"image/jpeg");
        }
    }
} 