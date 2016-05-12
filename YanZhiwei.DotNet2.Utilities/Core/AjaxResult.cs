namespace YanZhiwei.DotNet2.Utilities.Core
{
    using YanZhiwei.DotNet2.Utilities.Enums;

    /// <summary>
    /// 表示Ajax操作结果
    /// </summary>
    public class AjaxResult
    {
        #region Constructors

        /// <summary>
        /// 初始化一个<see cref="AjaxResult"/>类型的新实例
        /// </summary>
        public AjaxResult(string content, AjaxResultType type, object data)
            : this(content, data, type)
        {
        }

        /// <summary>
        /// 初始化一个<see cref="AjaxResult"/>类型的新实例
        /// </summary>
        /// <param name="content"></param>
        public AjaxResult(string content)
            : this(content, AjaxResultType.Info, null)
        {
        }

        /// <summary>
        /// 初始化一个<see cref="AjaxResult"/>类型的新实例
        /// </summary>
        public AjaxResult(string content, object data, AjaxResultType type)
        {
            Type = type.ToString();
            Content = content;
            Data = data;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// 获取 消息内容
        /// </summary>
        public string Content
        {
            get; private set;
        }

        /// <summary>
        /// 获取 返回数据
        /// </summary>
        public object Data
        {
            get; private set;
        }

        /// <summary>
        /// 获取 Ajax操作结果类型
        /// </summary>
        public string Type
        {
            get; private set;
        }

        #endregion Properties
    }
}