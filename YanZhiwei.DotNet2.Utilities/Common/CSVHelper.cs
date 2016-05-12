namespace YanZhiwei.DotNet2.Utilities.Common
{
    using System.Data;
    using System.IO;
    using System.Text;

    /// <summary>
    /// CSV 帮助类
    /// </summary>
    public static class CSVHelper
    {
        #region Methods

        /// <summary>
        /// 将CSV文件导入到DataTable
        /// eg:CSVHelper.ImportToTable(_personInfoView, @"C:\Users\YanZh_000\Downloads\person.csv", 2);
        /// </summary>
        /// <param name="table">DataTable</param>
        /// <param name="filePath">csv文件物理路径</param>
        /// <param name="startRowIndex">数据导入起始行号</param>
        /// <returns>DataTable</returns>
        public static DataTable ImportToTable(this DataTable table, string filePath, int startRowIndex)
        {
            using (StreamReader reader = new StreamReader(filePath, Encoding.UTF8, false))
            {
                int j = 0;
                while (reader.Peek() > -1)
                {
                    j = j + 1;
                    string _line = reader.ReadLine();
                    if (j >= startRowIndex + 1)
                    {
                        string[] _dataArray = _line.Split(',');
                        DataRow _dataRow = table.NewRow();
                        for (int k = 0; k < table.Columns.Count; k++)
                        {
                            _dataRow[k] = _dataArray[k];
                        }
                        table.Rows.Add(_dataRow);
                    }
                }
                return table;
            }
        }

        /// <summary>
        /// 导出到csv文件
        /// eg:
        /// CSVHelper.ToCSV(_personInfoView, @"C:\Users\YanZh_000\Downloads\person.csv", "用户信息表", "名称,年龄");
        /// </summary>
        /// <param name="table">DataTable</param>
        /// <param name="filePath">导出路径</param>
        /// <param name="tableheader">标题</param>
        /// <param name="columname">列名称，以','英文逗号分隔</param>
        /// <returns>是否导出成功</returns>
        public static bool ToCSV(this DataTable table, string filePath, string tableheader, string columname)
        {
            try
            {
                if (File.Exists(filePath))
                    File.Delete(filePath);
                using (FileStream _stream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite))
                {
                    StreamWriter _writer = new StreamWriter(_stream, Encoding.UTF8);
                    _writer.WriteLine(tableheader);
                    _writer.WriteLine(columname);
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        for (int j = 0; j < table.Columns.Count; j++)
                        {
                            _writer.Write(table.Rows[i][j].ToString());
                            _writer.Write(",");
                        }
                        _writer.WriteLine();
                    }
                    _writer.Close();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        #endregion Methods
    }
}