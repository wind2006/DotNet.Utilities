namespace YanZhiwei.DotNet2.Utilities.WinForm
{
    using System.ComponentModel;
    using System.Reflection;
    using System.Windows.Forms;

    /// <summary>
    ///Control帮助类
    /// </summary>
    public static class ControlHelper
    {
        #region Methods

        /// <summary>
        /// 向容器控件中添加控件
        /// </summary>
        /// <typeparam name="C">泛型</typeparam>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="c">容器控件</param>
        /// <param name="t">添加控件</param>
        public static void AddControl<C, T>(this C c, T t)
            where C : Control
            where T : Control, new()
        {
            if (t == null)
                t = new T();
            t.Dock = DockStyle.Fill;
            c.Controls.Clear();
            c.Controls.Add(t);
        }

        /// <summary>
        /// 将Control转换某种控件类型
        /// </summary>
        /// <typeparam name="T">控件类型</typeparam>
        /// <param name="control">Control</param>
        /// <param name="result">转换结果</param>
        /// <returns>若成功则返回控件；若失败则返回NULL</returns>
        public static T Cast<T>(this Control control, out bool result)
            where T : Control
        {
            result = false;
            T _castCtrl = null;
            if (control != null)
            {
                if (control is T)
                {
                    _castCtrl = control as T;
                    result = true;
                }
            }
            return _castCtrl;
        }

        /// <summary>
        /// 向下递归查找控件
        /// </summary>
        /// <param name="parentControl">查找控件的父容器控件</param>
        /// <param name="findCtrlName">查找控件名称</param>
        /// <returns>若没有查找到返回NULL</returns>
        public static Control DownRecursiveFindControl<T>(this Control parentControl, string findCtrlName)
            where T : Control
        {
            Control _findedControl = null;
            if (!string.IsNullOrEmpty(findCtrlName) && parentControl != null)
            {
                foreach (Control ctrl in parentControl.Controls)
                {
                    if ((ctrl is T) && string.Compare(ctrl.Name, findCtrlName) == 0)
                    {
                        _findedControl = ctrl;
                        break;
                    }
                    else
                    {
                        if (ctrl.Controls.Count > 0 && _findedControl == null)
                            _findedControl = DownRecursiveFindControl<T>(ctrl, findCtrlName);
                    }
                }
            }
            return _findedControl;
        }

        /// <summary>
        /// 移除控件某个事件
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="eventName">需要移除的控件名称eg:EventClick</param>
        public static void RemoveEvent(this Control control, string eventName)
        {
            FieldInfo _fl = typeof(Control).GetField(eventName, BindingFlags.Static | BindingFlags.NonPublic);
            if (_fl != null)
            {
                object _obj = _fl.GetValue(control);
                PropertyInfo _pi = control.GetType().GetProperty("Events", BindingFlags.NonPublic | BindingFlags.Instance);
                EventHandlerList _eventlist = (EventHandlerList)_pi.GetValue(control, null);
                if (_obj != null && _eventlist != null)
                    _eventlist.RemoveHandler(_obj, _eventlist[_obj]);
            }
        }

        #endregion Methods
    }
}