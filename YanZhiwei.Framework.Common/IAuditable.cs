namespace YanZhiwei.DotNet.Framework.Contract
{
    /// <summary>
    /// 用于写数据修改，添加等历史日志
    /// </summary>
    public interface IAuditable
    {
        #region Methods

        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="modelId">模块Id</param>
        /// <param name="userName">用户名称</param>
        /// <param name="moduleName">模块名称</param>
        /// <param name="tableName">表名称</param>
        /// <param name="eventType">事件类型</param>
        /// <param name="newValues">新的数值</param>
        /// 时间：2016-01-06 16:47
        /// 备注：
        void WriteLog(int modelId, string userName, string moduleName, string tableName, string eventType, ModelBase newValues);

        #endregion Methods
    }
}