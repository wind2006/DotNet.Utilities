namespace YanZhiwei.DotNet.Framework.Contract
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Remoting.Messaging;

    using YanZhiwei.DotNet2.Utilities.Common;
    using YanZhiwei.DotNet3._5.Utilities.Common;

    /// <summary>
    /// 服务调用上下文，主要传输IP，操作人，认证等数据，可用于WCF传输用
    /// </summary>
    [Serializable]
    public class ServiceCallContext : Dictionary<string, object>
    {
        #region Fields

        private const string callContextKey = "__CallContext";

        #endregion Fields

        #region Properties

        /*
        CallContext 是类似于方法调用的线程本地存储区的专用集合对象，并提供对每个逻辑执行线程都唯一的数据槽。数据槽不在其他逻辑线程上的调用上下文之间共享。
        当 CallContext 沿执行代码路径往返传播并且由该路径中的各个对象检查时，可将对象添加到其中。

        也就是说，当前线程对对象进行储存到线程本地储存区，对象随着线程的销毁而销毁。

        使用场景：我个人认为，当对象需要线程内全局使用，而其他线程包扩子线程都不能访问的时候使用。
        比如EF的数据上下文，每次请求都会生成一个线程处理请求，这时候创建一个数据上下文对象给不同的函数使用，最后一起提交就完全可以避免事务的问题。
        当然也许有人会问我可以创建一个变量来使用，同样可以达到一样的目的，这当然也是可以的，只是这个对象你也是可以和其他线程数据进行交互的，这就违背了线程内唯一的概念了。
        */
        /// <summary>
        /// ServiceCallContext 数据槽
        /// </summary>
        public static ServiceCallContext Current
        {
            get
            {
                if (CallContext.GetData(callContextKey) == null)
                {
                    CallContext.SetData(callContextKey, new ServiceCallContext());
                }

                return CallContext.GetData(callContextKey) as ServiceCallContext;
            }
            set
            {
                CallContext.SetData(callContextKey, value);
            }
        }

        /// <summary>
        /// 操作者
        /// </summary>
        public Operater Operater
        {
            get
            {
                return SerializationHelper.JsonDeserialize<Operater>(this["__Operater"].ToString());
            }
            set
            {
                this["__Operater"] = SerializationHelper.JsonSerialize<Operater>(value);
            }
        }

        #endregion Properties

        #region Indexers

        /// <summary>
        /// 索引器
        /// </summary>
        /// <value>
        /// The <see cref="System.Object"/>.
        /// </value>
        /// <param name="key">索引值</param>
        /// <returns>object</returns>
        public new object this[string key]
        {
            get
            {
                return base[key];
            }
            set
            {
                ValidateHelper.Begin().NotNull(value, "索引值").IsSerializable(value);
                base[key] = value;
            }
        }

        #endregion Indexers
    }
}