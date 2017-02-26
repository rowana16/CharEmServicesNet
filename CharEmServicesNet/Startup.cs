using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CharEmServicesNet.Startup))]
namespace CharEmServicesNet
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
