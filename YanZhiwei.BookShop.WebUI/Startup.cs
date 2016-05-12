using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(YanZhiwei.BookShop.WebUI.Startup))]
namespace YanZhiwei.BookShop.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
