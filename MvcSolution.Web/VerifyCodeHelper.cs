using System;

namespace MvcSolution.Web
{
    public class VerifyCodeHelper
    {
        public static Guid SaveVerifyCode(string verifyCode)
        {
            var userService = ServiceContext.Current.AccountService;
            var result = userService.SaveVerifyCode(verifyCode);
            return result;
        }

        public static bool CheckVerifyCode(string verifyCodeText, Guid guid)
        {
            var userService = ServiceContext.Current.AccountService;
            var result = userService.CheckVerifyCode(verifyCodeText, guid);
            return result;
        }
    }
}