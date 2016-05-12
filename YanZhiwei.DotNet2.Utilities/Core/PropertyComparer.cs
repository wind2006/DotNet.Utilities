namespace YanZhiwei.DotNet2.Utilities.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Reflection;

    /// <summary>
    /// 属性比较
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    internal class PropertyComparer<T> : IComparer<T>
    {
        #region Fields

        /// <summary>
        /// The direction
        /// </summary>
        private ListSortDirection direction;

        /// <summary>
        /// The property
        /// </summary>
        private PropertyDescriptor property;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyComparer{T}"/> class.
        /// </summary>
        /// <param name="pperty">The pperty.</param>
        /// <param name="pdirection">The pdirection.</param>
        public PropertyComparer(PropertyDescriptor pperty, ListSortDirection pdirection)
        {
            property = pperty;
            direction = pdirection;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// 比较两个对象并返回一个值，该值指示一个对象小于、等于还是大于另一个对象。
        /// </summary>
        /// <param name="objA">The object a.</param>
        /// <param name="objB">The object b.</param>
        /// <returns>int</returns>
        public int Compare(T objA, T objB)
        {
            object _valueX = GetPropertyValue(objA, property.Name);
            object _valueY = GetPropertyValue(objB, property.Name);

            if (direction == ListSortDirection.Ascending)
            {
                return CompareAscending(_valueX, _valueY);
            }
            else
            {
                return CompareDescending(_valueX, _valueY);
            }
        }

        /// <summary>
        /// Equalses the specified object a.
        /// </summary>
        /// <param name="objA">The object a.</param>
        /// <param name="objB">The object b.</param>
        /// <returns>是否相等</returns>
        public bool Equals(T objA, T objB)
        {
            return objA.Equals(objB);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public int GetHashCode(T obj)
        {
            return obj.GetHashCode();
        }

        /// <summary>
        /// Sets the direction.
        /// </summary>
        /// <param name="sortDirection">The sort direction.</param>
        public void SetDirection(ListSortDirection sortDirection)
        {
            direction = sortDirection;
        }

        /// <summary>
        /// Compares the ascending.
        /// </summary>
        /// <param name="objA">The object a.</param>
        /// <param name="objB">The object b.</param>
        /// <returns>int</returns>
        private int CompareAscending(object objA, object objB)
        {
            int _result;
            if (objA is IComparable)
            {
                _result = ((IComparable)objA).CompareTo(objB);
            }
            else if (objA.Equals(objB))
            {
                _result = 0;
            }
            else
            {
                _result = ((IComparable)objA).CompareTo(objB);
            }

            return _result;
        }

        /// <summary>
        /// Compares the descending.
        /// </summary>
        /// <param name="objA">The object a.</param>
        /// <param name="objB">The object b.</param>
        /// <returns>int</returns>
        private int CompareDescending(object objA, object objB)
        {
            // Return result adjusted for ascending or descending sort order ie
            // multiplied by 1 for ascending or -1 for descending
            return -CompareAscending(objA, objB);
        }

        /// <summary>
        /// Gets the property value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="property">The property.</param>
        /// <returns>object</returns>
        private object GetPropertyValue(T value, string property)
        {
            // Get property
            PropertyInfo propertyInfo = value.GetType().GetProperty(property);

            // Return value
            return propertyInfo.GetValue(value, null);
        }

        #endregion Methods
    }
}