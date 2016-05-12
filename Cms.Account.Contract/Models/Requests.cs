using YanZhiwei.DotNet.Framework.Contract;

namespace MvcSolution.Cms.Models
{
    public class ArticleRequest : BusinessRequest
    {
        public string Title { get; set; }
        public int ChannelId { get; set; }
        public bool? IsActive { get; set; }
    }

    public class ChannelRequest : BusinessRequest
    {
        public string Name { get; set; }
        public bool? IsActive { get; set; }
    }

    public class TagRequest : BusinessRequest
    {
        public Orderby Orderby { get; set; }
    }

    public enum Orderby
    {
        ID = 0,
        Hits = 1
    }
}