using System;
using YanZhiwei.DotNet2.Utilities.WebForm.Core;
using System.Web.Caching;
using YanZhiwei.DotNet2.Utilities.Common;
using System.Diagnostics;
namespace YanZhiwei.DotNet2.Utilities.WebFormExamples
{
    public partial class CacheDemo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("Set Cache By AbsoluteExpiration:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            CacheManger.Set("time", DateTime.Now, null, DateTime.Now.AddSeconds(10), Cache.NoSlidingExpiration, CacheItemPriority.High, (key, value, reason) =>
            {
                Debug.WriteLine("Set Cache By AbsoluteExpiration Expired:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                if (CacheItemRemovedReason.Expired == reason)
                {

                }
                Debug.WriteLine("Contain Cache:" + CacheManger.Contain("time"));

            });

        }

        protected void Button2_Click(object sender, EventArgs e)
        {

            Debug.WriteLine("Set Cache By SlidingExpiration:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            CacheManger.Set("time", DateTime.Now, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, 0, 0, 10, 0), ItemRemovedCallback);
        }
        private void ItemRemovedCallback(string key, object value, CacheItemRemovedReason reason)
        {
            Debug.WriteLine("Set Cache By SlidingExpiration Expired:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            CacheManger.Set(key, value, null, Cache.NoAbsoluteExpiration, new TimeSpan(0, 0, 0, 10, 0), CacheItemPriority.NotRemovable, ItemRemovedCallback);

        }
        private void WriteLog(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
            System.Diagnostics.EventLog log = new System.Diagnostics.EventLog("Application");
            log.Source = "Application";
            log.WriteEntry(message, System.Diagnostics.EventLogEntryType.Warning, 1);
        }

    }
}