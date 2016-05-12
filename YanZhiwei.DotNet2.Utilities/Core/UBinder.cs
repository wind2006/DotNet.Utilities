namespace YanZhiwei.DotNet2.Utilities.Core
{
    using System;
    using System.Reflection;
    using System.Runtime.Serialization;

    /// <summary>
    /// SerializationBinder重写
    /// </summary>
    public class UBinder : SerializationBinder
    {
        #region Methods

        /// <summary>
        /// 当在派生类中重写时，控制将序列化对象绑定到类型的过程。
        /// </summary>
        /// <param name="assemblyName">指定序列化对象的 <see cref="T:System.Reflection.Assembly" /> 名称。</param>
        /// <param name="typeName">指定序列化对象的 <see cref="T:System.Type" /> 名称。</param>
        /// <returns>
        /// 格式化程序为其创建新实例的对象的类型。
        /// </returns>
        /// 日期：2015-09-17 10:56
        /// 备注：
        public override Type BindToType(string assemblyName, string typeName)
        {
            Assembly _ass = Assembly.GetExecutingAssembly();
            return _ass.GetType(typeName);
        }

        #endregion Methods
    }
}