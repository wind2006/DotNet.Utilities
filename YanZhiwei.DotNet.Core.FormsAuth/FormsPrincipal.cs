namespace YanZhiwei.DotNet.Core.FormsAuth
{
    using System;
    using System.Security.Principal;
    using System.Web;
    using System.Web.Security;
    using System.Xml;

    using YanZhiwei.DotNet3._5.Utilities.Common;

    /// <summary>
    ///  Form验证帮助类
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    /// 时间：2016-04-29 11:24
    /// 备注：
    public class FormsPrincipal<T> : IPrincipal
        where T : class, new()
    {
        #region Fields

        private IIdentity identity;
        private T userExtData;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ticket">FormsAuthenticationTicket/param>
        /// <param name="userData">用户数据</param>
        /// 时间：2016-04-29 11:25
        /// 备注：
        public FormsPrincipal(FormsAuthenticationTicket ticket, T userData)
        {
            identity = new FormsIdentity(ticket);
            userExtData = userData;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// 获取当前用户的标识。
        /// </summary>
        /// 时间：2016-04-29 11:25
        /// 备注：
        public IIdentity Identity
        {
            get
            {
                return identity;
            }
        }

        /// <summary>
        /// 用户数据
        /// </summary>
        public T UserData
        {
            get
            {
                return userExtData;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// 用户权限初始化
        /// </summary>
        /// <param name="roles">用户隶属的权限组</param>
        /// 时间：2016-04-29 11:25
        /// 备注：在 Application_AuthenticateRequest()中使用
        public static void AddPermission(string[] roles)
        {
            HttpContext _context = HttpContext.Current;
            if (!_context.Request.IsAuthenticated) return;
            if (HttpContext.Current.User.Identity is FormsIdentity)
            {
                FormsPrincipal<T> _curPrincipal = ParsePrincipal();
                if (_curPrincipal != null)
                {
                    HttpContext.Current.User = new GenericPrincipal(_curPrincipal.Identity, roles);
                }
            }
        }

        /// <summary>
        ///  获取web.config中cookie超时时间，若获取失败，则返回-1
        /// </summary>
        /// <returns></returns>
        /// 时间：2016-04-29 11:26
        /// 备注：
        public static int GetCookieTimeout()
        {
            int _defaultTimeout = 0;
            try
            {
                XmlDocument _webConfig = new XmlDocument();
                _webConfig.Load(HttpContext.Current.Server.MapPath(@"~\web.config"));
                XmlNode _node = _webConfig.SelectSingleNode("/configuration/system.web/authentication/forms");
                if (_node != null && _node.Attributes["timeout"] != null)
                {
                    if (!int.TryParse(_node.Attributes["timeout"].Value, out _defaultTimeout))
                        _defaultTimeout = -1;
                }
            }
            catch (Exception)
            {
                _defaultTimeout = -1;
            }
            return _defaultTimeout;
        }

        /// <summary>
        /// 将用户数据转换为FormsPrincipal
        /// </summary>
        /// <param name="excepHanlder">The excep hanlder.</param>
        /// <returns></returns>
        /// 时间：2016-04-29 11:26
        /// 备注：
        /// <exception cref="System.InvalidOperationException"></exception>
        public static FormsPrincipal<T> ParsePrincipal()
        {
            HttpContext _context = HttpContext.Current;

            FormsIdentity _identity = (FormsIdentity)_context.User.Identity;
            FormsAuthenticationTicket _ticket = _identity.Ticket;
            if (!FormsAuthentication.CookiesSupported)
            {
                _ticket = FormsAuthentication.Decrypt(_identity.Ticket.Name);
            }
            else
            {
                HttpCookie _cookie = _context.Request.Cookies[FormsAuthentication.FormsCookieName];
                if (_cookie == null || string.IsNullOrEmpty(_cookie.Value))
                    return null;
                _ticket = FormsAuthentication.Decrypt(_cookie.Value);
            }

            T _userData = null;
            if (_ticket != null && !string.IsNullOrEmpty(_ticket.UserData))
                _userData = _ticket.UserData.JsonDeserialize<T>();

            if (_ticket != null && _userData != null)
                return new FormsPrincipal<T>(_ticket, _userData);

            return null;
        }

        /// <summary>
        /// 根据ReturnUrl来页面跳转
        /// </summary>
        /// 时间：2016-04-29 11:27
        /// 备注：
        public static void Redirect()
        {
            string _returnUrl = HttpContext.Current.Request.QueryString["ReturnUrl"];
            if (string.IsNullOrEmpty(_returnUrl) == false)
                HttpContext.Current.Response.Redirect(_returnUrl);
        }

        /// <summary>
        ///跳转到web.config中defaultUrl配置连接
        /// </summary>
        /// <param name="userName">用户名称</param>
        /// <param name="persistentCookie">是否持久cookie</param>
        /// 时间：2016-04-29 11:27
        /// 备注：
        public static void RedirectDefaultPage(string userName, bool persistentCookie)
        {
            HttpContext.Current.Response.Redirect(FormsAuthentication.GetRedirectUrl(userName, persistentCookie));
        }

        /// <summary>
        /// 跳转到web.config中defaultUrl配置连接
        /// </summary>
        /// <param name="userName">用户名称</param>
        /// 时间：2016-04-29 11:28
        /// 备注：
        public static void RedirectDefaultPage(string userName)
        {
            HttpContext.Current.Response.Redirect(FormsAuthentication.GetRedirectUrl(userName, true));
        }

        /// <summary>
        ///根据HttpContext对象设置用户标识对象
        /// </summary>
        /// 时间：2016-04-29 11:28
        /// 备注：
        public static void SetUserInfo()
        {
            FormsPrincipal<T> _user = ParsePrincipal();

            if (_user != null)
                HttpContext.Current.User = _user;
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userName">用户名称</param>
        /// <param name="userData">用户数据</param>
        /// <param name="expiration">过期时间，单位：分钟.</param>
        /// <param name="persistentCookie">是否是持久cookie</param>
        /// 时间：2016-04-29 11:28
        /// 备注：
        public static void SignIn(string userName, T userData, int expiration, bool persistentCookie)
        {
            //序列化用户数据
            string _userDbJsonString = SerializationHelper.JsonSerialize<T>(userData);

            //创建票据
            FormsAuthenticationTicket _ticket = new FormsAuthenticationTicket(
                2, userName, DateTime.Now, DateTime.Now.AddMinutes(expiration), persistentCookie, _userDbJsonString);

            string _encryptTicket = FormsAuthentication.Encrypt(_ticket);//加密票据

            if (!FormsAuthentication.CookiesSupported)//如果应用程序已配置为支持无 Cookie 的 Forms 身份验证，则返回 true；否则返回 false。
            {
                FormsAuthentication.SetAuthCookie(_encryptTicket, persistentCookie);//将验证信息,存放在Uri中
            }
            else
            {
                HttpContext _context = HttpContext.Current;
                HttpCookie _cookie = new HttpCookie(FormsAuthentication.FormsCookieName, _encryptTicket);
                _cookie.HttpOnly = true;
                _cookie.Secure = FormsAuthentication.RequireSSL;
                _cookie.Domain = FormsAuthentication.CookieDomain;
                _cookie.Path = FormsAuthentication.FormsCookiePath;
                if (expiration > 0)
                    _cookie.Expires = DateTime.Now.AddMinutes(expiration);
                _context.Response.Cookies.Remove(_cookie.Name);
                _context.Response.Cookies.Add(_cookie);
            }
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userName">用户名称</param>
        /// <param name="userData">用户数据</param>
        /// <param name="expiration">过期时间，单位：分钟.</param>
        /// 时间：2016-04-29 11:29
        /// 备注：默认持久cookie
        public static void SignIn(string userName, T userData, int expiration)
        {
            SignIn(userName, userData, expiration, true);
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userName">用户名称</param>
        /// <param name="userData">用户数据</param>
        /// 时间：2016-04-29 11:29
        /// 备注：默认持久cookie，回话超时20分钟
        public static void SignIn(string userName, T userData)
        {
            SignIn(userName, userData, 20);
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// 时间：2016-04-29 11:30
        /// 备注：
        public static void SignOut()
        {
            FormsAuthentication.SignOut();
        }

        /// <summary>
        /// 用户是否在角色里面
        /// </summary>
        /// <param name="role">角色名称</param>
        /// <returns></returns>
        /// 时间：2016-04-29 11:30
        /// 备注：
        /// <exception cref="System.NotImplementedException"></exception>
        public bool IsInRole(string role)
        {
            IPrincipal principal = userExtData as IPrincipal;
            if (principal == null)
                throw new NotImplementedException();
            else
                return principal.IsInRole(role);
        }

        #endregion Methods
    }
}