namespace YanZhiwei.DotNet3._5.Utilities.WPF
{
    using System;
    using System.Windows.Threading;

    /// <summary>
    /// Dispatcher帮助类
    /// 参考
    /// http://blog.decarufel.net/2009/03/good-practice-to-use-dispatcher-in-wpf-background-thread.html
    /// http://msdn.microsoft.com/zh-tw/magazine/cc163328.aspx
    /// </summary>
    public static class DispatcherHelper
    {
        #region Methods

        /// <summary>
        /// Dispatch
        /// </summary>
        public static TResult Dispatch<TResult>(this DispatcherObject source, Func<TResult> func)
        {
            if (source.Dispatcher.CheckAccess())
                return func();
            return (TResult)source.Dispatcher.Invoke(func);
        }

        /// <summary>
        /// Dispatch
        /// </summary>
        public static TResult Dispatch<T, TResult>(this T source, Func<T, TResult> func)
            where T : DispatcherObject
        {
            if (source.Dispatcher.CheckAccess())
                return func(source);

            return (TResult)source.Dispatcher.Invoke(func, source);
        }

        /// <summary>
        /// Dispatch
        /// </summary>
        public static TResult Dispatch<TSource, T, TResult>(this TSource source, Func<TSource, T, TResult> func, T param1)
            where TSource : DispatcherObject
        {
            if (source.Dispatcher.CheckAccess())
                return func(source, param1);

            return (TResult)source.Dispatcher.Invoke(func, source, param1);
        }

        /// <summary>
        /// Dispatch
        /// </summary>
        public static TResult Dispatch<TSource, T1, T2, TResult>(this TSource source, Func<TSource, T1, T2, TResult> func, T1 param1, T2 param2)
            where TSource : DispatcherObject
        {
            if (source.Dispatcher.CheckAccess())
                return func(source, param1, param2);

            return (TResult)source.Dispatcher.Invoke(func, source, param1, param2);
        }

        /// <summary>
        /// Dispatch
        /// </summary>
        public static TResult Dispatch<TSource, T1, T2, T3, TResult>(this TSource source, Func<TSource, T1, T2, T3, TResult> func, T1 param1, T2 param2, T3 param3)
            where TSource : DispatcherObject
        {
            if (source.Dispatcher.CheckAccess())
                return func(source, param1, param2, param3);

            return (TResult)source.Dispatcher.Invoke(func, source, param1, param2, param3);
        }

        /// <summary>
        /// Dispatch
        /// </summary>
        public static void Dispatch(this DispatcherObject source, Action func)
        {
            if (source.Dispatcher.CheckAccess())
                func();
            else
                source.Dispatcher.Invoke(func);
        }

        /// <summary>
        /// Dispatch
        /// </summary>
        public static void Dispatch<TSource>(this TSource source, Action<TSource> func)
            where TSource : DispatcherObject
        {
            if (source.Dispatcher.CheckAccess())
                func(source);
            else
                source.Dispatcher.Invoke(func, source);
        }

        /// <summary>
        /// Dispatch
        /// </summary>
        public static void Dispatch<TSource, T1>(this TSource source, Action<TSource, T1> func, T1 param1)
            where TSource : DispatcherObject
        {
            if (source.Dispatcher.CheckAccess())
                func(source, param1);
            else
                source.Dispatcher.Invoke(func, source, param1);
        }

        /// <summary>
        /// Dispatch
        /// </summary>
        public static void Dispatch<TSource, T1, T2>(this TSource source, Action<TSource, T1, T2> func, T1 param1, T2 param2)
            where TSource : DispatcherObject
        {
            if (source.Dispatcher.CheckAccess())
                func(source, param1, param2);
            else
                source.Dispatcher.Invoke(func, source, param1, param2);
        }

        /// <summary>
        /// Dispatch
        /// </summary>
        public static void Dispatch<TSource, T1, T2, T3>(this TSource source, Action<TSource, T1, T2, T3> func,
            T1 param1, T2 param2, T3 param3)
            where TSource : DispatcherObject
        {
            if (source.Dispatcher.CheckAccess())
                func(source, param1, param2, param3);
            else
                source.Dispatcher.Invoke(func, source, param1, param2, param3);
        }

        #endregion Methods
    }
}