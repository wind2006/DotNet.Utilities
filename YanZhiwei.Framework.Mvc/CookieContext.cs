namespace YanZhiwei.Framework.Mvc
{
    using System;
    using System.Web;

    using YanZhiwei.DotNet2.Utilities.Common;
    using YanZhiwei.DotNet2.Utilities.WebForm.Core;
    using YanZhiwei.DotNet4.Utilities.Common;

    /// <summary>
    /// Cookie上下文
    /// </summary>
    /// 时间：2016-01-14 13:33
    /// 备注：
    public class CookieContext : IAuthCookie
    {
        #region Fields

        //默认密钥向量
        private static byte[] iv = { 0x11, 0x33, 0x57, 0x79, 0x91, 0xAC, 0xDC, 0xEF };

        //用户过期时间，10小时
        private int userExpiresHours = 10;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        public CookieContext()
        {
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// 是否启用验证码验证，默认当登陆错误超过一次启用
        /// </summary>
        public bool IsNeedVerifyCode
        {
            get
            {
                return LoginErrorTimes > 1;
            }
        }

        /// <summary>
        /// Cache或者CookieManger的Key前缀
        /// </summary>
        public virtual string KeyPrefix
        {
            get
            {
                return "Context_";
            }
        }

        /// <summary>
        /// 登陆错误时间
        /// </summary>
        public virtual int LoginErrorTimes
        {
            get
            {
                return CookieManger.GetValue(KeyPrefix + "LoginErrorTimes").ToIntOrDefault(0);
            }
            set
            {
                CookieManger.Save(KeyPrefix + "LoginErrorTimes", value.ToString(), 1);
            }
        }

        /// <summary>
        /// 用户过期时间，默认10小时
        /// </summary>
        public virtual int UserExpiresHours
        {
            get
            {
                return userExpiresHours;
            }
            set
            {
                userExpiresHours = value;
            }
        }

        /// <summary>
        /// 用户标识ID
        /// </summary>
        public virtual int UserId
        {
            get
            {
                return CookieManger.GetValue(KeyPrefix + "UserId").ToIntOrDefault(0);
            }
            set
            {
                CookieManger.Save(KeyPrefix + "UserId", value.ToString(), UserExpiresHours);
            }
        }

        /// <summary>
        /// 用户名称
        /// </summary>
        public virtual string UserName
        {
            get
            {
                return HttpUtility.UrlDecode(CookieManger.GetValue(KeyPrefix + "UserName"));
            }
            set
            {
                CookieManger.Save(KeyPrefix + "UserName", HttpUtility.UrlEncode(value), UserExpiresHours);
            }
        }

        /// <summary>
        /// 用户凭据
        /// </summary>
        public virtual Guid UserToken
        {
            get
            {
                return CookieManger.GetValue(KeyPrefix + "UserToken").ToGuid();
            }
            set
            {
                CookieManger.Save(KeyPrefix + "UserToken", value.ToString(), UserExpiresHours);
            }
        }

        /// <summary>
        /// 验证码
        /// </summary>
        public virtual string VerifyCode
        {
            get
            {
                string _verifyCode = DESEncryptHelper.Encrypt(CookieManger.GetValue(KeyPrefix + "VerifyCode"));

                //获取完CookieManger后马上过期，重新生成新的验证码
                CookieManger.Save(KeyPrefix + "VerifyCode", DESEncryptHelper.Encrypt(DateTime.Now.Ticks.ToString()), 1);

                return _verifyCode;
            }
            set
            {
                CookieManger.Save(KeyPrefix + "VerifyCode", DESEncryptHelper.Decrypt(value), 1);
            }
        }

        /// <summary>
        /// 验证码
        /// </summary>
        public virtual Guid VerifyCodeGuid
        {
            get
            {
                string _verifyCodeGuid = CookieManger.GetValue(KeyPrefix + "VerifyCodeGuid");
                return Guid.Parse(_verifyCodeGuid);
            }
            set
            {
                CookieManger.Save(KeyPrefix + "VerifyCodeGuid", value.ToString(), 1);
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// 设置cookie
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expiresHours">过期时间，小时</param>
        public void Set(string key, string value, int expiresHours = 0)
        {
            if (expiresHours > 0)
                CookieManger.Save(KeyPrefix + key, value, expiresHours);
            else
                CookieManger.Save(KeyPrefix + key, value);
        }

        #endregion Methods
    }
}