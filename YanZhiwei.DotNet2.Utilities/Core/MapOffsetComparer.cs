namespace YanZhiwei.DotNet2.Utilities.Core
{
    using System.Collections;

    using YanZhiwei.DotNet2.Utilities.Models;

    /// <summary>
    /// 地图纠偏比较类
    /// </summary>
    public class MapOffsetComparer : IComparer
    {
        #region Methods

        /// <summary>
        /// 比较两个对象并返回一个值，指示一个对象是小于、等于还是大于另一个对象。
        /// </summary>
        /// <param name="x">要比较的第一个对象。</param>
        /// <param name="y">要比较的第二个对象。</param>
        /// <returns>
        /// 值 条件 小于零 <paramref name="x" /> 小于 <paramref name="y" />。 零 <paramref name="x" /> 等于 <paramref name="y" />。 大于零 <paramref name="x" /> 大于 <paramref name="y" />。
        /// </returns>
        public int Compare(object x, object y)
        {
            MapCoord _coord1 = (MapCoord)x, _coord2 = (MapCoord)y;
            int _dlng = _coord1.Lon - _coord2.Lon;
            if (_dlng != 0)
            {
                return _dlng;
            }
            else
            {
                return _coord1.Lat - _coord2.Lat;
            }
        }

        #endregion Methods
    }
}