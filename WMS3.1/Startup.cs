using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WMS3._1.Startup))]
namespace WMS3._1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
