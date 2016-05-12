namespace YanZhiwei.DotNet2.Utilities.Core
{
    using System;
    using System.Data.SqlClient;

    using YanZhiwei.DotNet2.Utilities.Common;

    /// <summary>
    /// SqlServer 数据库备份和还原功能
    /// </summary>
    /// 时间：2016-02-24 14:20
    /// 备注：
    public class SqlServerDbBackUp
    {
        #region Fields

        /// <summary>
        /// 需要备份的数据库
        /// </summary>
        /// 时间：2016-02-24 17:37
        /// 备注：
        public readonly string BackUpDataBase;

        /// <summary>
        /// 连接字符串
        /// </summary>
        /// 时间：2016-02-24 17:37
        /// 备注：
        public readonly string ConnectString;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectString">连接字符串</param>
        /// <param name="backUpDataBase">需要备份的数据库</param>
        /// 时间：2016-02-24 17:46
        /// 备注：
        public SqlServerDbBackUp(string connectString, string backUpDataBase)
        {
            ValidateHelper.Begin().NotNullOrEmpty(connectString, "Sql Server连接字符串").NotNullOrEmpty(backUpDataBase, "需要备份数据库");
            ConnectString = connectString;
            BackUpDataBase = backUpDataBase;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// 备份数据库
        /// </summary>
        /// <param name="filePath">备份路径，需带后缀;eg:D:\ddd.bakRM_DB20160224151425.bak</param>
        /// <returns>备份数据库是否成功</returns>
        /// 时间：2016-02-24 16:44
        /// 备注：
        public bool DataBackup(string filePath)
        {
            ValidateHelper.Begin().IsFilePath(filePath, "Sql Server备份路径");
            bool _result = false;
            //创建连接对象
            SqlServerHelper _sqlHelper = new SqlServerHelper(ConnectString);
            DropBackupDevice(_sqlHelper);
            CreateBackupDevice(_sqlHelper, filePath);

            try
            {
                SqlParameter[] _paramters = new SqlParameter[2];
                _paramters[0] = new SqlParameter("@dataBase", BackUpDataBase);
                _paramters[1] = new SqlParameter("@descdataBase", BackUpDataBase);
                string _sql = "BACKUP DATABASE @dataBase TO @descdataBase WITH INIT";
                _sqlHelper.ExecuteNonQuery(_sql, _paramters);
                _result = false;
            }
            catch (Exception ex)
            {
                ex.Data.Add("filePath", filePath);
                ex.Data.Add("ConnectString", ConnectString);
                ex.Data.Add("BackUpDataBase", BackUpDataBase);
                throw new FrameworkException(string.Format("备份数据库{0}失败。", BackUpDataBase), ex);
            }
            return _result;
        }

        /// <summary>
        /// 还原数据库文件
        /// </summary>
        /// <param name="filePath">还原备份路径</param>
        /// <param name="restoreDbName">还原后数据库名称</param>
        /// <returns>还原是否成功</returns>
        /// 时间：2016-02-24 17:45
        /// 备注：
        public bool DataRestore(string filePath, string restoreDbName)
        {
            ValidateHelper.Begin().IsFilePath(filePath, "Sql Server备份路径").NotNullOrEmpty(restoreDbName, "还原数据库名称");
            bool _result = false;
            SqlServerHelper _sqlHelper = new SqlServerHelper(ConnectString);
            string _sql = @" use master ;
                declare @s varchar(8000);
                select @s=isnull(@s,'')+' kill '+rtrim(spID) from master..sysprocesses where dbid=db_id(@dbid);
                select @s;exec(@s) ;RESTORE DATABASE @database FROM DISK = @filepath with replace";
            SqlParameter[] _paramters = new SqlParameter[3];
            _paramters[0] = new SqlParameter("@dbid", restoreDbName);
            _paramters[1] = new SqlParameter("@database", restoreDbName);
            _paramters[2] = new SqlParameter("@filepath", filePath);
            try
            {
                _sqlHelper.ExecuteNonQuery(_sql, _paramters);
            }
            catch (Exception ex)
            {
                ex.Data.Add("filePath", filePath);
                ex.Data.Add("ConnectString", ConnectString);
                ex.Data.Add("BackUpDataBase", BackUpDataBase);
                throw new FrameworkException(string.Format("还原数据库{0}失败，可能该数据库正在使用。", restoreDbName), ex);
            }
            return _result;
        }

        /// <summary>
        /// 创建数据库的备份逻辑设备
        /// </summary>
        /// <param name="sqlHelper">SqlServerHelper</param>
        /// <param name="filePath">备份路径</param>
        /// 时间：2016-02-24 16:05
        /// 备注：
        private void CreateBackupDevice(SqlServerHelper sqlHelper, string filePath)
        {
            try
            {
                SqlParameter[] _paramters = new SqlParameter[3];
                _paramters[0] = new SqlParameter("@devtype", "disk");
                _paramters[1] = new SqlParameter("@logicalname", BackUpDataBase);
                _paramters[2] = new SqlParameter("@physicalname", filePath);
                sqlHelper.StoreExecuteNonQuery("sp_addumpdevice", _paramters);
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// 删除已有的备份设备
        /// </summary>
        /// <param name="sqlHelper">SqlServerHelper</param>
        /// 时间：2016-02-24 16:00
        /// 备注：
        private void DropBackupDevice(SqlServerHelper sqlHelper)
        {
            try
            {
                SqlParameter[] _paramters = new SqlParameter[1];
                _paramters[0] = new SqlParameter("@logicalname", BackUpDataBase);
                sqlHelper.StoreExecuteNonQuery("sp_dropdevice", _paramters);//删除备份设备
            }
            catch (Exception)
            {
            }
        }

        #endregion Methods
    }
}