namespace YanZhiwei.DotNet.Framework.Contract
{
    public class BusinessRequest : ModelBase
    {
        public BusinessRequest()
        {
            PageSize = 5000;
        }

        public int Top
        {
            set
            {
                this.PageSize = value;
                this.PageIndex = 1;
            }
        }

        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
}