namespace cdz360Tools.Services
{
    using Services;
    using YanZhiwei.DotNet.Framework.Data;

    public class Cdz360DbContext : DbContextBase
    {
        #region Constructors

        public Cdz360DbContext()
            : base(CachedConfigContext.Current.DaoConfig.Cdz360ConnectString, null)
        {
        }

        #endregion Constructors
    }
}