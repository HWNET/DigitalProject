using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Digital.Web.Startup))]
namespace Digital.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
