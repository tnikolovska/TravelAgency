using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TravelAgency.Startup))]
namespace TravelAgency
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
