using DevExpress.XtraGrid.Localization;
using System.Collections.Generic;

namespace YanZhiwei.DotNet.DevExpress12._1.Utilities.Core
{
    /// <summary>
    /// GridLocalizer帮助类
    /// </summary>
    public class BuilderGridLocalizer : GridLocalizer
    {
        #region 带参数的构造函数以及变量

        private Dictionary<GridStringId, string> CusLocalizedKeyValue = null;

        /// <summary>
        /// 带参数的构造函数
        /// </summary>
        /// <param name="cusLocalizedKeyValue">需要转移的GridStringId，其对应的文字描述</param>
        public BuilderGridLocalizer(Dictionary<GridStringId, string> cusLocalizedKeyValue)
        {
            CusLocalizedKeyValue = cusLocalizedKeyValue;
        }

        #endregion 带参数的构造函数以及变量

        #region GetLocalizedString重载

        /// <summary>
        /// GetLocalizedString重载
        /// </summary>
        /// <param name="id">GridStringId</param>
        /// <returns>string</returns>
        public override string GetLocalizedString(GridStringId id)
        {
            if (CusLocalizedKeyValue != null)
            {
                string _gridStringDisplay = string.Empty;
                foreach (KeyValuePair<GridStringId, string> gridLocalizer in CusLocalizedKeyValue)
                {
                    if (gridLocalizer.Key.Equals(id))
                    {
                        _gridStringDisplay = gridLocalizer.Value;
                        break;
                    }
                }
                return _gridStringDisplay;
            }
            return base.GetLocalizedString(id);
        }

        #endregion GetLocalizedString重载
    }
}