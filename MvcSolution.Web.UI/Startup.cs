using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MvcSolution.Web.UI.Startup))]
namespace MvcSolution.Web.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
