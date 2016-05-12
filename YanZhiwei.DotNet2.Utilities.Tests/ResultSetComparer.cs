using System.Data;
using System.Linq;

namespace YanZhiwei.DotNet2.Utilities
{
    /// <summary>
    /// 参考：http://www.databasejournal.com/features/mssql/article.php/3844791/Database-Unit-Testing.htm
    /// http://msdn.microsoft.com/en-us/library/windows/hardware/hh439554(v=vs.85).aspx
    /// </summary>
    public class ResultSetComparer
    {
        public static bool AreIdenticalResultSets(DataTable dt1, DataTable dt2)
        {
            var v1 = dt1.AsEnumerable();
            var v2 = dt2.AsEnumerable();

            var diff1 = v1.Except(v2, DataRowComparer.Default);
            var diff2 = v2.Except(v1, DataRowComparer.Default);
            if (diff1.Any())
            {
                DataTable diffTbl1 = diff1.CopyToDataTable();
            }

            if (diff2.Any())
            {
                DataTable diffTbl2 = diff2.CopyToDataTable();
            }

            return !(diff1.Any() || diff2.Any());
        }
    }
}