using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(alterdata.Site.Web.Startup))]
namespace alterdata.Site.Web
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
