namespace YanZhiwei.DotNet2.Utilities.Core
{
    using System;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.IO;
    using System.Text;

    using YanZhiwei.DotNet2.Utilities.Common;

    /// <summary>
    /// 日志类
    /// </summary>
    public sealed class Logger
    {
        #region Fields

        /// <summary>
        /// The log_space
        /// </summary>
        private const string logspace = "                     ";

        /// <summary>
        /// The logtextformat
        /// </summary>
        private const string logtextformat = "        {0} = {1}";

        /// <summary>
        /// 用于Trace的组织输出的类别名称
        /// </summary>
        private const string tracesql = "\r\n***********************TRACE_SQL {0}*****************************\r\nTRACE_SQL";

        /// <summary>
        /// 1   仅控制台输出
        /// 2   仅日志输出
        /// 3   控制台+日志输出
        /// </summary>
        private static readonly int Flag = 2;

        /// <summary>
        /// 日志根目录
        /// </summary>
        private static readonly string LogRootDirectory = string.Format(@"{0}\Log", ProjectHelper.GetExecuteDirectory());

        /// <summary>
        /// 当前日志的日期
        /// </summary>
        private static DateTime logCurFileDate = DateTime.Now;

        /// <summary>
        /// 日志子目录
        /// </summary>
        private static string logFolder;

        /// <summary>
        /// 日志对象
        /// </summary>
        private static TextWriterTraceListener traceListener;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes the <see cref="Logger"/> class.
        /// </summary>
        static Logger()
        {
            Trace.AutoFlush = true;

            switch (Flag)
            {
                case 1:
                    Trace.Listeners.Add(new ConsoleTraceListener());
                    break;

                case 2:
                    Trace.Listeners.Add(TWTL);
                    break;

                case 3:
                    Trace.Listeners.Add(new ConsoleTraceListener());
                    Trace.Listeners.Add(TWTL);
                    break;
            }
        }

        #endregion Constructors

        #region Delegates

        /// <summary>
        /// 异常类型日志委托
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        private delegate void AsyncLogException(string message, Exception ex);

        /// <summary>
        /// 日志委托
        /// </summary>
        /// <param name="message">The message.</param>
        private delegate void AsyncLogMessage(string message);

        /// <summary>
        /// sql类型日志委托
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="parameter">The parameter.</param>
        private delegate void AsyncLogSql(string sql, params SqlParameter[] parameter);

        /// <summary>
        /// sql类型日志委托
        /// </summary>
        /// <param name="cmd">The command.</param>
        private delegate void AsyncLogSqlCommand(SqlCommand cmd);

        /// <summary>
        /// sql类型日志委托
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="sql">The SQL.</param>
        /// <param name="parameter">The parameter.</param>
        private delegate void AsyncLogSqlMessage(string message, string sql, params SqlParameter[] parameter);

        #endregion Delegates

        #region Properties

        /// <summary>
        /// 日志文件路径
        /// </summary>
        /// <returns></returns>
        private static string Logfullpath
        {
            get
            {
                return string.Concat(LogRootDirectory, '\\', string.Concat(logFolder, @"\", logCurFileDate.FormatDate(0), ".log"));
            }
        }

        /// <summary>
        /// 跟踪输出日志文件
        /// </summary>
        private static TextWriterTraceListener TWTL
        {
            get
            {
                if (traceListener == null)
                {
                    if (string.IsNullOrEmpty(logFolder))
                    {
                        CreateLogFolder(DateTime.Now);
                    }
                    else
                    {
                        string _logPath = Logfullpath;
                        if (!Directory.Exists(Path.GetDirectoryName(_logPath)))
                        {
                            CreateLogFolder(DateTime.Now);
                        }
                    }

                    traceListener = new TextWriterTraceListener(Logfullpath);
                }

                return traceListener;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// 日志记录
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        public static void Write(string message, Exception ex)
        {
            new AsyncLogException(BeginTraceError).BeginInvoke(message, ex, null, null);
        }

        /// <summary>
        /// 日志记录
        /// </summary>
        /// <param name="message">The message.</param>
        public static void Write(string message)
        {
            new AsyncLogMessage(BeginTrace).BeginInvoke(message, null, null);
        }

        /// <summary>
        /// 异步SQL日志
        /// </summary>
        /// <param name="cmd">The command.</param>
        public static void Write(SqlCommand cmd)
        {
            new AsyncLogSqlCommand(BeginTraceSqlCommand).BeginInvoke(cmd, null, null);
        }

        /// <summary>
        /// SQL日志
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="sql">The SQL.</param>
        /// <param name="parameter">The parameter.</param>
        public static void Write(string message, string sql, params SqlParameter[] parameter)
        {
            new AsyncLogSqlMessage(BeginTraceSql).BeginInvoke(message, sql, parameter, null, null);
        }

        /// <summary>
        /// SQL日志
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="parameter">The parameter.</param>
        public static void Write(string sql, params SqlParameter[] parameter)
        {
            new AsyncLogSql(BeginTraceSql).BeginInvoke(sql, parameter, null, null);
        }

        /// <summary>
        /// Begins the trace.
        /// </summary>
        /// <param name="message">The message.</param>
        private static void BeginTrace(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                CheckedLogPolicy();
                Trace.WriteLine(string.Format("{0}：{1}{2}", DateTime.Now.FormatDate(1), message, Environment.NewLine));
            }
        }

        /// <summary>
        /// Begins the trace error.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        private static void BeginTraceError(string message, Exception ex)
        {
            if (null != ex)
            {
                CheckedLogPolicy();
                StringBuilder _builder = new StringBuilder();
                _builder.AppendFormat("{0}：{1}", DateTime.Now.FormatDate(1), message);
                _builder.AppendLine();
                _builder.Append(ex.FormatMessage(false, logspace));
                Trace.WriteLine(_builder);
            }
        }

        /// <summary>
        /// Begins the trace SQL.
        /// </summary>
        /// <param name="sql">The SQL.</param>
        /// <param name="parameter">The parameter.</param>
        private static void BeginTraceSql(string sql, params SqlParameter[] parameter)
        {
            if (!string.IsNullOrEmpty(sql))
            {
                CheckedLogPolicy();
                StringBuilder _builder = new StringBuilder();
                _builder.AppendFormat("{0}：{1}", DateTime.Now.FormatDate(1), sql);
                _builder.AppendLine();
                if (parameter != null)
                {
                    foreach (SqlParameter param in parameter)
                    {
                        _builder.AppendFormat("{0}{1}={2}", logspace, param.ParameterName, param.Value).AppendLine();
                    }
                }

                Trace.WriteLine(_builder);
            }
        }

        /// <summary>
        /// Begins the trace SQL.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="sql">The SQL.</param>
        /// <param name="parameter">The parameter.</param>
        private static void BeginTraceSql(string message, string sql, params SqlParameter[] parameter)
        {
            if (!string.IsNullOrEmpty(sql))
            {
                CheckedLogPolicy();

                StringBuilder _builder = new StringBuilder();
                _builder.AppendFormat("{0}：{1}", DateTime.Now.FormatDate(1), message).AppendLine();
                _builder.AppendFormat("{0}SQL：{1}", logspace, sql).AppendLine();
                if (parameter != null)
                {
                    foreach (SqlParameter param in parameter)
                    {
                        _builder.AppendFormat("{0}{1}={2}", logspace, param.ParameterName, param.Value).AppendLine();
                    }
                }

                Trace.WriteLine(_builder);
            }
        }

        /// <summary>
        /// Begins the trace SQL command.
        /// </summary>
        /// <param name="cmd">The command.</param>
        private static void BeginTraceSqlCommand(SqlCommand cmd)
        {
            if (null != cmd)
            {
                SqlParameter[] _parameter = new SqlParameter[cmd.Parameters.Count];
                cmd.Parameters.CopyTo(_parameter, 0);
                BeginTraceSql(cmd.CommandText, _parameter);
            }
        }

        /// <summary>
        /// 检查日志策略；根据时间生成日志策略
        /// </summary>
        private static void CheckedLogPolicy()
        {
            if (!DateTime.Now.IsDateEqual(logCurFileDate))
            {
                DateTime _currentDate = DateTime.Now.Date;
                CreateLogFolder(_currentDate);
                logCurFileDate = _currentDate;
                Trace.Flush();
                if (traceListener != null)
                {
                    Trace.Listeners.Remove(traceListener);
                }

                Trace.Listeners.Add(TWTL);
            }
        }

        /// <summary>
        /// 创建日志存储文件夹
        /// </summary>
        /// <param name="date">时间</param>
        private static void CreateLogFolder(DateTime date)
        {
            int _year = date.Year,
                _month = date.Month;
            string _logFolder = string.Concat(_year, '\\', _month),
                   _logFullPath = Path.Combine(LogRootDirectory, _logFolder);
            if (!Directory.Exists(_logFullPath))
            {
                Directory.CreateDirectory(_logFullPath);
            }

            logFolder = _logFolder;
        }

        #endregion Methods
    }
}