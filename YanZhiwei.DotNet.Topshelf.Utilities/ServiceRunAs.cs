namespace YanZhiwei.DotNet.Topshelf.Utilities
{
    /// <summary>
    /// 服务运行身份标识
    /// </summary>
    /// 时间：2016-02-02 15:11
    /// 备注：
    public enum ServiceRunAs
    {
        /// <summary>
        /// LocalSystem是预设的拥有本机所有权限的本地账户，这个账户跟通常的用户账户没有任何关联，也没有用户名和密码之类的凭证。这个服务账户可以打开注册表的HKEY_LOCAL_MACHINE\Security键，当LocalSystem访问网络资源时，它是作为计算机的域账户使用的。 
        /// </summary>
        LocalSystem,

        /// <summary>
        /// LocalService账户是预设的拥有最小权限的本地账户，并在网络凭证中具有匿名的身份。Local   Service账户通常可以访问Local   Service、Everyone组还有认证用户有权限访问的资源。
        /// </summary>
        LocalService,

        /// <summary>
        /// Network  Service账户是预设的拥有本机部分权限的本地账户，它能够以计算机的名义访问网络资源。但是他没有Local   System   那么多的权限，以这个账户运行的服务会根据实际环境把访问凭据提交给远程的计算机。Network   Service账户通常可以访问Network   Service、Everyone组，还有认证用户有权限访问的资源。
        /// </summary>
        NetworkService,

        /// <summary>
        /// 当安装了服务时，安装程序将提示输入用户名/密码组合用于启动该服务。
        /// </summary>
        Prompt,

        /// <summary>
        /// 用户登陆凭据
        /// </summary>
        UserLogin
    }
}