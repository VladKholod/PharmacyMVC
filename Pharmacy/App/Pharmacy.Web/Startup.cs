using Microsoft.Owin;
using Owin;
using Pharmacy.Web;

[assembly: OwinStartup(typeof(Startup))]
namespace Pharmacy.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
