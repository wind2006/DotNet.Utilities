namespace YanZhiwei.DotNet2.Utilities.Common
{
    using System;

    /// <summary>
    /// Array 帮助类
    /// </summary>
    public static class ArrayHelper
    {
        #region Methods

        /// <summary>
        /// 向数组中中添加新元素
        /// <para>eg: CollectionAssert.AreEqual(new int[6] { 1, 2, 3, 4, 5, 6 }, ArrayHelper.Add(new int[5] { 1, 2, 3, 4, 5 }, 6));</para>
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="data">The array.</param>
        /// <param name="item">The item.</param>
        /// <returns>数组</returns>
        public static T[] Add<T>(this T[] data, T item)
        {
            int _count = data.Length;
            Array.Resize<T>(ref data, _count + 1);
            data[_count] = item;
            return data;
        }

        /// <summary>
        /// 向数组中添加新数组；
        /// <para>
        /// eg: CollectionAssert.AreEqual(new int[7] { 1, 2, 3, 4, 5, 6, 7 },
        ///     ArrayHelper.AddRange(new int[5] { 1, 2, 3, 4, 5 }, new int[2] { 6, 7 }));
        /// </para>
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="sourceArray">The source array.</param>
        /// <param name="addArray">The add array.</param>
        /// <returns>数组</returns>
        public static T[] AddRange<T>(this T[] sourceArray, T[] addArray)
        {
            int _count = sourceArray.Length;
            int _addCount = addArray.Length;
            Array.Resize<T>(ref sourceArray, _count + _addCount);
            addArray.CopyTo(sourceArray, _count);
            return sourceArray;
        }

        /// <summary>
        /// 清空数组
        /// <para>
        /// eg:
        /// int[] _test = new int[5] { 1, 2, 3, 4, 5 };
        /// _test.ClearAll();
        /// CollectionAssert.AreEqual(new int[5] { 0, 0, 0, 0, 0 }, _test);
        /// </para>
        /// </summary>
        /// <param name="data">数组</param>
        public static void ClearAll(this Array data)
        {
            Array.Clear(data, 0, data.Length);
        }

        /// <summary>
        /// 清除特定索引数据
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="data">数组</param>
        /// <param name="index">需要清除索引</param>
        public static void ClearAt<T>(this T[] data, int index)
        {
            Array.Clear(data, index, 1);
        }

        /// <summary>
        /// 复制数组
        /// <para>
        /// eg: CollectionAssert.AreEqual(new int[3] { 1, 2, 3 }, ArrayHelper.Copy(new int[5] { 1,
        ///     2, 3, 4, 5 }, 0, 3));
        /// </para>
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="data">需要操作数组</param>
        /// <param name="startIndex">复制起始索引，从零开始</param>
        /// <param name="endIndex">复制结束索引，从零开始</param>
        /// <returns>数组</returns>
        public static T[] Copy<T>(this T[] data, int startIndex, int endIndex)
        {
            if (startIndex < endIndex)
            {
                int _len = endIndex - startIndex;
                T[] _destination = new T[_len];
                Array.Copy(data, startIndex, _destination, 0, _len);
                return _destination;
            }

            return data;
        }

        /// <summary>
        /// 动态添加
        /// <para>eg:  CollectionAssert.AreEqual(new int[6] { 1, 2, 3, 4, 5, 6 }, ArrayHelper.DynamicAdd(new int[5] { 1, 2, 3, 4, 5 }, 6));</para>
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="target">目标数组</param>
        /// <param name="item">添加项</param>
        /// <returns>数组</returns>
        /// 时间：2015-09-09 16:59
        /// 备注：
        public static T[] DynamicAdd<T>(this T[] target, T item)
        {
            if (target == null)
            {
                return target;
            }

            T[] result = new T[target.Length + 1];
            target.CopyTo(result, 0);
            result[target.Length] = item;
            return result;
        }

        /// <summary>
        /// 判断数组的值是否相等
        /// <para> eg: Assert.IsTrue(ArrayHelper.EqualValue(new int[5] { 1, 2, 3, 4, 5 }, new int[5] { 1, 2, 3, 4, 5 }));
        /// </para>
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="source">数组一</param>
        /// <param name="compare">数组二</param>
        /// <returns>是否相等</returns>
        public static bool EqualValue<T>(this T[] source, T[] compare)
        {
            if (source == null || compare == null)
            {
                return false;
            }

            if (source.Length != compare.Length)
            {
                return false;
            }

            for (int i = 0; i < source.Length; i++)
            {
                if (!source[i].Equals(compare[i]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 判断数组是否是空还是NULL
        /// <para>eg:Assert.IsTrue(ArrayHelper.IsNullOrEmpty(new int[0]));</para>
        /// </summary>
        /// <param name="data">数组</param>
        /// <returns>是否是空或者NULL</returns>
        public static bool IsNullOrEmpty(this Array data)
        {
            if (data == null || data.Length == 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 引用类型数组值比较
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="array1">数组一.</param>
        /// <param name="array2">数组二</param>
        /// <returns>值是否相等</returns>
        public static bool ReferenceTypeValueEqual<T>(this T[] array1, T[] array2)
            where T : class, IComparable<T>
        {
            bool _resut = false;
            if (array1 != null && array2 != null)
            {
                _resut = array1.Length == array2.Length;
                if (_resut)
                {
                    array1.BubbleSort<T>();
                    array2.BubbleSort<T>();
                    int _length = array1.Length;
                    for (int i = 0; i < _length; i++)
                    {
                        _resut = EntityHelper.ValueEqual(array1[i], array2[i]);
                        if (!_resut)
                        {
                            break;
                        }
                    }
                }
            }

            return _resut;
        }

        /// <summary>
        /// 重新设置数组大小
        /// <para>eg: CollectionAssert.AreEqual(new int[5] { 1, 2, 3, 0, 0 }, ArrayHelper.Resize(new int[3] { 1, 2, 3 }, 5)); </para>
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="data">需要设置的数组</param>
        /// <param name="targetNumber">目标数组大小</param>
        /// <returns>数组</returns>
        public static T[] Resize<T>(this T[] data, int targetNumber)
        {
            if (data != null)
            {
                int _nowLength = data.Length;
                if (_nowLength < targetNumber)
                {
                    T[] _completeArray = new T[targetNumber];
                    data.CopyTo(_completeArray, 0);
                    return _completeArray;
                }

                return data;
            }
            else
            {
                return new T[targetNumber];
            }
        }

        /// <summary>
        /// 给定索引是否在数组索引内
        /// <para>
        /// int[] _test = new int[5] { 1, 2, 3, 4, 5 };
        /// Assert.IsFalse(_test.WithinIndex(5));
        /// Assert.IsTrue(_test.WithinIndex(0));
        /// Assert.IsTrue(_test.WithinIndex(4));
        /// </para>
        /// </summary>
        /// <param name="data">数组</param>
        /// <param name="index">索引</param>
        /// <returns>是否在范围内</returns>
        public static bool WithinIndex(this Array data, int index)
        {
            return index >= 0 && index < data.Length;
        }

        #endregion Methods
    }
}