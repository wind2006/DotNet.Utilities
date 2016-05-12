using Molin_CRM.DAL;
using Molin_CRM.Model;
using System;

namespace Molin_CRM.Business
{
    public class SellBus
    {
        private static SellBus instance = null;

        public static SellBus Instance
        {
            get
            {
                if (instance == null)
                    instance = new SellBus();
                return instance;
            }
        }

        public bool Add(Sell sell, int totalNumber)
        {
            bool _result = false;
            try
            {
                string _addSell = string.Format("insert into Sell values('" + sell.Name + "','" + sell.Customer + "'," + sell.Number + "," + sell.Price + ",'" + DateTime.Now.ToString("s") + "')");
                string _updateProduct = string.Format("update Product set Number={0} where name='{1}'", (totalNumber - sell.Number), sell.Name);
                _result = DataAccess.Instance.SQLHelper.ExecuteNonQueryWithTrans(new string[2] { _addSell, _updateProduct }) >= 1;
            }
            catch (Exception)
            {
                _result = false;
            }
            return _result;
        }
    }
}