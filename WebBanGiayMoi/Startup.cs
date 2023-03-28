using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebBanGiayMoi.Startup))]
namespace WebBanGiayMoi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
