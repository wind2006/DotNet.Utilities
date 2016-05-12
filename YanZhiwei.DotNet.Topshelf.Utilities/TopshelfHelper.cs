namespace YanZhiwei.DotNet.Topshelf.Utilities
{
    using global::Topshelf;
    using global::Topshelf.HostConfigurators;
    using System;

    using YanZhiwei.DotNet2.Utilities.Common;

    /// <summary>
    /// Topshelf 辅助类
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    /// 时间：2016-02-02 15:57
    /// 备注：
    public class TopshelfHelper<T>
        where T : class, new()
    {
        #region Fields

        /// <summary>
        /// 安装服务后，服务的描述
        /// </summary>
        /// 时间：2016-02-02 15:03
        /// 备注：
        public readonly string Description;

        /// <summary>
        /// 显示名称
        /// </summary>
        /// 时间：2016-02-02 15:03
        /// 备注：
        public readonly string DisplayName;

        /// <summary>
        /// 服务名称
        /// </summary>
        /// 时间：2016-02-02 15:03
        /// 备注：
        public readonly string ServiceName;

        /// <summary>
        /// 服务运行身份标识
        /// </summary>
        /// 时间：2016-02-02 15:13
        /// 备注：
        public ServiceRunAs ServiceRunAs;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="description">安装服务后，服务的描述</param>
        /// <param name="displayName">显示名称</param>
        /// <param name="serviceName">服务名称</param>
        /// 时间：2016-02-02 15:04
        /// 备注：
        public TopshelfHelper(string description, string displayName, string serviceName)
        {
            ValidateHelper.Begin().NotNullOrEmpty(description, "安装服务后，服务的描述").NotNullOrEmpty(displayName, "服务显示名称").NotNullOrEmpty(serviceName, "服务名称");
            Description = description;
            DisplayName = displayName;
            ServiceName = serviceName;
            ServiceRunAs = ServiceRunAs.LocalSystem;
        }

        #endregion Constructors

        #region Events

        /// <summary>
        /// 服务启动事件
        /// </summary>
        /// 时间：2016-02-02 15:48
        /// 备注：
        public event Action<T> SerivceStarted;

        /// <summary>
        /// 服务停止事件
        /// </summary>
        /// 时间：2016-02-02 15:48
        /// 备注：
        public event Action<T> SerivceStoped;

        #endregion Events

        #region Properties

        /// <summary>
        /// 服务登陆名称
        /// </summary>
        public string UserName
        {
            get; set;
        }

        /// <summary>
        /// 服务登陆密码
        /// </summary>
        public string UserPassword
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Local   Service账户是预设的拥有最小权限的本地账户，并在网络凭证中具有匿名的身份。Local   Service账户通常可以访问Local   Service、Everyone组还有认证用户有权限访问的资源。
        /// </summary>
        /// 时间：2016-02-02 15:52
        /// 备注：
        public void SetRunAsLocalService()
        {
            ServiceRunAs = ServiceRunAs.LocalService;
        }

        /// <summary>
        /// LocalSystem是预设的拥有本机所有权限的本地账户，这个账户跟通常的用户账户没有任何关联，也没有用户名和密码之类的凭证。这个服务账户可以打开注册表的HKEY_LOCAL_MACHINE\Security键，当LocalSystem访问网络资源时，它是作为计算机的域账户使用的。
        /// </summary>
        /// 时间：2016-02-02 15:51
        /// 备注：
        public void SetRunAsLocalSystem()
        {
            ServiceRunAs = ServiceRunAs.LocalSystem;
        }

        /// <summary>
        /// Network   Service账户是预设的拥有本机部分权限的本地账户，它能够以计算机的名义访问网络资源。但是他没有Local   System   那么多的权限，以这个账户运行的服务会根据实际环境把访问凭据提交给远程的计算机。Network   Service账户通常可以访问Network   Service、Everyone组，还有认证用户有权限访问的资源。
        /// </summary>
        /// 时间：2016-02-02 15:52
        /// 备注：
        public void SetRunAsNetworkService()
        {
            ServiceRunAs = ServiceRunAs.NetworkService;
        }

        /// <summary>
        /// 当安装了服务时，安装程序将提示输入用户名/密码组合用于启动该服务。
        /// </summary>
        /// 时间：2016-02-02 15:52
        /// 备注：
        public void SetRunAsPrompt()
        {
            ServiceRunAs = ServiceRunAs.Prompt;
        }

        /// <summary>
        /// 使用用户凭据
        /// </summary>
        /// <param name="userName">用户名称</param>
        /// <param name="userPassword">用户密码</param>
        /// 时间：2016-02-02 15:57
        /// 备注：
        public void SetServiceRunAs(string userName, string userPassword)
        {
            ServiceRunAs = ServiceRunAs.UserLogin;
            ValidateHelper.Begin().NotNullOrEmpty(userName, "服务登陆用户名称").NotNullOrEmpty(userPassword, "服务登陆用户密码");
            UserName = userName;
            UserPassword = userPassword;
        }

        /// <summary>
        /// 无依赖的服务启动
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// 时间：2016-02-02 15:35
        /// 备注：
        public void StartService()
        {
            HostFactory.Run(x =>
            {
                x.Service<T>(s =>
                {
                    s.ConstructUsing(name => new T());
                    s.WhenStarted(SerivceStarted);
                    s.WhenStopped(SerivceStoped);
                });

                SetServiceRunAs(x);
                x.RunAsLocalSystem();
                x.SetDescription(Description);        //安装服务后，服务的描述
                x.SetDisplayName(DisplayName);                       //显示名称
                x.SetServiceName(ServiceName);                       //服务名称
            });
        }

        private void SetServiceRunAs(HostConfigurator x)
        {
            switch (ServiceRunAs)
            {
                case ServiceRunAs.LocalService:
                    x.RunAsLocalService();
                    break;

                case ServiceRunAs.LocalSystem:
                    x.RunAsLocalSystem();
                    break;

                case ServiceRunAs.NetworkService:
                    x.RunAsNetworkService();
                    break;

                case ServiceRunAs.Prompt:
                    x.RunAsPrompt();
                    break;

                case ServiceRunAs.UserLogin:
                    x.RunAs(UserName, UserPassword);
                    break;
            }
        }

        #endregion Methods
    }
}