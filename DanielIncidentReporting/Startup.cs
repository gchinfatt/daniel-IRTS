using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DanielIncidentReporting.Startup))]
namespace DanielIncidentReporting
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
