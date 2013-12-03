using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebForLocalizedUrl.Startup))]
namespace WebForLocalizedUrl
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
