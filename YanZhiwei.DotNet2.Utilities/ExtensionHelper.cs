#if RUNNING_ON_2

//参考:http://stackoverflow.com/questions/3436526/detect-target-framework-version-at-compile-time
namespace System.Runtime.CompilerServices
{
    /// <summary>
    /// ExtensionAttribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class | AttributeTargets.Assembly)]
    public sealed class ExtensionAttribute : Attribute
    {
    }
}

#endif


#if RUNNING_ON_2

namespace System
{
    #region Delegates

    /// <summary>
    /// Action委托
    /// </summary>
    public delegate void Action();

    /////// <summary>
    /////// Action委托
    /////// </summary>
    //public delegate void Action<T>(T t);
    /// <summary>
    /// Action委托
    /// </summary>
    public delegate void Action<T, U>(T t, U u);

    /// <summary>
    /// Action委托
    /// </summary>
    public delegate void Action<T, U, V>(T t, U u, V v);

    /// <summary>
    /// Action委托
    /// </summary>
    public delegate TResult Func<TResult>();

    /// <summary>
    /// Action委托
    /// </summary>
    public delegate TResult Func<T, TResult>(T t);

    /// <summary>
    /// Action委托
    /// </summary>
    public delegate TResult Func<T, U, TResult>(T t, U u);

    /// <summary>
    /// Action委托
    /// </summary>
    public delegate TResult Func<T, U, V, TResult>(T t, U u, V v);

    #endregion Delegates
}

#endif