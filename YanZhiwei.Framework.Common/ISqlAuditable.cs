using System;

namespace YanZhiwei.DotNet.Framework.Contract
{
    /// <summary>
    /// 基于Sql数据拦截接口
    /// </summary>
    /// 时间：2016-03-30 11:12
    /// 备注：
    public interface ISqlAuditable
    {
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="tableName">涉及表名称</param>
        /// <param name="sql">添加，删除，修改拦截到sql语句</param>
        /// <param name="optTime">操作时间</param>
        /// 时间：2016-03-30 11:11
        /// 备注：
        void WriteLog(int userId, string tableName, string sql, DateTime optTime);
    }
}