using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Nemesis.Web.Startup))]
namespace Nemesis.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
