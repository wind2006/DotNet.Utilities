namespace YanZhiwei.DotNet.DevExpress12._1.Utilities
{
    using System.Collections;

    using DevExpress.XtraEditors.Repository;

    /// <summary>
    /// RepositoryItemComboBox帮助类
    /// </summary>
    public static class RepositoryItemComboBoxHelper
    {
        #region Methods

        /// <summary>
        /// 数据绑定
        /// </summary>
        /// <param name="combox">RepositoryItemComboBox</param>
        /// <param name="source">ICollection</param>
        public static void SetDataSource(this RepositoryItemComboBox combox, ICollection source)
        {
            /*说明：
             *所涉及的列叙设定FieldName，否则会出现无法选中的问题；
             *eg:
             *List<PersonInfo> _source = new List<PersonInfo>();
             *_source.Add(new PersonInfo("Sven", "Petersen"));
             *_source.Add(new PersonInfo("Cheryl", "Saylor"));
             *_source.Add(new PersonInfo("Dirk", "Luchte"));
             *repositoryItemComboBox1.SetDataSource<PersonInfo>(_source);
             */
            if (source != null)
            {
                try
                {
                    combox.BeginUpdate();
                    combox.Items.AddRange(source);
                    combox.ParseEditValue += (sender, e) =>
                    {
                        e.Value = e.Value.ToString();
                        e.Handled = true;
                    };
                }
                finally
                {
                    combox.EndUpdate();
                }
            }
        }

        #endregion Methods
    }
}