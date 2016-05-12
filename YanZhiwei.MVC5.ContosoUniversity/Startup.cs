using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(YanZhiwei.MVC5.ContosoUniversity.Startup))]
namespace YanZhiwei.MVC5.ContosoUniversity
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
