namespace YanZhiwei.DotNet2.Utilities.Common
{
    using System;
    using System.Net.Mail;
    using System.Text;

    using YanZhiwei.DotNet2.Utilities.Models;

    /// <summary>
    /// SmtpClient 帮助类
    /// </summary>
    public class SmtpClientHelper
    {
        #region Fields

        /// <summary>
        /// 附件
        /// </summary>
        public readonly string[] attachmentsPathList;

        /// <summary>
        /// 正文是否是html格式
        /// </summary>
        public readonly bool isbodyHtml;

        /// <summary>
        /// 正文
        /// </summary>
        public readonly string mailBody;

        /// <summary>
        /// 抄送
        /// </summary>
        public readonly string[] mailCcList;

        /// <summary>
        /// 标题
        /// </summary>
        public readonly string mailSubject;

        /// <summary>
        /// 收件人
        /// </summary>
        public readonly string[] mailToList;

        /// <summary>
        /// 发送者昵称
        /// </summary>
        public readonly string nickName;

        /// <summary>
        /// 优先级别
        /// </summary>
        public readonly MailPriority priority;

        /// <summary>
        /// SMTP服务器
        /// </summary>
        public readonly SmtpServer stmpServer;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="stmpserver">SMTP服务器</param>
        /// <param name="nickname">发送者昵称</param>
        /// <param name="mailsubject">标题</param>
        /// <param name="mailbody">正文</param>
        /// <param name="mailTolist">收件人</param>
        /// <param name="mailCclist">抄送</param>
        /// <param name="attachmentsPathlist">附件</param>
        /// <param name="isbodyhtml">正文是否是html格式</param>
        /// <param name="mailPriority">优先级别</param>
        public SmtpClientHelper(SmtpServer stmpserver, string nickname, string mailsubject, string mailbody, string[] mailTolist, string[] mailCclist, string[] attachmentsPathlist, bool isbodyhtml, MailPriority mailPriority)
        {
            this.stmpServer = stmpserver;
            this.nickName = nickname;
            this.mailSubject = mailsubject;
            this.mailBody = mailbody;
            this.mailToList = mailTolist;
            this.mailCcList = mailCclist;
            this.attachmentsPathList = attachmentsPathlist;
            this.isbodyHtml = isbodyhtml;
            this.priority = mailPriority;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="stmpserver">SMTP服务器</param>
        /// <param name="nickname">发送者昵称</param>
        /// <param name="mailsubject">标题</param>
        /// <param name="mailbody">正文</param>
        /// <param name="mailTolist">收件人</param>
        public SmtpClientHelper(SmtpServer stmpserver, string nickname, string mailsubject, string mailbody, string[] mailTolist)
            : this(stmpserver, nickname, mailsubject, mailbody, mailTolist, null, null, true, MailPriority.Normal)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="stmpserver">SMTP服务器</param>
        /// <param name="nickname">发送者昵称</param>
        /// <param name="mailsubject">标题</param>
        /// <param name="mailbody">正文</param>
        /// <param name="mailTolist">收件人</param>
        /// <param name="mailPriority">优先级别</param>
        public SmtpClientHelper(SmtpServer stmpserver, string nickname, string mailsubject, string mailbody, string[] mailTolist, MailPriority mailPriority)
            : this(stmpserver, nickname, mailsubject, mailbody, mailTolist, null, null, true, mailPriority)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="stmpserver">SMTP服务器</param>
        /// <param name="nickname">发送者昵称</param>
        /// <param name="mailsubject">标题</param>
        /// <param name="mailbody">正文</param>
        /// <param name="mailTolist">收件人</param>
        /// <param name="attachmentsPathlist">附件</param>
        public SmtpClientHelper(SmtpServer stmpserver, string nickname, string mailsubject, string mailbody, string[] mailTolist, string[] attachmentsPathlist)
            : this(stmpserver, nickname, mailsubject, mailbody, mailTolist, null, attachmentsPathlist, true, MailPriority.Normal)
        {
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <returns>发送返回状态</returns>
        public Tuple<bool, string, Exception> Send()
        {
            MailAddress _mailAddress = new MailAddress(stmpServer.SendMail, nickName);
            MailMessage _mailMessage = new MailMessage();
            InitSendBase(_mailAddress, _mailMessage);
            InitSendMailList(_mailMessage);
            InitSendCcList(_mailMessage);
            Tuple<bool, string, Exception> _attrachResult = InitSendAttachFile(_mailMessage);
            if (_attrachResult.Item1)
            {
                SmtpClient _smtpClient = new SmtpClient();
                _smtpClient.Credentials = new System.Net.NetworkCredential(stmpServer.SendMail, stmpServer.SendMailPasswrod);//设置SMTP邮件服务器
                _smtpClient.Host = stmpServer.Host;

                try
                {
                    _smtpClient.Send(_mailMessage);
                    return Tuple.Create<bool, string, Exception>(true, string.Empty, null);
                }
                catch (SmtpException ex)
                {
                    return Tuple.Create<bool, string, Exception>(false, "发送邮件时候发生错误.", ex);
                }
                catch (Exception ex)
                {
                    return Tuple.Create<bool, string, Exception>(false, "发送邮件时候发生错误.", ex);
                }
            }
            return _attrachResult;
        }

        /// <summary>
        /// 添加邮件附件信息
        /// </summary>
        /// <param name="mailMessage"></param>
        /// <returns></returns>
        private Tuple<bool, string, Exception> InitSendAttachFile(MailMessage mailMessage)
        {
            try
            {
                if (attachmentsPathList != null && attachmentsPathList.Length > 0)
                {
                    Attachment _attachFile = null;
                    foreach (string path in attachmentsPathList)
                    {
                        _attachFile = new Attachment(path);
                        mailMessage.Attachments.Add(_attachFile);
                    }
                }
                return Tuple.Create<bool, string, Exception>(true, string.Empty, null);
            }
            catch (Exception ex)
            {
                return Tuple.Create<bool, string, Exception>(false, "添加邮件附件的时候发送错误.", ex);
            }
        }

        /// <summary>
        /// 初始化邮件基本信息
        /// </summary>
        /// <param name="mailAddress">MailAddress</param>
        /// <param name="mailMessage">mailMessage</param>
        private void InitSendBase(MailAddress mailAddress, MailMessage mailMessage)
        {
            //发件人地址
            mailMessage.From = mailAddress;

            //电子邮件的标题
            mailMessage.Subject = mailSubject;

            //电子邮件的主题内容使用的编码
            mailMessage.SubjectEncoding = Encoding.UTF8;

            //电子邮件正文
            mailMessage.Body = mailBody;

            //电子邮件正文的编码
            mailMessage.BodyEncoding = Encoding.Default;

            //邮件优先级
            mailMessage.Priority = priority;
            //是否HTML格式
            mailMessage.IsBodyHtml = isbodyHtml;
        }

        /// <summary>
        /// 初始化发送邮件抄送集合
        /// </summary>
        /// <param name="mailMessage">MailMessage</param>
        private void InitSendCcList(MailMessage mailMessage)
        {
            if (mailCcList != null)
            {
                for (int i = 0; i < mailCcList.Length; i++)
                {
                    mailMessage.CC.Add(mailCcList[i].ToString());
                }
            }
        }

        /// <summary>
        /// 初始化发送邮件集合
        /// </summary>
        /// <param name="mailMessage">MailMessage</param>
        private void InitSendMailList(MailMessage mailMessage)
        {
            if (mailToList != null)
            {
                for (int i = 0; i < mailToList.Length; i++)
                {
                    mailMessage.To.Add(mailToList[i].ToString());
                }
            }
        }

        #endregion Methods
    }
}