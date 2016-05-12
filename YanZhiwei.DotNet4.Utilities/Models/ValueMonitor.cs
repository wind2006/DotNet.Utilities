using System.Collections.Generic;

namespace YanZhiwei.DotNet4.Utilities.Models
{
    /// <summary>
    /// 数值改变委托
    /// </summary>
    /// <typeparam name="ValueType">The type of the alue type.</typeparam>
    /// <param name="oldValue">The old value.</param>
    /// <param name="newValue">The new value.</param>
    public delegate void ValueChangedDelegate<ValueType>(ValueType oldValue, ValueType newValue);

    /// <summary>
    /// 数值改变 接口
    /// </summary>
    /// <typeparam name="ValueType">The type of the alue type.</typeparam>
    public interface IValueMonitor<ValueType>
    {
        /// <summary>
        /// Gets the value.
        /// </summary>
        ValueType Value { get; }

        /// <summary>
        /// Occurs when [value changed].
        /// </summary>
        event ValueChangedDelegate<ValueType> ValueChanged;
    }

    /// <summary>
    /// 监视数值变化
    /// <para>1.通过SET方式设置数值，将触发事件</para>
    /// <para>2.通过SetQuietly方法设置数值，将不会触发事件</para>
    /// </summary>
    /// <typeparam name="ValueType">The type of the alue type.</typeparam>
    public class ValueMonitor<ValueType> : IValueMonitor<ValueType>
    {
        private ValueType aValue = default(ValueType);
        private IEqualityComparer<ValueType> comparer = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="initialValue">The initial value.</param>
        public ValueMonitor(ValueType initialValue)
        {
            aValue = initialValue;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="initialValue">The initial value.</param>
        /// <param name="comparator">The comparator.</param>
        public ValueMonitor(ValueType initialValue, IEqualityComparer<ValueType> comparator)
        {
            aValue = initialValue;
            comparer = comparator;
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public ValueType Value
        {
            get { return aValue; }
            set
            {
                bool areEqual = false;
                if (comparer == null)
                    areEqual = (aValue.Equals(value));
                else areEqual = comparer.Equals(aValue, value);

                if (areEqual == true) return;
                ValueType oldValue = aValue; // remember previous for the event rising
                aValue = value;
                if (ValueChanged != null)
                    ValueChanged(oldValue, aValue);
            }
        }

        /// <summary>
        /// 设置值将会不会触发事件
        /// </summary>
        /// <param name="newValue"></param>
        public void SetQuietly(ValueType newValue)
        {
            aValue = newValue;
        }

        /// <summary>
        ///值改变事件
        /// </summary>
        public event ValueChangedDelegate<ValueType> ValueChanged;
    }
}