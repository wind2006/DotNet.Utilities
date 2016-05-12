namespace YanZhiwei.DotNet2.Utilities.Common
{
    using System;
    using System.Collections.Generic;
    using System.DirectoryServices;
    using System.Text.RegularExpressions;

    /// <summary>
    /// AD域帮助类
    /// </summary>
    public class ADDomainHelper
    {
        #region Fields

        /// <summary>
        /// 域名称
        /// </summary>
        public readonly string ADDomian;

        /// <summary>
        /// 用户名称
        /// </summary>
        public readonly string UserName;

        /// <summary>
        /// 用户密码
        /// </summary>
        public readonly string UserPassword;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="domain">域名称</param>
        /// <param name="userName">用户名称</param>
        /// <param name="userPassword">用户密码</param>
        public ADDomainHelper(string domain, string userName, string userPassword)
        {
            this.UserName = userName;
            this.UserPassword = userPassword;
            this.ADDomian = domain;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// 取用户所对应的用户组
        /// </summary>
        /// <returns>集合</returns>
        public List<string> GetGroups()
        {
            List<string> _groups = new List<string>();
            try
            {
                DirectoryEntry _directoryEntity = new DirectoryEntry(string.Format("LDAP://{0}", this.ADDomian), this.UserName, this.UserPassword);
                _directoryEntity.RefreshCache();

                DirectorySearcher _searcher = new DirectorySearcher(_directoryEntity);
                _searcher.PropertiesToLoad.Add("memberof");
                _searcher.Filter = string.Format("sAMAccountName={0}", this.UserName);
                SearchResult _seachResult = _searcher.FindOne();

                if (_seachResult != null)
                {
                    ResultPropertyValueCollection _valueCollect = _seachResult.Properties["memberof"];
                    foreach (object group in _valueCollect)
                    {
                        string _group = group.ToString();
                        Match _match = Regex.Match(_group, @"CN=\s*(?<g>\w*)\s*.");
                        _groups.Add(_match.Groups["g"].Value);
                    }
                }
            }
            catch (Exception)
            {
                _groups = null;
            }

            return _groups;
        }

        /// <summary>
        /// 登陆域
        /// </summary>
        /// <returns>登陆是否成功</returns>
        public bool Login()
        {
            bool _result = false;

            try
            {
                DirectoryEntry _directoryEntity = new DirectoryEntry(string.Format("LDAP://{0}", this.ADDomian), this.UserName, this.UserPassword);
                _directoryEntity.RefreshCache();
                _result = true;
            }
            catch
            {
                _result = false;
            }

            return _result;
        }

        #endregion Methods
    }
}