using System.Windows.Forms;
using YanZhiwei.DotNet.SQLite.Utilities;

namespace Molin_CRM.DAL
{
    public class DataAccess
    {
        private static readonly object syncObject = new object();
        private static DataAccess instance = null;
        public SQLiteHelper SQLHelper = null;

        public DataAccess()
        {
            SQLHelper = new SQLiteHelper(string.Format(@"{0}\DB\db.db", Application.StartupPath));
        }

        public static DataAccess Instance
        {
            get
            {
                lock (syncObject)
                {
                    if (instance == null)
                        instance = new DataAccess();
                    return instance;
                }
            }
        }
    }
}