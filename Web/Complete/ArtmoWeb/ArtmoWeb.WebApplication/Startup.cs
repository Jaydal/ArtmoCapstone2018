using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ArtmoWeb.WebApplication.Startup))]
namespace ArtmoWeb.WebApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
