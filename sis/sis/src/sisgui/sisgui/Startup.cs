using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(sisgui.Startup))]
namespace sisgui
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
