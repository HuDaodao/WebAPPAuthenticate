using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebAppAuthenticate.Startup))]
namespace WebAppAuthenticate
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
