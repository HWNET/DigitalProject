using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Digital.Chart.Startup))]
namespace Digital.Chart
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            ConfigureAuth(app);
        }
    }
}
