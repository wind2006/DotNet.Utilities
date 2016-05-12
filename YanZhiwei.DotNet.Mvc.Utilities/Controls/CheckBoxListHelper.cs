namespace YanZhiwei.DotNet.Mvc.Utilities.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Text;
    using System.Web.Mvc;

    /// <summary>
    /// CheckBoxList 扩展
    /// </summary>
    /// 时间：2016-01-26 14:30
    /// 备注：
    public static class CheckBoxListHelper
    {
        #region Methods

        /// <summary>
        /// CheckBoxList
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="name">名称</param>
        /// <param name="isHorizon">是否水平排列</param>
        /// <returns>MvcHtmlString</returns>
        /// 时间：2016-01-26 14:31
        /// 备注：
        public static MvcHtmlString CheckBoxList(this HtmlHelper helper, string name, bool isHorizon = true)
        {
            return CheckBoxList(helper, name, helper.ViewData[name] as IEnumerable<SelectListItem>, new { }, isHorizon);
        }

        /// <summary>
        /// CheckBoxList
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="name">名称</param>
        /// <param name="selectList">集合</param>
        /// <param name="isHorizon">是否水平排列</param>
        /// <returns>MvcHtmlString</returns>
        /// 时间：2016-01-26 14:31
        /// 备注：
        public static MvcHtmlString CheckBoxList(this HtmlHelper helper, string name, IEnumerable<SelectListItem> selectList, bool isHorizon = true)
        {
            return CheckBoxList(helper, name, selectList, new { }, isHorizon);
        }

        /// <summary>
        /// CheckBoxList
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="name">名称</param>
        /// <param name="selectList">集合</param>
        /// <param name="htmlAttributes">Html 属性</param>
        /// <param name="isHorizon">是否水平排列</param>
        /// <returns>MvcHtmlString</returns>
        /// 时间：2016-01-26 14:31
        /// 备注：
        public static MvcHtmlString CheckBoxList(this HtmlHelper helper, string name, IEnumerable<SelectListItem> selectList, object htmlAttributes, bool isHorizon = true)
        {
            return CheckBoxList(helper, name, name, selectList, htmlAttributes, isHorizon);
        }

        /// <summary>
        /// CheckBoxList
        /// </summary>
        /// <param name="helper">HtmlHelper</param>
        /// <param name="id">Id</param>
        /// <param name="name">名称</param>
        /// <param name="selectList">集合</param>
        /// <param name="htmlAttributes">Html 属性</param>
        /// <param name="isHorizon">是否水平排列</param>
        /// <returns>MvcHtmlString</returns>
        /// 时间：2016-01-26 14:31
        /// 备注：
        public static MvcHtmlString CheckBoxList(this HtmlHelper helper, string id, string name, IEnumerable<SelectListItem> selectList, object htmlAttributes, bool isHorizon = true)
        {
            IDictionary<string, object> _htmlAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            HashSet<string> _set = new HashSet<string>();
            List<SelectListItem> _list = new List<SelectListItem>();
            string _selectedValues = (selectList as SelectList).SelectedValue == null ? string.Empty : Convert.ToString((selectList as SelectList).SelectedValue);
            if (!string.IsNullOrEmpty(_selectedValues))
            {
                if (_selectedValues.Contains(","))
                {
                    string[] tempStr = _selectedValues.Split(',');
                    for (int i = 0; i < tempStr.Length; i++)
                    {
                        _set.Add(tempStr[i].Trim());
                    }
                }
                else
                {
                    _set.Add(_selectedValues);
                }
            }

            foreach (SelectListItem item in selectList)
            {
                item.Selected = (item.Value != null) ? _set.Contains(item.Value) : _set.Contains(item.Text);
                _list.Add(item);
            }
            selectList = _list;

            _htmlAttributes.Add("type", "checkbox");
            _htmlAttributes.Add("id", id);
            _htmlAttributes.Add("name", name);
            _htmlAttributes.Add("style", "border:none;");

            StringBuilder _builder = new StringBuilder();

            foreach (SelectListItem selectItem in selectList)
            {
                IDictionary<string, object> _newHtmlAttributes = _htmlAttributes.DeepCopy();
                _newHtmlAttributes.Add("value", selectItem.Value);
                if (selectItem.Selected)
                {
                    _newHtmlAttributes.Add("checked", "checked");
                }

                TagBuilder _tagBuilder = new TagBuilder("input");
                _tagBuilder.MergeAttributes<string, object>(_newHtmlAttributes);
                string _inputAllHtml = _tagBuilder.ToString(TagRenderMode.SelfClosing);
                string _containerFormat = isHorizon ? @"<label> {0}  {1}</label>" : @"<p><label> {0}  {1}</label></p>";
                _builder.AppendFormat(_containerFormat,
                   _inputAllHtml, selectItem.Text);
            }
            return MvcHtmlString.Create(_builder.ToString());
        }

        /// <summary>
        /// CheckBoxes the list for.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="helper">The helper.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="selectList">The select list.</param>
        /// <param name="isHorizon">if set to <c>true</c> [is horizon].</param>
        /// <returns>MvcHtmlString</returns>
        /// 时间：2016-01-26 14:31
        /// 备注：
        public static MvcHtmlString CheckBoxListFor<TModel, TProperty>(this HtmlHelper helper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, bool isHorizon = true)
        {
            string[] _propertys = expression.ToString().Split(".".ToCharArray());
            string _id = string.Join("_", _propertys, 1, _propertys.Length - 1);
            string _name = string.Join(".", _propertys, 1, _propertys.Length - 1);

            return CheckBoxList(helper, _id, _name, selectList, new { }, isHorizon);
        }

        private static IDictionary<string, object> DeepCopy(this IDictionary<string, object> ht)
        {
            Dictionary<string, object> _ht = new Dictionary<string, object>();

            foreach (var p in ht)
            {
                _ht.Add(p.Key, p.Value);
            }
            return _ht;
        }

        #endregion Methods
    }
}