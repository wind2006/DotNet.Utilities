namespace YanZhiwei.DotNet3._5.Utilities.Core
{
    using System.ComponentModel;

    /// <summary>
    /// 向客户端发出某一属性值已更改的通知。
    /// </summary>
    public class NotifyPropertyChanged : INotifyPropertyChanged
    {
        #region Events

        /// <summary>
        /// 向客户端发出某一属性值已更改的通知事件
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Methods

        /// <summary>
        /// 需要向客户端通知的属性名称
        /// </summary>
        /// <param name="propertyName">属性名称</param>
        public void NotifyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion Methods
    }
}